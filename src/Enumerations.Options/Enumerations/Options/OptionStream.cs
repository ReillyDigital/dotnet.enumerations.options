namespace ReillyDigital.Enumerations.Options;

using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

/// <summary>
/// Represents a stream of options with a value of <see cref="TValue" /> that are accessed by
/// subscribing to events of each possible option type, triggered when an item of that type is
/// added to the stream. Errors are of type <see cref="Exception" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public sealed class OptionStream<TValue> : IVoid
{
	/// <inheritdoc />
	public IEnumerable<Exception> IgnoredErrors => throw new(
		"Ignored errors are only supported on option stream values, not on the stream itself."
	);

	/// <summary>
	/// Flag set when an option of <see cref="IEnd" /> is read from the stream, marking the end of
	/// the stream.
	/// </summary>
	public bool IsAtEnd { get; private set; } = false;

	/// <summary>
	/// Queue used to temporarily hold items of the stream until they are read.
	/// </summary>
	private ConcurrentQueue<IOption<TValue>> Queue { get; } = [];

	/// <summary>
	/// Returns a read-only wrapper for the current stream.
	/// </summary>
	/// <returns>A new <see cref="ReadOnlyOptionStream{TValue}" /> wrapping this stream.</returns>
	public ReadOnlyOptionStream<TValue> AsReadOnly() => new(this);

	/// <summary>
	/// A chainable call to add an option of <see cref="IEnd{TValue}" /> to the stream, returning this
	/// class instance.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> End(IEnumerable<Exception>? ignoredErrors = null)
		=> Next(IOption<TValue>.End(ignoredErrors: ignoredErrors));

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="message">The error message.</param>
	/// <param name="innerException">An optional inner exception.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> Error(
		string message, Exception? innerException = null, IEnumerable<Exception>? ignoredErrors = null
	) => Next(IOption<TValue>.Error(
		message, innerException: innerException, ignoredErrors: ignoredErrors)
	);

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the stream.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> Error(Exception value, IEnumerable<Exception>? ignoredErrors = null)
		=> Next(IOption<TValue>.Error(value, ignoredErrors: ignoredErrors));

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="error">The option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> Error(IError<TValue> error)
	{
		if (error is IOption<TValue> option)
		{
			return Next(option);
		}
		return Next(IOption<TValue>.Error(error.Value, ignoredErrors: error.IgnoredErrors));
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="IOption{TValue}" /> to the stream, returning
	/// this class instance. If the option is of type <see cref="IEnd{TValue}" /> then
	/// <see cref="IsAtEnd" /> will be set to true.
	/// </summary>
	/// <param name="option">The option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> Next(IOption<TValue> option)
	{
		Queue.Enqueue(option);
		return this;
	}

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{TValue}" /> to the stream. The
	/// options are iterated over and added to the stream one at a time. Then returning this class
	/// instance. If any of the options is of type <see cref="IEnd{TValue}" /> then
	/// <see cref="IsAtEnd" /> will be set to true.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> Next(params IOption<TValue>[] options)
		=> Next(new OptionList<TValue>(options));

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{TValue}" /> to the stream.
	/// The options are iterated over and added to the stream one at a time. Then returning this
	/// class instance. If any of the options is of type <see cref="IEnd{TValue}" /> then
	/// <see cref="IsAtEnd" /> will be set to true.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> Next(IOptionEnumerable<TValue> options)
	{
		options.ForEach(Next);
		return this;
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="INone{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> None(IEnumerable<Exception>? ignoredErrors = null)
		=> Next(IOption<TValue>.None(ignoredErrors: ignoredErrors));

	/// <summary>
	/// Reads the next option from the stream. Once an option of <see cref="IEnd" /> is read, that
	/// will mark the end of the stream. An <see cref="Exception" /> will be thrown if the stream is
	/// read again after that.
	/// </summary>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>The next option from the stream.</returns>
	/// <exception cref="Exception">Thrown when reading after the stream has ended.</exception>
	public async Task<IOption<TValue>> Read(CancellationToken cancellationToken = default)
	{
		if (IsAtEnd)
		{
			throw new Exception("Stream has already been read to end.");
		}
		IOption<TValue>? next;
		while (!Queue.TryDequeue(out next) || next is null)
		{
			cancellationToken.ThrowIfCancellationRequested();
			await Task.Delay(100, cancellationToken: cancellationToken);
		}
		if (next is IEnd)
		{
			IsAtEnd = true;
		}
		return next;
	}

	/// <summary>
	/// Reads all options from the stream until an option of <see cref="IEnd" /> is read, which will
	/// mark the end of the stream. An <see cref="Exception" /> will be thrown if the stream is read
	/// again after that.
	/// </summary>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>
	/// An enumeration of option from the stream until the end of the stream is reached.
	/// </returns>
	/// <exception cref="Exception">Thrown when reading after the stream has ended.</exception>
	public async IAsyncEnumerable<IOption<TValue>> ReadToEnd(
		[EnumeratorCancellation] CancellationToken cancellationToken = default
	)
	{
		while (true)
		{
			var next = await Read(cancellationToken: cancellationToken);
			if (IsAtEnd)
			{
				yield break;
			}
			yield return next;
		}
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="some">The option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> Some(ISome<TValue> some)
	{
		if (some is IOption<TValue> option)
		{
			return Next(option);
		}
		return Next(IOption<TValue>.Some(some.Value, ignoredErrors: some.IgnoredErrors));
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the stream.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> Some(TValue value, IEnumerable<Exception>? ignoredErrors = null)
		=> Next(IOption<TValue>.Some(value, ignoredErrors: ignoredErrors));
}

/// <summary>
/// Represents a stream of options with a value of <see cref="TValue" /> that are accessed by
/// subscribing to events of each possible option type, triggered when an item of that type is
/// added to the stream. Errors are of type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public sealed class OptionStream<TValue, TError> : IVoid<TError>
{
	/// <inheritdoc />
	public IEnumerable<TError> IgnoredErrors => throw new(
		"Ignored errors are only supported on option stream values, not on the stream itself."
	);

	/// <summary>
	/// Flag set when an option of <see cref="IEnd" /> is read from the stream, marking the end of
	/// the stream.
	/// </summary>
	public bool IsAtEnd { get; private set; } = false;

	/// <summary>
	/// Queue used to temporarily hold items of the stream until they are read.
	/// </summary>
	private ConcurrentQueue<IOption<TValue, TError>> Queue { get; } = [];

	/// <summary>
	/// Returns a read-only wrapper for the current stream.
	/// </summary>
	/// <returns>A new <see cref="ReadOnlyOptionStream{TValue}" /> wrapping this stream.</returns>
	public ReadOnlyOptionStream<TValue, TError> AsReadOnly() => new(this);

	/// <summary>
	/// A chainable call to add an option of <see cref="IEnd{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> End(IEnumerable<TError>? ignoredErrors = null)
		=> Next(IOption<TValue, TError>.End(ignoredErrors: ignoredErrors));

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the stream.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Error(
		TError value, IEnumerable<TError>? ignoredErrors = null
	) => Next(IOption<TValue, TError>.Error(value, ignoredErrors: ignoredErrors));

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="error">The option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Error(IError<TValue, TError> error)
	{
		if (error is IOption<TValue, TError> option)
		{
			return Next(option);
		}
		return Next(IOption<TValue, TError>.Error(error.Value, ignoredErrors: error.IgnoredErrors));
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="IOption{TValue}" /> to the stream, returning
	/// this class instance. If the option is of type <see cref="IEnd{TValue}" /> then
	/// <see cref="IsAtEnd" /> will be set to true.
	/// </summary>
	/// <param name="option">The option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Next(IOption<TValue, TError> option)
	{
		Queue.Enqueue(option);
		return this;
	}

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{TValue}" /> to the stream.
	/// The options are iterated over and added to the stream one at a time. Then returning this
	/// class instance. If any of the options is of type <see cref="IEnd{TValue}" /> then
	/// <see cref="IsAtEnd" /> will be set to true.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Next(params IOption<TValue, TError>[] options)
		=> Next(new OptionList<TValue, TError>(options));

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{TValue}" /> to the stream.
	/// The options are iterated over and added to the stream one at a time. Then returning this
	/// class instance. If any of the options is of type <see cref="IEnd{TValue}" /> then
	/// <see cref="IsAtEnd" /> will be set to true.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Next(IOptionEnumerable<TValue, TError> options)
	{
		options.ForEach(Next);
		return this;
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="INone{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> None(IEnumerable<TError>? ignoredErrors = null)
		=> Next(IOption<TValue, TError>.None(ignoredErrors: ignoredErrors));

	/// <summary>
	/// Reads the next option from the stream. Once an option of <see cref="IEnd" /> is read, that
	/// will mark the end of the stream. An <see cref="Exception" /> will be thrown if the stream is
	/// read again after that.
	/// </summary>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>The next option from the stream.</returns>
	/// <exception cref="Exception">Thrown when reading after the stream has ended.</exception>
	public async Task<IOption<TValue, TError>> Read(CancellationToken cancellationToken = default)
	{
		if (IsAtEnd)
		{
			throw new Exception("Stream has already been read to end.");
		}
		IOption<TValue, TError>? next;
		while (!Queue.TryDequeue(out next) || next is null)
		{
			cancellationToken.ThrowIfCancellationRequested();
			await Task.Delay(100, cancellationToken: cancellationToken);
		}
		if (next is IEnd)
		{
			IsAtEnd = true;
		}
		return next;
	}

	/// <summary>
	/// Reads all options from the stream until an option of <see cref="IEnd" /> is read, which will
	/// mark the end of the stream. An <see cref="Exception" /> will be thrown if the stream is read
	/// again after that.
	/// </summary>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>
	/// An enumeration of option from the stream until the end of the stream is reached.
	/// </returns>
	/// <exception cref="Exception">Thrown when reading after the stream has ended.</exception>
	public async IAsyncEnumerable<IOption<TValue, TError>> ReadToEnd(
		[EnumeratorCancellation] CancellationToken cancellationToken = default
	)
	{
		while (true)
		{
			var next = await Read(cancellationToken: cancellationToken);
			if (IsAtEnd)
			{
				yield break;
			}
			yield return next;
		}
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="some">The option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Some(ISome<TValue, TError> some)
	{
		if (some is IOption<TValue, TError> option)
		{
			return Next(option);
		}
		return Next(IOption<TValue, TError>.Some(some.Value, ignoredErrors: some.IgnoredErrors));
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the stream.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Some(TValue value, IEnumerable<TError>? ignoredErrors = null)
		=> Next(IOption<TValue, TError>.Some(value, ignoredErrors: ignoredErrors));
}
