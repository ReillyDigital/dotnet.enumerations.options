namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a stream of options with a value of <see cref="TValue" /> that are accessed by subscribing to events
/// of each possible option type, triggered when an item of that type is added to the stream.
/// </summary>
public sealed class OptionStream<TValue>
{
	/// <summary>
	/// An event triggered when an option of type <see cref="End{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<End<TValue>>? EndReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="Error{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<Error<TValue>>? ErrorReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="None{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<None<TValue>>? NoneReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue>>? OptionReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="Some{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<Some<TValue>>? SomeReceived;

	/// <summary>
	/// A task that is resolved once an option of <see cref="End{}" /> is added to the stream.
	/// </summary>
	public Task<End<TValue>> EndOfStream => TaskCompletionSource.Task;

	/// <summary>
	/// Cancellation tokens registered with this stream. The task for <see cref="EndOfStream" /> will be cancelled upon
	/// the cancellation of any of these tokens.
	/// </summary>
	private Dictionary<CancellationToken, CancellationTokenRegistration> CancellationTokens { get; init; } = [];

	/// <summary>
	/// A task source used to resolve <see cref="EndOfStream" />.
	/// </summary>
	private TaskCompletionSource<End<TValue>> TaskCompletionSource { get; } = new();

	/// <summary>
	/// Returns a read-only wrapper for the current stream.
	/// </summary>
	/// <returns>A new <see cref="ReadOnlyOptionStream{}" /> wrapping this stream.</returns>
	public ReadOnlyOptionStream<TValue> AsReadOnly() => new(this);

	/// <summary>
	/// A chainable call to add an option of <see cref="End{}" /> to the stream, returning this class instance.
	/// </summary>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> End() => Next(End<TValue>());

	/// <summary>
	/// A chainable call to add an option of <see cref="Error{}" /> to the stream, returning this class instance.
	/// </summary>
	/// <param name="error">The option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> Error(Error<TValue> error) => Next(error);

	/// <summary>
	/// A chainable call to add an option of <see cref="IOption{}" /> to the stream, returning this class instance. If
	/// the option is of type <see cref="End{}" /> then <see cref="EndOfStream" /> will be resolved.
	/// </summary>
	/// <param name="option">The option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> Next(IOption<TValue> option)
	{
		OptionReceived?.Invoke(this, option);
		switch (option)
		{
			case End<TValue> end:
			{
				EndReceived?.Invoke(this, end);
				if (!TaskCompletionSource.Task.IsCompleted)
				{
					TaskCompletionSource.SetResult(end);
				}
				break;
			}
			case Error<TValue> error:
			{
				ErrorReceived?.Invoke(this, error);
				break;
			}
			case None<TValue> none:
			{
				NoneReceived?.Invoke(this, none);
				break;
			}
			case Some<TValue> some:
			{
				SomeReceived?.Invoke(this, some);
				break;
			}
		}
		return this;
	}

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{}" /> to the stream. The options are iterated
	/// over and added to the stream one at a time. Then returning this class instance. If any of the options is of type
	/// <see cref="End{}" /> then <see cref="EndOfStream" /> will be resolved.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> Next(params IOption<TValue>[] options) => Next(new OptionList<TValue>(options));

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{}" /> to the stream. The options are iterated
	/// over and added to the stream one at a time. Then returning this class instance. If any of the options is of type
	/// <see cref="End{}" /> then <see cref="EndOfStream" /> will be resolved.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> Next(IOptionEnumerable<TValue> options)
	{
		options.ForEach(Next);
		return this;
	}

	/// <summary>
	/// A chainable call to register a cancellation token with the stream, then returning this class instance. The task
	/// for <see cref="EndOfStream" /> will be cancelled upon the cancellation of the provided token. If the provided
	/// token is equal to <see cref="CancellationToken.None" /> then nothing will be registered.
	/// </summary>
	/// <param name="cancellationToken">A cancellation token to register with the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> RegisterCancellationToken(CancellationToken cancellationToken)
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
	/// A chainable call to add an option of <see cref="Some{}" /> to the stream, returning this class instance.
	/// </summary>
	/// <param name="some">The option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> Some(Some<TValue> some) => Next(some);

	/// <summary>
	/// A chainable call to unregister a cancellation token with the stream, then returning this class instance. If the
	/// provided token is not registered then nothing will be unregistered.
	/// </summary>
	/// <param name="cancellationToken">A cancellation token to unregister from the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue> UnregisterCancellationToken(CancellationToken cancellationToken)
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

/// <summary>
/// Represents a stream of options with a value of <see cref="TValue" /> that are accessed by subscribing to events
/// of each possible option type, triggered when an item of that type is added to the stream.
/// </summary>
public sealed class OptionStream<TValue, TError>
{
	/// <summary>
	/// An event triggered when an option of type <see cref="End{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<End<TValue>>? EndReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="Error{,}" /> is added to the stream.
	/// </summary>
	public event EventHandler<Error<TValue, TError>>? ErrorReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="None{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<None<TValue>>? NoneReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue>>? OptionReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="Some{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<Some<TValue>>? SomeReceived;

	/// <summary>
	/// A task that is resolved once an option of <see cref="End{} "/> is added to the stream.
	/// </summary>
	public Task<End<TValue>> EndOfStream => TaskCompletionSource.Task;

	/// <summary>
	/// Cancellation tokens registered with this stream. The task for <see cref="EndOfStream" /> will be cancelled upon
	/// the cancellation of any of these tokens.
	/// </summary>
	private Dictionary<CancellationToken, CancellationTokenRegistration> CancellationTokens { get; init; } = [];

	/// <summary>
	/// A task source used to resolve <see cref="EndOfStream" />.
	/// </summary>
	private TaskCompletionSource<End<TValue>> TaskCompletionSource { get; } = new();

	/// <summary>
	/// Returns a read-only wrapper for the current stream.
	/// </summary>
	/// <returns>A new <see cref="ReadOnlyOptionStream{,}" /> wrapping this stream.</returns>
	public ReadOnlyOptionStream<TValue, TError> AsReadOnly() => new(this);

	/// <summary>
	/// A chainable call to add an option of <see cref="End{}" /> to the stream, returning this class instance.
	/// </summary>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> End() => Next(End<TValue>());

	/// <summary>
	/// A chainable call to add an option of <see cref="Error{,}" /> to the stream, returning this class instance.
	/// </summary>
	/// <param name="error">The option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Error(Error<TValue, TError> error) => Next(error);

	/// <summary>
	/// A chainable call to add an option of <see cref="IOption{}" /> to the stream, returning this class instance. If
	/// the option is of type <see cref="End{}" /> then <see cref="EndOfStream" /> will be resolved.
	/// </summary>
	/// <param name="option">The option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Next(IOption<TValue> option)
	{
		OptionReceived?.Invoke(this, option);
		switch (option)
		{
			case End<TValue> end:
			{
				EndReceived?.Invoke(this, end);
				if (!TaskCompletionSource.Task.IsCompleted)
				{
					TaskCompletionSource.SetResult(end);
				}
				break;
			}
			case Error<TValue, TError> error:
			{
				ErrorReceived?.Invoke(this, error);
				break;
			}
			case None<TValue> none:
			{
				NoneReceived?.Invoke(this, none);
				break;
			}
			case Some<TValue> some:
			{
				SomeReceived?.Invoke(this, some);
				break;
			}
		}
		return this;
	}

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{}" /> to the stream. The options are iterated
	/// over and added to the stream one at a time. Then returning this class instance. If any of the options is of type
	/// <see cref="End{}" /> then <see cref="EndOfStream" /> will be resolved.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Next(params IOption<TValue>[] options) =>
		Next(new OptionList<TValue>(options));

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{}" /> to the stream. The options are iterated
	/// over and added to the stream one at a time. Then returning this class instance. If any of the options is of type
	/// <see cref="End{}" /> then <see cref="EndOfStream" /> will be resolved.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Next(IOptionEnumerable<TValue> options)
	{
		options.ForEach(Next);
		return this;
	}

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
	/// A chainable call to add an option of <see cref="Some{}" /> to the stream, returning this class instance.
	/// </summary>
	/// <param name="some">The option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public OptionStream<TValue, TError> Some(Some<TValue> some) => Next(some);

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
