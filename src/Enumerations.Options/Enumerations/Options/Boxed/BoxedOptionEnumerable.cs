namespace ReillyDigital.Enumerations.Options.Boxed;

using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Represents a collection of options with a value of <see cref="TValue" />. Errors will be of
/// type <see cref="string" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public readonly struct BoxedOptionEnumerable<TValue> : IOptionEnumerable<TValue>
{
	/// <summary>
	/// Create a <see cref="BoxedOptionEnumerable{TValue}" /> in error state.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="BoxedOptionEnumerable{TValue}" /> in error state.</returns>
	public static BoxedOptionEnumerable<TValue> Error(
		IEnumerable<string>? ignoredErrors = null
	) => new("", ignoredErrors);

	/// <summary>
	/// Create a <see cref="BoxedOptionEnumerable{TValue}" /> in error state.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="BoxedOptionEnumerable{TValue}" /> in error state.</returns>
	public static BoxedOptionEnumerable<TValue> Error(
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
	/// The inner enumerable when not in error state.
	/// </summary>
	private IEnumerable<IOption<TValue>>? Enumerable { get; }

	/// <summary>
	/// Constructor for enumerable state.
	/// </summary>
	/// <param name="enumerable">The inner enumerable.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	public BoxedOptionEnumerable(
		IEnumerable<IOption<TValue>> enumerable, IEnumerable<string>? ignoredErrors = null
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
	private BoxedOptionEnumerable(string errorValue, IEnumerable<string>? ignoredErrors)
	{
		Enumerable = null;
		ErrorValue = errorValue;
		IgnoredErrors = ignoredErrors ?? [];
	}

	/// <summary>
	/// Returns the collection as an <see cref="IEnumerable{IOption{TValue}}" />.
	/// </summary>
	/// <returns>An <see cref="IEnumerable{IOption{TValue}}" />.</returns>
	public IEnumerable<IOption<TValue>> AsEnumerable()
		=> Enumerable ?? throw new Exception(ErrorValue ?? "Error");

	/// <summary>
	/// Iterates over each item in the collection, calling the param <paramref name="handler" />
	/// on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public void ForEach(Action<IOption<TValue>> handler)
	{
		if (Enumerable is null)
		{
			throw new Exception(ErrorValue ?? "Error");
		}
		foreach (var item in Enumerable)
		{
			handler(item);
		}
	}

	/// <summary>
	/// Iterates over each item in the collection, calling the param <paramref name="handler" />
	/// on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public void ForEach<TResult>(Func<IOption<TValue>, TResult> handler)
	{
		if (Enumerable is null)
		{
			throw new Exception(ErrorValue ?? "Error");
		}
		foreach (var item in Enumerable)
		{
			handler(item);
		}
	}

	/// <summary>
	/// Returns an enumerator for the collection.
	/// </summary>
	/// <returns>An <see cref="IEnumerator{IOption{TValue}}" />.</returns>
	public IEnumerator<IOption<TValue>> GetEnumerator()
		=> (Enumerable ?? throw new Exception(ErrorValue ?? "Error")).GetEnumerator();

	/// <summary>
	/// Convert this enumerable to an <see cref="OptionEnumerable{TValue}" />.
	/// </summary>
	/// <returns>An <see cref="OptionEnumerable{TValue}" />.</returns>
	public OptionEnumerable<TValue> ToUnboxed()
		=> IsError
			? OptionEnumerable<TValue>.Error(ErrorValue, IgnoredErrors)
			: new(Enumerable!.Select(Option<TValue>.Unbox), IgnoredErrors);

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

/// <summary>
/// Represents a collection of options with a value of <see cref="TValue" />. Errors will be of
/// type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public readonly struct BoxedOptionEnumerable<TValue, TError> : IOptionEnumerable<TValue, TError>
{
	/// <summary>
	/// Create a <see cref="BoxedOptionEnumerable{TValue, TError}" /> in error state.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="BoxedOptionEnumerable{TValue, TError}" /> in error state.</returns>
	public static BoxedOptionEnumerable<TValue, TError> Error(
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
	/// The inner enumerable when not in error state.
	/// </summary>
	private IEnumerable<IOption<TValue, TError>>? Enumerable { get; }

	/// <summary>
	/// Constructor for enumerable state.
	/// </summary>
	/// <param name="enumerable">The inner enumerable.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	public BoxedOptionEnumerable(
		IEnumerable<IOption<TValue, TError>> enumerable,
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
	private BoxedOptionEnumerable(TError? errorValue, IEnumerable<TError>? ignoredErrors)
	{
		Enumerable = null;
		ErrorValue = errorValue;
		IgnoredErrors = ignoredErrors ?? [];
	}

	/// <summary>
	/// Returns the collection as an <see cref="IEnumerable{IOption{TValue, TError}}" />.
	/// </summary>
	/// <returns>An <see cref="IEnumerable{IOption{TValue, TError}}" />.</returns>
	public IEnumerable<IOption<TValue, TError>> AsEnumerable()
		=> Enumerable ?? throw new Exception(ErrorValue?.ToString() ?? "Error");

	/// <summary>
	/// Iterates over each item in the collection, calling the param <paramref name="handler" />
	/// on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public void ForEach(Action<IOption<TValue, TError>> handler)
	{
		if (Enumerable is null)
		{
			throw new Exception(ErrorValue?.ToString() ?? "Error");
		}
		foreach (var item in Enumerable)
		{
			handler(item);
		}
	}

	/// <summary>
	/// Iterates over each item in the collection, calling the param <paramref name="handler" />
	/// on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public void ForEach<TResult>(Func<IOption<TValue, TError>, TResult> handler)
	{
		if (Enumerable is null)
		{
			throw new Exception(ErrorValue?.ToString() ?? "Error");
		}
		foreach (var item in Enumerable)
		{
			handler(item);
		}
	}

	/// <summary>
	/// Returns an enumerator for the collection.
	/// </summary>
	/// <returns>An <see cref="IEnumerator{IOption{TValue, TError}}" />.</returns>
	public IEnumerator<IOption<TValue, TError>> GetEnumerator()
		=> (Enumerable ?? throw new Exception(ErrorValue?.ToString() ?? "Error")).GetEnumerator();

	/// <summary>
	/// Convert this enumerable to an <see cref="OptionEnumerable{TValue, TError}" />.
	/// </summary>
	/// <returns>An <see cref="OptionEnumerable{TValue, TError}" />.</returns>
	public OptionEnumerable<TValue, TError> ToUnboxed()
		=> IsError
			? OptionEnumerable<TValue, TError>.Error(ErrorValue!, IgnoredErrors)
			: new(Enumerable!.Select(Option<TValue, TError>.Unbox), IgnoredErrors);

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
