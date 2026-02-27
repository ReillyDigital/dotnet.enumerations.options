namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a collection of options with a value of <see cref="TValue" />. Errors will be of
/// type <see cref="string" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public readonly struct AsyncOptionEnumerable<TValue> : IAsyncEnumerable<Option<TValue>>
{
	/// <summary>
	/// Create an <see cref="AsyncOptionEnumerable{TValue}" /> in error state.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An <see cref="AsyncOptionEnumerable{TValue}" /> in error state.</returns>
	public static AsyncOptionEnumerable<TValue> Error(
		IEnumerable<string>? ignoredErrors = null
	) => new("", ignoredErrors);

	/// <summary>
	/// Create an <see cref="AsyncOptionEnumerable{TValue}" /> in error state.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An <see cref="AsyncOptionEnumerable{TValue}" /> in error state.</returns>
	public static AsyncOptionEnumerable<TValue> Error(
		string value, IEnumerable<string>? ignoredErrors = null
	) => new(value, ignoredErrors);

	/// <summary>
	/// The error value of this option enumerable when in error state.
	/// </summary>
	public string ErrorValue { get; }

	/// <summary>
	/// Errors that are ignored instead of being returned as the option value.
	/// </summary>
	public IEnumerable<string> IgnoredErrors { get; }

	/// <summary>
	/// Whether this instance is in error state. When true, enumeration throws.
	/// </summary>
	public bool IsError => Enumerable is null;

	/// <summary>
	/// The inner async enumerable when not in error state.
	/// </summary>
	private IAsyncEnumerable<Option<TValue>>? Enumerable { get; }

	/// <summary>
	/// Constructor for enumerable state.
	/// </summary>
	/// <param name="enumerable">The inner async enumerable.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	public AsyncOptionEnumerable(
		IAsyncEnumerable<Option<TValue>> enumerable,
		IEnumerable<string>? ignoredErrors = null
	)
	{
		Enumerable = enumerable;
		ErrorValue = "";
		IgnoredErrors = ignoredErrors ?? [];
	}

	/// <summary>
	/// Constructor for error state.
	/// </summary>
	/// <param name="errorValue">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	private AsyncOptionEnumerable(string errorValue, IEnumerable<string>? ignoredErrors)
	{
		Enumerable = null;
		ErrorValue = errorValue;
		IgnoredErrors = ignoredErrors ?? [];
	}

	/// <summary>
	/// Returns the collection as an <see cref="IAsyncEnumerable{Option{TValue}}" />.
	/// </summary>
	/// <returns>An <see cref="IAsyncEnumerable{Option{TValue}}" />.</returns>
	public IAsyncEnumerable<Option<TValue>> AsAsyncEnumerable()
		=> Enumerable ?? throw new Exception(ErrorValue ?? "Error");

	/// <summary>
	/// Iterates over each item in the collection, calling the param <paramref name="handler" />
	/// on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	public async Task ForEach(
		Action<Option<TValue>> handler,
		CancellationToken cancellationToken = default
	)
	{
		if (Enumerable is null)
		{
			throw new Exception(ErrorValue ?? "Error");
		}
		await foreach (var item in Enumerable.WithCancellation(cancellationToken))
		{
			handler(item);
		}
	}

	/// <summary>
	/// Iterates over each item in the collection, calling the param <paramref name="handler" />
	/// on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	public async Task ForEach<TResult>(
		Func<Option<TValue>, TResult> handler,
		CancellationToken cancellationToken = default
	)
	{
		if (Enumerable is null)
		{
			throw new Exception(ErrorValue ?? "Error");
		}
		await foreach (var item in Enumerable.WithCancellation(cancellationToken))
		{
			handler(item);
		}
	}

	/// <summary>
	/// Returns an async enumerator for the collection.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>An <see cref="IAsyncEnumerator{Option{TValue}}" />.</returns>
	public IAsyncEnumerator<Option<TValue>> GetAsyncEnumerator(
		CancellationToken cancellationToken = default
	) => (Enumerable ?? throw new Exception(ErrorValue ?? "Error"))
		.GetAsyncEnumerator(cancellationToken);

	/// <summary>
	/// Convert this async enumerable to an <see cref="AsyncBoxedOptionEnumerable{TValue}" />.
	/// </summary>
	/// <returns>An <see cref="AsyncBoxedOptionEnumerable{TValue}" />.</returns>
	public AsyncBoxedOptionEnumerable<TValue> ToBoxed()
		=> IsError
			? AsyncBoxedOptionEnumerable<TValue>.Error(ErrorValue, IgnoredErrors)
			: new(ToBoxedAsync(), IgnoredErrors);

	/// <summary>
	/// Converts the async enumerable items to boxed options.
	/// </summary>
	/// <returns>An <see cref="IAsyncEnumerable{IOption{TValue}}" />.</returns>
	private async IAsyncEnumerable<IOption<TValue>> ToBoxedAsync()
	{
		await foreach (var item in Enumerable!)
		{
			yield return item.ToBoxed();
		}
	}
}

/// <summary>
/// Represents a collection of options with a value of <see cref="TValue" />. Errors will be of
/// type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public readonly struct AsyncOptionEnumerable<TValue, TError>
	: IAsyncEnumerable<Option<TValue, TError>>
{
	/// <summary>
	/// Create an <see cref="AsyncOptionEnumerable{TValue, TError}" /> in error state.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An <see cref="AsyncOptionEnumerable{TValue, TError}" /> in error state.</returns>
	public static AsyncOptionEnumerable<TValue, TError> Error(
		TError value, IEnumerable<TError>? ignoredErrors = null
	) => new(value, ignoredErrors);

	/// <summary>
	/// The error value of this option enumerable when in error state.
	/// </summary>
	public TError? ErrorValue { get; }

	/// <summary>
	/// Errors that are ignored instead of being returned as the option value.
	/// </summary>
	public IEnumerable<TError> IgnoredErrors { get; }

	/// <summary>
	/// Whether this instance is in error state. When true, enumeration throws.
	/// </summary>
	public bool IsError => Enumerable is null;

	/// <summary>
	/// The inner async enumerable when not in error state.
	/// </summary>
	private IAsyncEnumerable<Option<TValue, TError>>? Enumerable { get; }

	/// <summary>
	/// Constructor for enumerable state.
	/// </summary>
	/// <param name="enumerable">The inner async enumerable.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	public AsyncOptionEnumerable(
		IAsyncEnumerable<Option<TValue, TError>> enumerable,
		IEnumerable<TError>? ignoredErrors = null
	)
	{
		Enumerable = enumerable;
		ErrorValue = default;
		IgnoredErrors = ignoredErrors ?? [];
	}

	/// <summary>
	/// Constructor for error state.
	/// </summary>
	/// <param name="errorValue">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	private AsyncOptionEnumerable(TError? errorValue, IEnumerable<TError>? ignoredErrors)
	{
		Enumerable = null;
		ErrorValue = errorValue;
		IgnoredErrors = ignoredErrors ?? [];
	}

	/// <summary>
	/// Returns the collection as an <see cref="IAsyncEnumerable{Option{TValue, TError}}" />.
	/// </summary>
	/// <returns>An <see cref="IAsyncEnumerable{Option{TValue, TError}}" />.</returns>
	public IAsyncEnumerable<Option<TValue, TError>> AsAsyncEnumerable()
		=> Enumerable ?? throw new Exception(ErrorValue?.ToString() ?? "Error");

	/// <summary>
	/// Iterates over each item in the collection, calling the param <paramref name="handler" />
	/// on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	public async Task ForEach(
		Action<Option<TValue, TError>> handler,
		CancellationToken cancellationToken = default
	)
	{
		if (Enumerable is null)
		{
			throw new Exception(ErrorValue?.ToString() ?? "Error");
		}
		await foreach (var item in Enumerable.WithCancellation(cancellationToken))
		{
			handler(item);
		}
	}

	/// <summary>
	/// Iterates over each item in the collection, calling the param <paramref name="handler" />
	/// on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	public async Task ForEach<TResult>(
		Func<Option<TValue, TError>, TResult> handler,
		CancellationToken cancellationToken = default
	)
	{
		if (Enumerable is null)
		{
			throw new Exception(ErrorValue?.ToString() ?? "Error");
		}
		await foreach (var item in Enumerable.WithCancellation(cancellationToken))
		{
			handler(item);
		}
	}

	/// <summary>
	/// Returns an async enumerator for the collection.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>An <see cref="IAsyncEnumerator{Option{TValue, TError}}" />.</returns>
	public IAsyncEnumerator<Option<TValue, TError>> GetAsyncEnumerator(
		CancellationToken cancellationToken = default
	) => (Enumerable ?? throw new Exception(ErrorValue?.ToString() ?? "Error"))
		.GetAsyncEnumerator(cancellationToken);

	/// <summary>
	/// Convert this async enumerable to an
	/// <see cref="AsyncBoxedOptionEnumerable{TValue, TError}" />.
	/// </summary>
	/// <returns>An <see cref="AsyncBoxedOptionEnumerable{TValue, TError}" />.</returns>
	public AsyncBoxedOptionEnumerable<TValue, TError> ToBoxed()
		=> IsError
			? AsyncBoxedOptionEnumerable<TValue, TError>.Error(ErrorValue!, IgnoredErrors)
			: new(ToBoxedAsync(), IgnoredErrors);

	/// <summary>
	/// Converts the async enumerable items to boxed options.
	/// </summary>
	/// <returns>An <see cref="IAsyncEnumerable{IOption{TValue, TError}}" />.</returns>
	private async IAsyncEnumerable<IOption<TValue, TError>> ToBoxedAsync()
	{
		await foreach (var item in Enumerable!)
		{
			yield return item.ToBoxed();
		}
	}
}
