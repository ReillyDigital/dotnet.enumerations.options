namespace ReillyDigital.Enumerations.Options;

using System.Runtime.CompilerServices;

/// <summary>
/// Represents a stream of options with a value of <see cref="TValue" /> that are accessed by
/// subscribing to events of each possible option type, triggered when an item of that type is
/// added to the stream. Errors are of type <see cref="string" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public sealed class OptionStream<TValue> : IVoid
{
	/// <summary>
	/// The current option from the stream.
	/// </summary>
	public Option<TValue>? Current { get; private set; }

	/// <summary>
	/// Whether the stream has ended.
	/// </summary>
	public bool IsEnded { get; private set; }

	/// <summary>
	/// Buffer used to temporarily hold items of the stream until they are read.
	/// </summary>
	private Queue<Option<TValue>>? Buffer { get; }

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
	private Option<TValue>? UnbufferedNext { get; set; }

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
	/// Signals the end of the stream.
	/// </summary>
	public void End() => IsEnded = true;

	/// <summary>
	/// A chainable call to add an option of Error to the stream.
	/// </summary>
	/// <param name="error">The option to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Error(IError error, CancellationToken cancellationToken = default)
		=> await Next(
			Option<TValue>.Error(error.Value, error.IgnoredErrors),
			cancellationToken: cancellationToken
		);

	/// <summary>
	/// A chainable call to add an option of Error to the stream.
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
		string? value,
		IEnumerable<string?>? ignoredErrors = null,
		CancellationToken cancellationToken = default
	) => await Next(Option<TValue>.Error(value, ignoredErrors), cancellationToken: cancellationToken);

	/// <summary>
	/// A chainable call to add an option to the stream.
	/// </summary>
	/// <param name="option">The option to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Next(Option<TValue> option, CancellationToken cancellationToken = default)
	{
		await BufferLock.WaitAsync(cancellationToken);
		if (Buffer is null)
		{
			while (UnbufferedNext is not null)
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
	/// A chainable call to add multiple options to the stream. The options are iterated over and
	/// added to the stream one at a time.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Next(
		IEnumerable<Option<TValue>> options, CancellationToken cancellationToken = default
	)
	{
		foreach (var option in options)
		{
			await Next(option, cancellationToken: cancellationToken);
		}
	}

	/// <summary>
	/// A chainable call to add multiple options to the stream. The options are iterated over and
	/// added to the stream one at a time.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Next(
		IAsyncEnumerable<Option<TValue>> options, CancellationToken cancellationToken = default
	)
	{
		await foreach (var option in options)
		{
			await Next(option, cancellationToken: cancellationToken);
		}
	}

	/// <summary>
	/// A chainable call to add multiple options to the stream. The options are iterated over and
	/// added to the stream one at a time.
	/// </summary>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <param name="options">The options to add to the stream.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Next(
		CancellationToken cancellationToken = default, params Option<TValue>[] options
	) => await Next((IEnumerable<Option<TValue>>)options, cancellationToken: cancellationToken);

	/// <summary>
	/// A chainable call to add an option of None to the stream.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task None(
		IEnumerable<string?>? ignoredErrors = null, CancellationToken cancellationToken = default
	) => await Next(Option<TValue>.None(ignoredErrors), cancellationToken: cancellationToken);

	/// <summary>
	/// Reads the next option from the stream. An <see cref="Exception" /> will be thrown if the
	/// stream has ended and no buffered items remain.
	/// </summary>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>
	/// A task representing the asynchronous operation. The result value is the next option from
	/// the stream.
	/// </returns>
	/// <exception cref="Exception">
	/// Thrown when reading after the stream has ended with no buffered items.
	/// </exception>
	public async Task<Option<TValue>> Read(CancellationToken cancellationToken = default)
	{
		if (Buffer is null)
		{
			while (UnbufferedNext is null)
			{
				if (IsEnded)
				{
					throw new Exception("Stream has ended with no items to read.");
				}
				await Task.Delay(100, cancellationToken);
			}
			Current = UnbufferedNext;
			UnbufferedNext = null;
		}
		else
		{
			Option<TValue> next;
			while (!Buffer.TryDequeue(out next) || next.Type == default)
			{
				if (IsEnded && Buffer.Count == 0)
				{
					throw new Exception("Stream has ended with no items to read.");
				}
				await Task.Delay(100, cancellationToken);
			}
			Current = next;
		}
		return Current.Value;
	}

	/// <summary>
	/// Reads all options from the stream until the stream has ended.
	/// </summary>
	/// <param name="shouldSkipErrors">Whether to skip error options.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>
	/// An enumeration of options from the stream until the end of the stream is reached.
	/// </returns>
	public async IAsyncEnumerable<Option<TValue>> ReadToEnd(
		bool shouldSkipErrors = false,
		[EnumeratorCancellation] CancellationToken cancellationToken = default
	)
	{
		while (!IsEnded || (Buffer is not null && Buffer.Count > 0) || UnbufferedNext is not null)
		{
			var next = await Read(cancellationToken: cancellationToken);
			switch (next.Type)
			{
				case OptionType.Error:
					if (shouldSkipErrors)
					{
						continue;
					}
					yield return next;
					break;
				case OptionType.Some:
					yield return next;
					break;
				default:
					continue;
			}
		}
	}

	/// <summary>
	/// A chainable call to add an option of Some to the stream.
	/// </summary>
	/// <param name="some">The option to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Some(ISome<TValue> some, CancellationToken cancellationToken = default)
		=> await Next(
			Option<TValue>.Some(some.Value, some.IgnoredErrors),
			cancellationToken: cancellationToken
		);

	/// <summary>
	/// A chainable call to add an option of Some to the stream.
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
		IEnumerable<string?>? ignoredErrors = null,
		CancellationToken cancellationToken = default
	) => await Next(Option<TValue>.Some(value, ignoredErrors), cancellationToken: cancellationToken);
}

/// <summary>
/// Represents a stream of options with a value of <see cref="TValue" /> that are accessed by
/// subscribing to events of each possible option type, triggered when an item of that type is
/// added to the stream. Errors are of type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the errors.</typeparam>
public sealed class OptionStream<TValue, TError> : IVoid<TError>
{
	/// <summary>
	/// The current option from the stream.
	/// </summary>
	public Option<TValue, TError>? Current { get; private set; }

	/// <summary>
	/// Whether the stream has ended.
	/// </summary>
	public bool IsEnded { get; private set; }

	/// <summary>
	/// Buffer used to temporarily hold items of the stream until they are read.
	/// </summary>
	private Queue<Option<TValue, TError>>? Buffer { get; }

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
	private Option<TValue, TError>? UnbufferedNext { get; set; }

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
	/// Signals the end of the stream.
	/// </summary>
	public void End() => IsEnded = true;

	/// <summary>
	/// A chainable call to add an option of Error to the stream.
	/// </summary>
	/// <param name="error">The error to add to the stream.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Error(
		TError error,
		IEnumerable<TError>? ignoredErrors = null,
		CancellationToken cancellationToken = default
	) => await Next(
		Option<TValue, TError>.Error(error, ignoredErrors),
		cancellationToken: cancellationToken
	);

	/// <summary>
	/// A chainable call to add an option to the stream.
	/// </summary>
	/// <param name="option">The option to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Next(
		Option<TValue, TError> option, CancellationToken cancellationToken = default
	)
	{
		await BufferLock.WaitAsync(cancellationToken);
		if (Buffer is null)
		{
			while (UnbufferedNext is not null)
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
	/// A chainable call to add multiple options to the stream. The options are iterated over and
	/// added to the stream one at a time.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Next(
		IEnumerable<Option<TValue, TError>> options, CancellationToken cancellationToken = default
	)
	{
		foreach (var option in options)
		{
			await Next(option, cancellationToken: cancellationToken);
		}
	}

	/// <summary>
	/// A chainable call to add multiple options to the stream. The options are iterated over and
	/// added to the stream one at a time.
	/// </summary>
	/// <param name="options">The options to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Next(
		IAsyncEnumerable<Option<TValue, TError>> options,
		CancellationToken cancellationToken = default
	)
	{
		await foreach (var option in options)
		{
			await Next(option, cancellationToken: cancellationToken);
		}
	}

	/// <summary>
	/// A chainable call to add multiple options to the stream. The options are iterated over and
	/// added to the stream one at a time.
	/// </summary>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <param name="options">The options to add to the stream.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Next(
		CancellationToken cancellationToken = default, params Option<TValue, TError>[] options
	) => await Next(
		(IEnumerable<Option<TValue, TError>>)options, cancellationToken: cancellationToken
	);

	/// <summary>
	/// A chainable call to add an option of None to the stream.
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
		Option<TValue, TError>.None(ignoredErrors), cancellationToken: cancellationToken
	);

	/// <summary>
	/// Reads the next option from the stream. An <see cref="Exception" /> will be thrown if the
	/// stream has ended and no buffered items remain.
	/// </summary>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>
	/// A task representing the asynchronous operation. The result value is the next option from
	/// the stream.
	/// </returns>
	/// <exception cref="Exception">
	/// Thrown when reading after the stream has ended with no buffered items.
	/// </exception>
	public async Task<Option<TValue, TError>> Read(CancellationToken cancellationToken = default)
	{
		if (Buffer is null)
		{
			while (UnbufferedNext is null)
			{
				if (IsEnded)
				{
					throw new Exception("Stream has ended with no items to read.");
				}
				await Task.Delay(100, cancellationToken);
			}
			Current = UnbufferedNext;
			UnbufferedNext = null;
		}
		else
		{
			Option<TValue, TError> next;
			while (!Buffer.TryDequeue(out next) || next.Type == default)
			{
				if (IsEnded && Buffer.Count == 0)
				{
					throw new Exception("Stream has ended with no items to read.");
				}
				await Task.Delay(100, cancellationToken);
			}
			Current = next;
		}
		return Current.Value;
	}

	/// <summary>
	/// Reads all options from the stream until the stream has ended.
	/// </summary>
	/// <param name="shouldSkipErrors">Whether to skip error options.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>
	/// An enumeration of options from the stream until the end of the stream is reached.
	/// </returns>
	public async IAsyncEnumerable<Option<TValue, TError>> ReadToEnd(
		bool shouldSkipErrors = false,
		[EnumeratorCancellation] CancellationToken cancellationToken = default
	)
	{
		while (!IsEnded || (Buffer is not null && Buffer.Count > 0) || UnbufferedNext is not null)
		{
			var next = await Read(cancellationToken: cancellationToken);
			switch (next.Type)
			{
				case OptionType.Error:
					if (shouldSkipErrors)
					{
						continue;
					}
					yield return next;
					break;
				case OptionType.Some:
					yield return next;
					break;
				default:
					continue;
			}
		}
	}

	/// <summary>
	/// A chainable call to add an option of Some to the stream.
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
		Option<TValue, TError>.Some(value, ignoredErrors), cancellationToken: cancellationToken
	);
}
