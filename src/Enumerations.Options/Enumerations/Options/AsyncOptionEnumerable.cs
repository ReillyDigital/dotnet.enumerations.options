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
	/// Backing field for the inner async enumerable when not in error state.
	/// </summary>
	private readonly IAsyncEnumerable<Option<TValue>>? _Enumerable;

	/// <summary>
	/// Backing field for the error value when in error state.
	/// </summary>
	private readonly string _ErrorValue;

	/// <summary>
	/// Backing field for errors that are ignored instead of being returned as the option value.
	/// </summary>
	private readonly IEnumerable<string>? _IgnoredErrors;

	/// <summary>
	/// The error value of this option enumerable when in error state.
	/// </summary>
	public string ErrorValue => _ErrorValue;

	/// <summary>
	/// Errors that are ignored instead of being returned as the option value.
	/// </summary>
	public IEnumerable<string> IgnoredErrors => _IgnoredErrors ?? [];

	/// <summary>
	/// Whether this instance is in error state. When true, enumeration throws.
	/// </summary>
	public bool IsError => _Enumerable is null;

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
		_Enumerable = enumerable;
		_ErrorValue = "";
		_IgnoredErrors = ignoredErrors ?? [];
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
		_Enumerable = null;
		_ErrorValue = errorValue;
		_IgnoredErrors = ignoredErrors ?? [];
	}

	/// <summary>
	/// Returns the collection as an <see cref="IAsyncEnumerable{Option{TValue}}" />.
	/// </summary>
	/// <returns>An <see cref="IAsyncEnumerable{Option{TValue}}" />.</returns>
	public IAsyncEnumerable<Option<TValue>> AsAsyncEnumerable()
		=> _Enumerable ?? throw new Exception(_ErrorValue ?? "Error");

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
		if (_Enumerable is null)
		{
			throw new Exception(_ErrorValue ?? "Error");
		}
		await foreach (var item in _Enumerable.WithCancellation(cancellationToken))
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
		if (_Enumerable is null)
		{
			throw new Exception(_ErrorValue ?? "Error");
		}
		await foreach (var item in _Enumerable.WithCancellation(cancellationToken))
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
	) => (_Enumerable ?? throw new Exception(_ErrorValue ?? "Error"))
		.GetAsyncEnumerator(cancellationToken);
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
	/// Backing field for the inner async enumerable when not in error state.
	/// </summary>
	private readonly IAsyncEnumerable<Option<TValue, TError>>? _Enumerable;

	/// <summary>
	/// Backing field for the error value when in error state.
	/// </summary>
	private readonly TError? _ErrorValue;

	/// <summary>
	/// Backing field for errors that are ignored instead of being returned as the option value.
	/// </summary>
	private readonly IEnumerable<TError>? _IgnoredErrors;

	/// <summary>
	/// The error value of this option enumerable when in error state.
	/// </summary>
	public TError? ErrorValue => _ErrorValue;

	/// <summary>
	/// Errors that are ignored instead of being returned as the option value.
	/// </summary>
	public IEnumerable<TError> IgnoredErrors => _IgnoredErrors ?? [];

	/// <summary>
	/// Whether this instance is in error state. When true, enumeration throws.
	/// </summary>
	public bool IsError => _Enumerable is null;

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
		_Enumerable = enumerable;
		_ErrorValue = default;
		_IgnoredErrors = ignoredErrors ?? [];
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
		_Enumerable = null;
		_ErrorValue = errorValue;
		_IgnoredErrors = ignoredErrors ?? [];
	}

	/// <summary>
	/// Returns the collection as an <see cref="IAsyncEnumerable{Option{TValue, TError}}" />.
	/// </summary>
	/// <returns>An <see cref="IAsyncEnumerable{Option{TValue, TError}}" />.</returns>
	public IAsyncEnumerable<Option<TValue, TError>> AsAsyncEnumerable()
		=> _Enumerable ?? throw new Exception(_ErrorValue?.ToString() ?? "Error");

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
		if (_Enumerable is null)
		{
			throw new Exception(_ErrorValue?.ToString() ?? "Error");
		}
		await foreach (var item in _Enumerable.WithCancellation(cancellationToken))
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
		if (_Enumerable is null)
		{
			throw new Exception(_ErrorValue?.ToString() ?? "Error");
		}
		await foreach (var item in _Enumerable.WithCancellation(cancellationToken))
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
	) => (_Enumerable ?? throw new Exception(_ErrorValue?.ToString() ?? "Error"))
		.GetAsyncEnumerator(cancellationToken);
}
