namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a stream of options with a value of <see cref="TValue" /> that are accessed by subscribing to events
/// of each possible option type, triggered when an item of that type is added to the stream. Errors are of type
/// <see cref="Exception" />.
/// </summary>
public class OptionStream<TValue> : OptionStream<TValue, Exception>
{
	/// <summary>
	/// An event triggered when an option of type <see cref="IError" /> is added to the stream.
	/// </summary>
	public new event EventHandler<IError>? ErrorReceived;

	/// <inheritdoc />
	public override ReadOnlyOptionStream<TValue> AsReadOnly() => new(this);

	/// <inheritdoc />
	public override OptionStream<TValue> End() => (OptionStream<TValue>)base.End();

	/// <inheritdoc />
	public override OptionStream<TValue> Error(string message, Exception? innerException = null) =>
		(OptionStream<TValue>)base.Error(message, innerException);

	/// <inheritdoc />
	public override OptionStream<TValue> Error(Exception value) => (OptionStream<TValue>)base.Error(value);

	/// <inheritdoc />
	public override OptionStream<TValue> Error(IError error) => (OptionStream<TValue>)base.Error(error);

	/// <inheritdoc />
	public override OptionStream<TValue> Next(IOption<TValue> option)
	{
		if (option is IError error)
		{
			ErrorReceived?.Invoke(this, error);
		}
		return (OptionStream<TValue>)base.Next(option);
	}

	/// <inheritdoc />
	public override OptionStream<TValue> Next(params IOption<TValue>[] options) =>
		(OptionStream<TValue>)base.Next(options);

	/// <inheritdoc />
	public override OptionStream<TValue> Next(IOptionEnumerable<TValue> options) =>
		(OptionStream<TValue>)base.Next(options);

	/// <inheritdoc />
	public override OptionStream<TValue> None() => Next(None<TValue>());

	/// <inheritdoc />
	public override OptionStream<TValue> RegisterCancellationToken(CancellationToken cancellationToken) =>
		(OptionStream<TValue>)base.RegisterCancellationToken(cancellationToken);

	/// <inheritdoc />
	public override OptionStream<TValue> Some(ISome<TValue> some) => (OptionStream<TValue>)base.Some(some);

	/// <inheritdoc />
	public override OptionStream<TValue> Some(TValue value) =>
		(OptionStream<TValue>)base.Next(IOption<TValue>.Some(value));

	/// <inheritdoc />
	public override OptionStream<TValue> UnregisterCancellationToken(CancellationToken cancellationToken) =>
		(OptionStream<TValue>)base.UnregisterCancellationToken(cancellationToken);
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
	public event EventHandler<IEnd>? EndReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="IError{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IError<TError>>? ErrorReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="INone" /> is added to the stream.
	/// </summary>
	public event EventHandler<INone>? NoneReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="IOption{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<IOption<TValue>>? OptionReceived;

	/// <summary>
	/// An event triggered when an option of type <see cref="ISome{}" /> is added to the stream.
	/// </summary>
	public event EventHandler<ISome<TValue>>? SomeReceived;

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
	public virtual ReadOnlyOptionStream<TValue, TError> AsReadOnly() => new(this);

	/// <summary>
	/// A chainable call to add an option of <see cref="IEnd" /> to the stream, returning this class instance.
	/// </summary>
	/// <returns>This class instance.</returns>
	public virtual OptionStream<TValue, TError> End() => Next(End<TValue>());

	/// <summary>
	/// A chainable call to add an option of <see cref="IError" /> to the stream, returning this class instance.
	/// </summary>
	/// <param name="message">The error message.</param>
	/// <param name="innerException">An optional inner exception.</param>
	/// <returns>This class instance.</returns>
	public virtual OptionStream<TValue, TError> Error(string message, Exception? innerException = null) =>
		Next(IOption<TValue>.Error(message, innerException));

	/// <summary>
	/// A chainable call to add an option of <see cref="IError" /> to the stream, returning this class instance.
	/// </summary>
	/// <param name="value">The value of an option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public virtual OptionStream<TValue, TError> Error(TError value) => Next(IOption<TValue>.Error(value));

	/// <summary>
	/// A chainable call to add an option of <see cref="IError" /> to the stream, returning this class instance.
	/// </summary>
	/// <param name="error">The option to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public virtual OptionStream<TValue, TError> Error(IError error)
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
	public virtual OptionStream<TValue, TError> Next(IOption<TValue> option)
	{
		OptionReceived?.Invoke(this, option);
		switch (option)
		{
			case IEnd end:
			{
				EndReceived?.Invoke(this, end);
				if (!TaskCompletionSource.Task.IsCompleted)
				{
					TaskCompletionSource.SetResult(end);
				}
				break;
			}
			case IError<TError> error:
			{
				ErrorReceived?.Invoke(this, error);
				break;
			}
			case INone none:
			{
				NoneReceived?.Invoke(this, none);
				break;
			}
			case ISome<TValue> some:
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
	/// <see cref="IEnd" /> then <see cref="EndOfStream" /> will be resolved.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public virtual OptionStream<TValue, TError> Next(params IOption<TValue>[] options) =>
		Next(new OptionList<TValue>(options));

	/// <summary>
	/// A chainable call to add multiple options of <see cref="IOption{}" /> to the stream. The options are iterated
	/// over and added to the stream one at a time. Then returning this class instance. If any of the options is of type
	/// <see cref="IEnd" /> then <see cref="EndOfStream" /> will be resolved.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <returns>This class instance.</returns>
	public virtual OptionStream<TValue, TError> Next(IOptionEnumerable<TValue> options)
	{
		options.ForEach(Next);
		return this;
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="INone" /> to the stream, returning this class instance.
	/// </summary>
	/// <returns>This class instance.</returns>
	public virtual OptionStream<TValue, TError> None() => Next(None<TValue>());

	/// <summary>
	/// A chainable call to register a cancellation token with the stream, then returning this class instance. The task
	/// for <see cref="EndOfStream" /> will be cancelled upon the cancellation of the provided token. If the provided
	/// token is equal to <see cref="CancellationToken.None" /> then nothing will be registered.
	/// </summary>
	/// <param name="cancellationToken">A cancellation token to register with the stream.</param>
	/// <returns>This class instance.</returns>
	public virtual OptionStream<TValue, TError> RegisterCancellationToken(CancellationToken cancellationToken)
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
	public virtual OptionStream<TValue, TError> Some(ISome<TValue> some)
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
	public virtual OptionStream<TValue, TError> Some(TValue value) => Next(IOption<TValue>.Some(value));

	/// <summary>
	/// A chainable call to unregister a cancellation token with the stream, then returning this class instance. If the
	/// provided token is not registered then nothing will be unregistered.
	/// </summary>
	/// <param name="cancellationToken">A cancellation token to unregister from the stream.</param>
	/// <returns>This class instance.</returns>
	public virtual OptionStream<TValue, TError> UnregisterCancellationToken(CancellationToken cancellationToken)
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
