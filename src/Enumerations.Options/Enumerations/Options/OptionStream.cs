namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a stream of options with a value of <see cref="TValue" /> that are accessed by subscribing to events
/// of each possible option type, triggered when an item of that type is added to the stream. Errors are of type
/// <see cref="Exception" />.
/// </summary>
public class OptionStream<TValue> : OptionStream<TValue, Exception>
{
	/// <summary>
	/// Returns a read-only wrapper for the current stream.
	/// </summary>
	/// <returns>A new <see cref="ReadOnlyOptionStream{}" /> wrapping this stream.</returns>
	public new ReadOnlyOptionStream<TValue> AsReadOnly() => new(this);
}

/// <summary>
/// Represents a stream of options with a value of <see cref="TValue" /> that are accessed by subscribing to events
/// of each possible option type, triggered when an item of that type is added to the stream. Errors are of type
/// <see cref="TError" />.
/// </summary>
public class OptionStream<TValue, TError> : IVoid
{
	/// <summary>
	/// An event triggered when an option of type <see cref="IEnd" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue>>? EndReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="IError{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue>>? ErrorReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="INone" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue>>? NoneReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue>>? OptionReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="ISome{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue>>? SomeReceived;

	/// <summary>
	/// A task that is resolved once an option of <see cref="IEnd" /> is added to the stream.
	/// </summary>
	public Task<IEnd> EndOfStream => TaskCompletionSource.Task;

	/// <summary>
	/// Cancellation tokens registered with this stream. The task for <see cref="EndOfStream" /> will be cancelled upon
	/// the cancellation of any of these tokens.
	/// </summary>
	private Dictionary<CancellationToken, CancellationTokenRegistration> CancellationTokens { get; init; } = [];

	/// <summary>
	/// A task source used to resolve <see cref="EndOfStream" />.
	/// </summary>
	private TaskCompletionSource<IEnd> TaskCompletionSource { get; } = new();

	/// <summary>
	/// Returns a read-only wrapper for the current stream.
	/// </summary>
	/// <returns>A new <see cref="ReadOnlyOptionStream{}" /> wrapping this stream.</returns>
	public ReadOnlyOptionStream<TValue, TError> AsReadOnly() => new(this);

	/// <summary>
	/// A chainable call to add an option of <see cref="IEnd" /> to the stream, returning this class instance.
	/// </summary>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> End() => Next(End<TValue>());

	/// <summary>
	/// A chainable call to add an option of <see cref="IError" /> to the stream, returning this class instance.
	/// </summary>
	/// <param name="message">The error message.</param>
	/// <param name="innerException">An optional inner exception.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Error(string message, Exception? innerException = null) =>
		Next(IOption<TValue>.Error(message, innerException));

	/// <summary>
	/// A chainable call to add an option of <see cref="IError" /> to the stream, returning this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Error(TError value) => Next(IOption<TValue>.Error(value));

	/// <summary>
	/// A chainable call to add an option of <see cref="IError" /> to the stream, returning this class instance.
	/// </summary>
	/// <param name="error">The option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Error(IError error)
	{
		if (error is IOption<TValue> option)
		{
			return Next(option);
		}
		return Next(IOption<TValue>.Error(error.Value));
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="IOption{}" /> to the stream, returning this class instance. If
	/// the option is of type <see cref="IEnd" /> then <see cref="EndOfStream" /> will be resolved.
	/// </summary>
	/// <param name="option">The option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Next(IOption<TValue> option)
	{
		OptionReceived?.Invoke(this, option);
		switch (option)
		{
			case IEnd end:
			{
				EndReceived?.Invoke(this, option);
				if (!TaskCompletionSource.Task.IsCompleted)
				{
					TaskCompletionSource.SetResult(end);
				}
				break;
			}
			case IError<TError>:
			{
				ErrorReceived?.Invoke(this, option);
				break;
			}
			case INone:
			{
				NoneReceived?.Invoke(this, option);
				break;
			}
			case ISome<TValue>:
			{
				SomeReceived?.Invoke(this, option);
				break;
			}
		}
		return this;
	}

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{}" /> to the stream. The options are iterated
	/// over and added to the stream one at a time. Then returning this class instance. If any of the options is of type
	/// <see cref="IEnd" /> then <see cref="EndOfStream" /> will be resolved.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Next(params IOption<TValue>[] options) => Next(new OptionList<TValue>(options));

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{}" /> to the stream. The options are iterated
	/// over and added to the stream one at a time. Then returning this class instance. If any of the options is of type
	/// <see cref="IEnd" /> then <see cref="EndOfStream" /> will be resolved.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Next(IOptionEnumerable<TValue> options)
	{
		options.ForEach(Next);
		return this;
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="INone" /> to the stream, returning this class instance.
	/// </summary>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> None() => Next(None<TValue>());

	/// <summary>
	/// A chainable call to register a cancellation token with the stream, then returning this class instance. The task
	/// for <see cref="EndOfStream" /> will be cancelled upon the cancellation of the provided token. If the provided
	/// token is equal to <see cref="CancellationToken.None" /> then nothing will be registered.
	/// </summary>
	/// <param name="cancellationToken">A cancellation token to register with the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> RegisterCancellationToken(CancellationToken cancellationToken)
	{
		if (cancellationToken == CancellationToken.None)
		{
			return this;
		}
		CancellationTokens.TryAdd(
			cancellationToken,
			cancellationToken.Register(() => TaskCompletionSource.SetCanceled(cancellationToken))
		);
		return this;
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{}" /> to the stream, returning this class instance.
	/// </summary>
	/// <param name="some">The option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Some(ISome<TValue> some)
	{
		if (some is IOption<TValue> option)
		{
			return Next(option);
		}
		return Next(IOption<TValue>.Some(some.Value));
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{}" /> to the stream, returning this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Some(TValue value) => Next(IOption<TValue>.Some(value));

	/// <summary>
	/// A chainable call to unregister a cancellation token with the stream, then returning this class instance. If the
	/// provided token is not registered then nothing will be unregistered.
	/// </summary>
	/// <param name="cancellationToken">A cancellation token to unregister from the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> UnregisterCancellationToken(CancellationToken cancellationToken)
	{
		if (cancellationToken == CancellationToken.None)
		{
			return this;
		}
		if (!CancellationTokens.TryGetValue(cancellationToken, out var registration))
		{
			return this;
		}
		registration.Unregister();
		CancellationTokens.Remove(cancellationToken);
		return this;
	}
}
