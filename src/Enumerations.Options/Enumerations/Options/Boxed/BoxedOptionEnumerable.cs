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
	/// Backing field for the inner enumerable when not in error state.
	/// </summary>
	private readonly IEnumerable<IOption<TValue>>? _Enumerable;

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
	/// <param name="enumerable">The inner enumerable.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	public BoxedOptionEnumerable(
		IEnumerable<IOption<TValue>> enumerable, IEnumerable<string>? ignoredErrors = null
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
	private BoxedOptionEnumerable(string errorValue, IEnumerable<string>? ignoredErrors)
	{
		_Enumerable = null;
		_ErrorValue = errorValue;
		_IgnoredErrors = ignoredErrors ?? [];
	}

	/// <summary>
	/// Returns the collection as an <see cref="IEnumerable{IOption{TValue}}" />.
	/// </summary>
	/// <returns>An <see cref="IEnumerable{IOption{TValue}}" />.</returns>
	public IEnumerable<IOption<TValue>> AsEnumerable()
		=> _Enumerable ?? throw new Exception(_ErrorValue ?? "Error");

	/// <summary>
	/// Iterates over each item in the collection, calling the param <paramref name="handler" />
	/// on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public void ForEach(Action<IOption<TValue>> handler)
	{
		if (_Enumerable is null)
		{
			throw new Exception(_ErrorValue ?? "Error");
		}
		foreach (var item in _Enumerable)
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
		if (_Enumerable is null)
		{
			throw new Exception(_ErrorValue ?? "Error");
		}
		foreach (var item in _Enumerable)
		{
			handler(item);
		}
	}

	/// <summary>
	/// Returns an enumerator for the collection.
	/// </summary>
	/// <returns>An <see cref="IEnumerator{IOption{TValue}}" />.</returns>
	public IEnumerator<IOption<TValue>> GetEnumerator()
		=> (_Enumerable ?? throw new Exception(_ErrorValue ?? "Error")).GetEnumerator();

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
	/// Backing field for the inner enumerable when not in error state.
	/// </summary>
	private readonly IEnumerable<IOption<TValue, TError>>? _Enumerable;

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
	/// <param name="enumerable">The inner enumerable.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	public BoxedOptionEnumerable(
		IEnumerable<IOption<TValue, TError>> enumerable,
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
	private BoxedOptionEnumerable(TError? errorValue, IEnumerable<TError>? ignoredErrors)
	{
		_Enumerable = null;
		_ErrorValue = errorValue;
		_IgnoredErrors = ignoredErrors ?? [];
	}

	/// <summary>
	/// Returns the collection as an <see cref="IEnumerable{IOption{TValue, TError}}" />.
	/// </summary>
	/// <returns>An <see cref="IEnumerable{IOption{TValue, TError}}" />.</returns>
	public IEnumerable<IOption<TValue, TError>> AsEnumerable()
		=> _Enumerable ?? throw new Exception(_ErrorValue?.ToString() ?? "Error");

	/// <summary>
	/// Iterates over each item in the collection, calling the param <paramref name="handler" />
	/// on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public void ForEach(Action<IOption<TValue, TError>> handler)
	{
		if (_Enumerable is null)
		{
			throw new Exception(_ErrorValue?.ToString() ?? "Error");
		}
		foreach (var item in _Enumerable)
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
		if (_Enumerable is null)
		{
			throw new Exception(_ErrorValue?.ToString() ?? "Error");
		}
		foreach (var item in _Enumerable)
		{
			handler(item);
		}
	}

	/// <summary>
	/// Returns an enumerator for the collection.
	/// </summary>
	/// <returns>An <see cref="IEnumerator{IOption{TValue, TError}}" />.</returns>
	public IEnumerator<IOption<TValue, TError>> GetEnumerator()
		=> (_Enumerable ?? throw new Exception(_ErrorValue?.ToString() ?? "Error"))
			.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
