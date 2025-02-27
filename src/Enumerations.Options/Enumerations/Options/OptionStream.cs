namespace ReillyDigital.Enumerations.Options;

using System;
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
	/// <summary>
	/// The current option from the stream.
	/// </summary>
	public IOption<TValue>? Current { get; private set; }

	/// <inheritdoc />
	public IEnumerable<Exception> IgnoredErrors => throw new(
		"Ignored errors are only supported on option stream values, not on the stream itself."
	);

	/// <summary>
	/// Buffer used to temporarily hold items of the stream until they are read.
	/// </summary>
	private Queue<IOption<TValue>>? Buffer { get; }

	/// <summary>
	/// A lock to prevent too many items from being added to the buffer at once.
	/// </summary>
	private SemaphoreSlim BufferLock { get; } = new(1, 1);

	/// <summary>
	/// The size of the buffer.
	/// </summary>
	private int BufferSize { get; }

	/// <summary>
	/// The next option to be read from the stream when the buffer is not used.
	/// </summary>
	private IOption<TValue>? UnbufferedNext { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="OptionStream{TValue}" /> class.
	/// </summary>
	/// <param name="bufferSize">The size of the buffer.</param>
	public OptionStream(int bufferSize = 0)
	{
		BufferSize = bufferSize;
		if (BufferSize > 0)
		{
			Buffer = [];
		}
	}

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
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task End(
		IEnumerable<Exception>? ignoredErrors = null, CancellationToken cancellationToken = default
	) => await Next(
		IOption<TValue>.End(ignoredErrors: ignoredErrors), cancellationToken: cancellationToken
	);

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="error">The option to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Error(IError error, CancellationToken cancellationToken = default)
	{
		if (error is IOption<TValue> option)
		{
			await Next(option, cancellationToken: cancellationToken);
			return;
		}
		await Next(
			IOption<TValue>.Error(error.Value, ignoredErrors: error.IgnoredErrors),
			cancellationToken: cancellationToken
		);
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the stream.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Error(
		Exception value,
		IEnumerable<Exception>? ignoredErrors = null,
		CancellationToken cancellationToken = default
	) => await Next(
		IOption<TValue>.Error(value, ignoredErrors: ignoredErrors),
		cancellationToken: cancellationToken
	);

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="message">The error message.</param>
	/// <param name="innerException">An optional inner exception.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Error(
		string message,
		Exception? innerException = null,
		IEnumerable<Exception>? ignoredErrors = null,
		CancellationToken cancellationToken = default
	) => await Next(
		IOption<TValue>.Error(message, innerException: innerException, ignoredErrors: ignoredErrors),
		cancellationToken: cancellationToken
	);

	/// <summary>
	/// A chainable call to add an option of <see cref="IOption{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="option">The option to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Next(IOption<TValue> option, CancellationToken cancellationToken = default)
	{
		await BufferLock.WaitAsync(cancellationToken);
		if (Buffer is null)
		{
			while (UnbufferedNext is null)
			{
				await Task.Delay(100, cancellationToken);
			}
			UnbufferedNext = option;
		}
		else
		{
			while (Buffer.Count >= BufferSize)
			{
				await Task.Delay(100, cancellationToken);
			}
			Buffer.Enqueue(option);
		}
		BufferLock.Release();
	}

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{TValue}" /> to the stream.
	/// The options are iterated over and added to the stream one at a time. Then returning this
	/// class instance.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Next(
		IOptionEnumerable<TValue> options, CancellationToken cancellationToken = default
	)
	{
		foreach (var option in options)
		{
			await Next(option, cancellationToken: cancellationToken);
		}
	}

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{TValue}" /> to the stream.
	/// The options are iterated over and added to the stream one at a time. Then returning this
	/// class instance.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Next(
		IAsyncOptionEnumerable<TValue> options, CancellationToken cancellationToken = default
	)
	{
		await foreach (var option in options)
		{
			await Next(option, cancellationToken: cancellationToken);
		}
	}

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{TValue}" /> to the stream. The
	/// options are iterated over and added to the stream one at a time. Then returning this class
	/// instance. If any of the options is of type <see cref="IEnd{TValue}" /> then
	/// <see cref="IsAtEnd" /> will be set to true.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Next(
		CancellationToken cancellationToken = default, params IOption<TValue>[] options
	) => await Next(new OptionList<TValue>(options), cancellationToken: cancellationToken);

	/// <summary>
	/// A chainable call to add an option of <see cref="INone{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task None(
		IEnumerable<Exception>? ignoredErrors = null, CancellationToken cancellationToken = default
	) => await Next(
		IOption<TValue>.None(ignoredErrors: ignoredErrors), cancellationToken: cancellationToken
	);

	/// <summary>
	/// Reads the next option from the stream. Once an option of <see cref="IEnd" /> is read, that
	/// will mark the end of the stream. An <see cref="Exception" /> will be thrown if the stream is
	/// read again after that.
	/// </summary>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>
	/// A task representing the asynchronous operation. The result value is the next option from
	/// the stream.
	/// </returns>
	/// <exception cref="Exception">Thrown when reading after the stream has ended.</exception>
	public async Task<IOption<TValue>> Read(CancellationToken cancellationToken = default)
	{
		if (Current is IEnd)
		{
			throw new Exception("Stream has already been read to end.");
		}
		if (Buffer is null)
		{
			while (UnbufferedNext is null)
			{
				await Task.Delay(100, cancellationToken);
			}
			Current = UnbufferedNext;
			UnbufferedNext = null;
		}
		else
		{
			IOption<TValue>? next;
			while (!Buffer.TryDequeue(out next) || next is null)
			{
				await Task.Delay(100, cancellationToken);
			}
			Current = next;
		}
		return Current;
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
		bool shouldSkipErrors = false,
		[EnumeratorCancellation] CancellationToken cancellationToken = default
	)
	{
		while (Current is not IEnd)
		{
			var next = await Read(cancellationToken: cancellationToken);
			switch (next)
			{
				case IEnd:
				{
					yield break;
				}
				case IError:
				{
					if (shouldSkipErrors)
					{
						continue;
					}
					yield return next;
					break;
				}
				case ISome<TValue>:
				{
					yield return next;
					break;
				}
				default:
				{
					continue;
				}
			}
		}
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="some">The option to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Some(
		ISome<TValue> some, CancellationToken cancellationToken = default
	) => await Next(
		IOption<TValue>.Some(some.Value, ignoredErrors: some.IgnoredErrors),
		cancellationToken: cancellationToken
	);

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue}" /> to the stream, returning
	/// this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the stream.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Some(
		TValue value,
		IEnumerable<Exception>? ignoredErrors = null,
		CancellationToken cancellationToken = default
	) => await Next(
		IOption<TValue>.Some(value, ignoredErrors: ignoredErrors),
		cancellationToken: cancellationToken
	);
}

/// <summary>
/// Represents a stream of options with a value of <see cref="TValue" /> that are accessed by
/// subscribing to events of each possible option type, triggered when an item of that type is
/// added to the stream. Errors are of type <see cref="Exception" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the errors.</typeparam>
public sealed class OptionStream<TValue, TError> : IVoid<TError>
{
	/// <summary>
	/// The current option from the stream.
	/// </summary>
	public IOption<TValue, TError>? Current { get; private set; }

	/// <inheritdoc />
	public IEnumerable<TError> IgnoredErrors => throw new(
		"Ignored errors are only supported on option stream values, not on the stream itself."
	);

	/// <summary>
	/// Buffer used to temporarily hold items of the stream until they are read.
	/// </summary>
	private Queue<IOption<TValue, TError>>? Buffer { get; }

	/// <summary>
	/// A lock to prevent too many items from being added to the buffer at once.
	/// </summary>
	private SemaphoreSlim BufferLock { get; } = new(1, 1);

	/// <summary>
	/// The size of the buffer.
	/// </summary>
	private int BufferSize { get; }

	/// <summary>
	/// The next option to be read from the stream when the buffer is not used.
	/// </summary>
	private IOption<TValue, TError>? UnbufferedNext { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="OptionStream{TValue, TError}" /> class.
	/// </summary>
	/// <param name="bufferSize">The size of the buffer.</param>
	public OptionStream(int bufferSize = 0)
	{
		BufferSize = bufferSize;
		if (BufferSize > 0)
		{
			Buffer = [];
		}
	}

	/// <summary>
	/// Returns a read-only wrapper for the current stream.
	/// </summary>
	/// <returns>
	/// A new <see cref="ReadOnlyOptionStream{TValue, TError}" /> wrapping this stream.
	/// </returns>
	public ReadOnlyOptionStream<TValue, TError> AsReadOnly() => new(this);

	/// <summary>
	/// A chainable call to add an option of <see cref="IEnd{TValue, TError}" /> to the stream,
	/// returning this class instance.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task End(
		IEnumerable<TError>? ignoredErrors = null, CancellationToken cancellationToken = default
	) => await Next(
		IOption<TValue, TError>.End(ignoredErrors: ignoredErrors),
		cancellationToken: cancellationToken
	);

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue, TError}" /> to the stream,
	/// returning this class instance.
	/// </summary>
	/// <param name="error">The option to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Error(
		IError<TValue, TError> error, CancellationToken cancellationToken = default
	)
	{
		if (error is IOption<TValue, TError> option)
		{
			await Next(option, cancellationToken: cancellationToken);
			return;
		}
		await Next(
			IOption<TValue, TError>.Error(error.Value, ignoredErrors: error.IgnoredErrors),
			cancellationToken: cancellationToken
		);
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="IOption{TValue, TError}" /> to the stream,
	/// returning this class instance.
	/// </summary>
	/// <param name="option">The option to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Next(
		IOption<TValue, TError> option, CancellationToken cancellationToken = default
	)
	{
		await BufferLock.WaitAsync(cancellationToken);
		if (Buffer is null)
		{
			while (UnbufferedNext is null)
			{
				await Task.Delay(100, cancellationToken);
			}
			UnbufferedNext = option;
		}
		else
		{
			while (Buffer.Count >= BufferSize)
			{
				await Task.Delay(100, cancellationToken);
			}
			Buffer.Enqueue(option);
		}
		BufferLock.Release();
	}

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{TValue, TError}" /> to the
	/// stream. The options are iterated over and added to the stream one at a time. Then returning
	/// this class instance.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Next(
		IOptionEnumerable<TValue, TError> options, CancellationToken cancellationToken = default
	)
	{
		foreach (var option in options)
		{
			await Next(option, cancellationToken: cancellationToken);
		}
	}

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{TValue, TError}" /> to the
	/// stream. The options are iterated over and added to the stream one at a time. Then returning
	/// this class instance.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Next(
		IAsyncOptionEnumerable<TValue, TError> options, CancellationToken cancellationToken = default
	)
	{
		await foreach (var option in options)
		{
			await Next(option, cancellationToken: cancellationToken);
		}
	}

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{TValue, TError}" /> to the
	/// stream. The options are iterated over and added to the stream one at a time. Then returning
	/// this class instance.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Next(
		CancellationToken cancellationToken = default, params IOption<TValue, TError>[] options
	) => await Next(new OptionList<TValue, TError>(options), cancellationToken: cancellationToken);

	/// <summary>
	/// A chainable call to add an option of <see cref="INone{TValue, TError}" /> to the stream,
	/// returning this class instance.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task None(
		IEnumerable<TError>? ignoredErrors = null, CancellationToken cancellationToken = default
	) => await Next(
		IOption<TValue, TError>.None(ignoredErrors: ignoredErrors),
		cancellationToken: cancellationToken
	);

	/// <summary>
	/// Reads the next option from the stream. Once an option of <see cref="IEnd" /> is read, that
	/// will mark the end of the stream. An <see cref="TError" /> will be thrown if the stream is
	/// read again after that.
	/// </summary>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>
	/// A task representing the asynchronous operation. The result value is the next option from
	/// the stream.
	/// </returns>
	/// <exception cref="TError">Thrown when reading after the stream has ended.</exception>
	public async Task<IOption<TValue, TError>> Read(
		CancellationToken cancellationToken = default
	)
	{
		if (Current is IEnd)
		{
			throw new Exception("Stream has already been read to end.");
		}
		if (Buffer is null)
		{
			while (UnbufferedNext is null)
			{
				await Task.Delay(100, cancellationToken);
			}
			Current = UnbufferedNext;
			UnbufferedNext = null;
		}
		else
		{
			IOption<TValue, TError>? next;
			while (!Buffer.TryDequeue(out next) || next is null)
			{
				await Task.Delay(100, cancellationToken);
			}
			Current = next;
		}
		return Current;
	}

	/// <summary>
	/// Reads all options from the stream until an option of <see cref="IEnd" /> is read, which will
	/// mark the end of the stream. An <see cref="TError" /> will be thrown if the stream is read
	/// again after that.
	/// </summary>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>
	/// An enumeration of option from the stream until the end of the stream is reached.
	/// </returns>
	/// <exception cref="TError">Thrown when reading after the stream has ended.</exception>
	public async IAsyncEnumerable<IOption<TValue, TError>> ReadToEnd(
		bool shouldSkipErrors = false,
		[EnumeratorCancellation] CancellationToken cancellationToken = default
	)
	{
		while (Current is not IEnd)
		{
			var next = await Read(cancellationToken: cancellationToken);
			switch (next)
			{
				case IEnd:
				{
					yield break;
				}
				case IError:
				{
					if (shouldSkipErrors)
					{
						continue;
					}
					yield return next;
					break;
				}
				case ISome<TValue>:
				{
					yield return next;
					break;
				}
				default:
				{
					continue;
				}
			}
		}
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue, TError}" /> to the stream,
	/// returning this class instance.
	/// </summary>
	/// <param name="some">The option to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Some(
		ISome<TValue, TError> some, CancellationToken cancellationToken = default
	) => await Next(
		IOption<TValue, TError>.Some(some.Value, ignoredErrors: some.IgnoredErrors),
		cancellationToken: cancellationToken
	);

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue, TError}" /> to the stream,
	/// returning this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the stream.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Some(
		TValue value,
		IEnumerable<TError>? ignoredErrors = null,
		CancellationToken cancellationToken = default
	) => await Next(
		IOption<TValue, TError>.Some(value, ignoredErrors: ignoredErrors),
		cancellationToken: cancellationToken
	);
}
