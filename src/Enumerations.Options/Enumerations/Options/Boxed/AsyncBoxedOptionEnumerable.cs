namespace ReillyDigital.Enumerations.Options.Boxed;

using System.Collections.Generic;

/// <summary>
/// Represents a collection of options with a value of <see cref="TValue" />. Errors will be of
/// type <see cref="string" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public readonly struct AsyncBoxedOptionEnumerable<TValue>
	: IAsyncOptionEnumerable<TValue>, IIgnoredErrorSet
{
	/// <summary>
	/// Create an <see cref="AsyncBoxedOptionEnumerable{TValue}" /> in error state.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An <see cref="AsyncBoxedOptionEnumerable{TValue}" /> in error state.</returns>
	public static AsyncBoxedOptionEnumerable<TValue> Error(
		IEnumerable<string>? ignoredErrors = null
	) => new("", ignoredErrors);

	/// <summary>
	/// Create an <see cref="AsyncBoxedOptionEnumerable{TValue}" /> in error state.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An <see cref="AsyncBoxedOptionEnumerable{TValue}" /> in error state.</returns>
	public static AsyncBoxedOptionEnumerable<TValue> Error(
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
	private IAsyncEnumerable<IOption<TValue>>? Enumerable { get; }

	/// <summary>
	/// Constructor for enumerable state.
	/// </summary>
	/// <param name="enumerable">The inner async enumerable.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	public AsyncBoxedOptionEnumerable(
		IAsyncEnumerable<IOption<TValue>> enumerable,
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
	private AsyncBoxedOptionEnumerable(string errorValue, IEnumerable<string>? ignoredErrors)
	{
		Enumerable = null;
		ErrorValue = errorValue;
		IgnoredErrors = ignoredErrors ?? [];
	}

	/// <summary>
	/// Returns the collection as an <see cref="IAsyncEnumerable{IOption{TValue}}" />.
	/// </summary>
	/// <returns>An <see cref="IAsyncEnumerable{IOption{TValue}}" />.</returns>
	public IAsyncEnumerable<IOption<TValue>> AsAsyncEnumerable()
		=> Enumerable ?? throw new Exception(ErrorValue ?? "Error");

	/// <summary>
	/// Iterates over each item in the collection, calling the param <paramref name="handler" />
	/// on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public async Task ForEach(Action<IOption<TValue>> handler)
	{
		if (Enumerable is null)
		{
			throw new Exception(ErrorValue ?? "Error");
		}
		await foreach (var item in Enumerable)
		{
			handler(item);
		}
	}

	/// <summary>
	/// Iterates over each item in the collection, calling the param <paramref name="handler" />
	/// on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public async Task ForEach<TResult>(Func<IOption<TValue>, TResult> handler)
	{
		if (Enumerable is null)
		{
			throw new Exception(ErrorValue ?? "Error");
		}
		await foreach (var item in Enumerable)
		{
			handler(item);
		}
	}

	/// <summary>
	/// Returns an async enumerator for the collection.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>An <see cref="IAsyncEnumerator{IOption{TValue}}" />.</returns>
	public IAsyncEnumerator<IOption<TValue>> GetAsyncEnumerator(
		CancellationToken cancellationToken = default
	) => (Enumerable ?? throw new Exception(ErrorValue ?? "Error"))
		.GetAsyncEnumerator(cancellationToken);

	/// <summary>
	/// Convert this async enumerable to an <see cref="AsyncOptionEnumerable{TValue}" />.
	/// </summary>
	/// <returns>An <see cref="AsyncOptionEnumerable{TValue}" />.</returns>
	public AsyncOptionEnumerable<TValue> ToUnboxed()
		=> IsError
			? AsyncOptionEnumerable<TValue>.Error(ErrorValue, IgnoredErrors)
			: new(ToUnboxedAsync(), IgnoredErrors);

	/// <summary>
	/// Converts the async enumerable items to unboxed options.
	/// </summary>
	/// <returns>An <see cref="IAsyncEnumerable{Option{TValue}}" />.</returns>
	private async IAsyncEnumerable<Option<TValue>> ToUnboxedAsync()
	{
		await foreach (var item in Enumerable!)
		{
			yield return Option<TValue>.Unbox(item);
		}
	}
}

/// <summary>
/// Represents a collection of options with a value of <see cref="TValue" />. Errors will be of
/// type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public readonly struct AsyncBoxedOptionEnumerable<TValue, TError>
	: IAsyncOptionEnumerable<TValue, TError>, IIgnoredErrorSet<TError>
{
	/// <summary>
	/// Create an <see cref="AsyncBoxedOptionEnumerable{TValue, TError}" /> in error state.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>
	/// An <see cref="AsyncBoxedOptionEnumerable{TValue, TError}" /> in error state.
	/// </returns>
	public static AsyncBoxedOptionEnumerable<TValue, TError> Error(
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
	private IAsyncEnumerable<IOption<TValue, TError>>? Enumerable { get; }

	/// <summary>
	/// Constructor for enumerable state.
	/// </summary>
	/// <param name="enumerable">The inner async enumerable.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	public AsyncBoxedOptionEnumerable(
		IAsyncEnumerable<IOption<TValue, TError>> enumerable,
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
	private AsyncBoxedOptionEnumerable(TError? errorValue, IEnumerable<TError>? ignoredErrors)
	{
		Enumerable = null;
		ErrorValue = errorValue;
		IgnoredErrors = ignoredErrors ?? [];
	}

	/// <summary>
	/// Returns the collection as an <see cref="IAsyncEnumerable{IOption{TValue, TError}}" />.
	/// </summary>
	/// <returns>An <see cref="IAsyncEnumerable{IOption{TValue, TError}}" />.</returns>
	public IAsyncEnumerable<IOption<TValue, TError>> AsAsyncEnumerable()
		=> Enumerable ?? throw new Exception(ErrorValue?.ToString() ?? "Error");

	/// <summary>
	/// Iterates over each item in the collection, calling the param <paramref name="handler" />
	/// on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public async Task ForEach(Action<IOption<TValue, TError>> handler)
	{
		if (Enumerable is null)
		{
			throw new Exception(ErrorValue?.ToString() ?? "Error");
		}
		await foreach (var item in Enumerable)
		{
			handler(item);
		}
	}

	/// <summary>
	/// Iterates over each item in the collection, calling the param <paramref name="handler" />
	/// on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public async Task ForEach<TResult>(Func<IOption<TValue, TError>, TResult> handler)
	{
		if (Enumerable is null)
		{
			throw new Exception(ErrorValue?.ToString() ?? "Error");
		}
		await foreach (var item in Enumerable)
		{
			handler(item);
		}
	}

	/// <summary>
	/// Returns an async enumerator for the collection.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>An <see cref="IAsyncEnumerator{IOption{TValue, TError}}" />.</returns>
	public IAsyncEnumerator<IOption<TValue, TError>> GetAsyncEnumerator(
		CancellationToken cancellationToken = default
	) => (Enumerable ?? throw new Exception(ErrorValue?.ToString() ?? "Error"))
		.GetAsyncEnumerator(cancellationToken);

	/// <summary>
	/// Convert this async enumerable to an
	/// <see cref="AsyncOptionEnumerable{TValue, TError}" />.
	/// </summary>
	/// <returns>An <see cref="AsyncOptionEnumerable{TValue, TError}" />.</returns>
	public AsyncOptionEnumerable<TValue, TError> ToUnboxed()
		=> IsError
			? AsyncOptionEnumerable<TValue, TError>.Error(ErrorValue!, IgnoredErrors)
			: new(ToUnboxedAsync(), IgnoredErrors);

	/// <summary>
	/// Converts the async enumerable items to unboxed options.
	/// </summary>
	/// <returns>An <see cref="IAsyncEnumerable{Option{TValue, TError}}" />.</returns>
	private async IAsyncEnumerable<Option<TValue, TError>> ToUnboxedAsync()
	{
		await foreach (var item in Enumerable!)
		{
			yield return Option<TValue, TError>.Unbox(item);
		}
	}
}
