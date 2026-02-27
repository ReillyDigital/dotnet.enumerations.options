namespace ReillyDigital.Enumerations.Options.Boxed;

using System.Collections.Generic;
using System.Runtime.CompilerServices;

/// <summary>
/// Represents a stream of options with a value of <see cref="TValue" /> that are accessed by
/// subscribing to events of each possible option type, triggered when an item of that type is
/// added to the stream. Errors are of type <see cref="string" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public sealed class BoxedOptionStream<TValue> : IVoid
{
	/// <summary>
	/// The current option from the stream.
	/// </summary>
	public IOption<TValue>? Current { get; private set; }

	/// <summary>
	/// Whether the stream has ended.
	/// </summary>
	public bool IsEnded { get; private set; }

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
	/// Constructor for this stream.
	/// </summary>
	/// <param name="bufferSize">The size of the buffer.</param>
	public BoxedOptionStream(int bufferSize = 0)
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
	/// <returns>A new <see cref="ReadOnlyBoxedOptionStream{TValue}" /> wrapping this stream.</returns>
	public ReadOnlyBoxedOptionStream<TValue> AsReadOnly() => new(this);

	/// <summary>
	/// Signals the end of the stream.
	/// </summary>
	public void End() => IsEnded = true;

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue}" /> to the stream.
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
			IOption<TValue>.Error(error.Value, error.IgnoredErrors),
			cancellationToken: cancellationToken
		);
	}

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
		string value,
		IEnumerable<string>? ignoredErrors = null,
		CancellationToken cancellationToken = default
	) => await Next(
		IOption<TValue>.Error(value, ignoredErrors),
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
	public async Task Next(IOption<TValue> option, CancellationToken cancellationToken = default)
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
		IOptionEnumerable<TValue> options, CancellationToken cancellationToken = default
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
		IAsyncOptionEnumerable<TValue> options,
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
		CancellationToken cancellationToken = default, params IOption<TValue>[] options
	)
	{
		foreach (var option in options)
		{
			await Next(option, cancellationToken);
		}
	}

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
		IEnumerable<string>? ignoredErrors = null, CancellationToken cancellationToken = default
	) => await Next(IOption<TValue>.None(ignoredErrors), cancellationToken: cancellationToken);

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
	public async Task<IOption<TValue>> Read(CancellationToken cancellationToken = default)
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
			IOption<TValue>? next;
			while (!Buffer.TryDequeue(out next))
			{
				if (IsEnded && Buffer.Count == 0)
				{
					throw new Exception("Stream has ended with no items to read.");
				}
				await Task.Delay(100, cancellationToken);
			}
			Current = next;
		}
		if (Current is null)
		{
			throw new InvalidOperationException("Current option cannot be null.");
		}
		return Current;
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
	public async IAsyncEnumerable<IOption<TValue>> ReadToEnd(
		bool shouldSkipErrors = false,
		[EnumeratorCancellation] CancellationToken cancellationToken = default
	)
	{
		while (!IsEnded || (Buffer is not null && Buffer.Count > 0) || UnbufferedNext is not null)
		{
			var next = await Read(cancellationToken: cancellationToken);
			if (next is IError<TValue>)
			{
				if (!shouldSkipErrors)
				{
					yield return next;
				}
			}
			else if (next is ISome<TValue>)
			{
				yield return next;
			}
		}
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue}" /> to the stream.
	/// </summary>
	/// <param name="some">The option to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Some(ISome<TValue> some, CancellationToken cancellationToken = default)
	{
		if (some is IOption<TValue> option)
		{
			await Next(option, cancellationToken: cancellationToken);
			return;
		}
		await Next(
			IOption<TValue>.Some(some.Value, some.IgnoredErrors),
			cancellationToken: cancellationToken
		);
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
		IEnumerable<string>? ignoredErrors = null,
		CancellationToken cancellationToken = default
	) => await Next(
		IOption<TValue>.Some(value, ignoredErrors), cancellationToken: cancellationToken
	);
}

/// <summary>
/// Represents a stream of options with a value of <see cref="TValue" /> that are accessed by
/// subscribing to events of each possible option type, triggered when an item of that type is
/// added to the stream. Errors are of type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the errors.</typeparam>
public sealed class BoxedOptionStream<TValue, TError> : IVoid<TError>
{
	/// <summary>
	/// The current option from the stream.
	/// </summary>
	public IOption<TValue, TError>? Current { get; private set; }

	/// <summary>
	/// Whether the stream has ended.
	/// </summary>
	public bool IsEnded { get; private set; }

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
	/// Constructor for this stream.
	/// </summary>
	/// <param name="bufferSize">The size of the buffer.</param>
	public BoxedOptionStream(int bufferSize = 0)
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
	/// A new <see cref="ReadOnlyBoxedOptionStream{TValue, TError}" /> wrapping this stream.
	/// </returns>
	public ReadOnlyBoxedOptionStream<TValue, TError> AsReadOnly() => new(this);

	/// <summary>
	/// Signals the end of the stream.
	/// </summary>
	public void End() => IsEnded = true;

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue, TError}" /> to the stream.
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
		TError value,
		IEnumerable<TError>? ignoredErrors = null,
		CancellationToken cancellationToken = default
	) => await Next(
		IOption<TValue, TError>.Error(value, ignoredErrors),
		cancellationToken: cancellationToken
	);

	/// <summary>
	/// A chainable call to add an option of <see cref="IError{TValue, TError}" /> to the stream.
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
			IOption<TValue, TError>.Error(error.Value, error.IgnoredErrors),
			cancellationToken: cancellationToken
		);
	}

	/// <summary>
	/// A chainable call to add an option to the stream.
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
		IOptionEnumerable<TValue, TError> options,
		CancellationToken cancellationToken = default
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
		IAsyncOptionEnumerable<TValue, TError> options,
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
		CancellationToken cancellationToken = default,
		params IOption<TValue, TError>[] options
	)
	{
		foreach (var option in options)
		{
			await Next(option, cancellationToken);
		}
	}

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
		IEnumerable<TError>? ignoredErrors = null,
		CancellationToken cancellationToken = default
	) => await Next(
		IOption<TValue, TError>.None(ignoredErrors), cancellationToken: cancellationToken
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
	public async Task<IOption<TValue, TError>> Read(
		CancellationToken cancellationToken = default
	)
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
			IOption<TValue, TError>? next;
			while (!Buffer.TryDequeue(out next))
			{
				if (IsEnded && Buffer.Count == 0)
				{
					throw new Exception("Stream has ended with no items to read.");
				}
				await Task.Delay(100, cancellationToken);
			}
			Current = next;
		}
		if (Current is null)
		{
			throw new InvalidOperationException("Current option cannot be null.");
		}
		return Current;
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
	public async IAsyncEnumerable<IOption<TValue, TError>> ReadToEnd(
		bool shouldSkipErrors = false,
		[EnumeratorCancellation] CancellationToken cancellationToken = default
	)
	{
		while (!IsEnded || (Buffer is not null && Buffer.Count > 0) || UnbufferedNext is not null)
		{
			var next = await Read(cancellationToken: cancellationToken);
			if (next is IError<TValue, TError>)
			{
				if (!shouldSkipErrors)
				{
					yield return next;
				}
			}
			else if (next is ISome<TValue>)
			{
				yield return next;
			}
		}
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue, TError}" /> to the stream.
	/// </summary>
	/// <param name="some">The option to add to the stream.</param>
	/// <param name="cancellationToken">
	/// A cancellation token to observe while waiting for the task to complete.
	/// </param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task Some(
		ISome<TValue, TError> some, CancellationToken cancellationToken = default
	)
	{
		if (some is IOption<TValue, TError> option)
		{
			await Next(option, cancellationToken: cancellationToken);
			return;
		}
		await Next(
			IOption<TValue, TError>.Some(some.Value, some.IgnoredErrors),
			cancellationToken: cancellationToken
		);
	}

	/// <summary>
	/// A chainable call to add an option of <see cref="ISome{TValue, TError}" /> to the stream.
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
		IOption<TValue, TError>.Some(value, ignoredErrors),
		cancellationToken: cancellationToken
	);
}
