namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents an option with a potential value of <typeparamref name="TValue" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the option.</typeparam>
public readonly struct Option<TValue>
{
	/// <summary>
	/// Create an <see cref="Option{TValue}" /> of Error.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An <see cref="Option{TValue}" /> of Error.</returns>
	public static Option<TValue> Error(IEnumerable<ErrorValue>? ignoredErrors = null)
		=> Error(new(), ignoredErrors);

	/// <summary>
	/// Create an <see cref="Option{TValue}" /> of Error.
	/// </summary>
	/// <param name="error">The error value.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An <see cref="Option{TValue}" /> of Error.</returns>
	public static Option<TValue> Error(
		ErrorValue error, IEnumerable<ErrorValue>? ignoredErrors = null
	) => new(Option<TValue, ErrorValue>.Error(error, ignoredErrors));

	/// <summary>
	/// Create an <see cref="Option{TValue}" /> of None.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An <see cref="Option{TValue}" /> of None.</returns>
	public static Option<TValue> None(IEnumerable<ErrorValue>? ignoredErrors = null)
		=> new(Option<TValue, ErrorValue>.None(ignoredErrors));

	/// <summary>
	/// Create an <see cref="Option{TValue}" /> of Some.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An <see cref="Option{TValue}" /> of Some.</returns>
	public static Option<TValue> Some(TValue value, IEnumerable<ErrorValue>? ignoredErrors = null)
		=> new(Option<TValue, ErrorValue>.Some(value, ignoredErrors));

	/// <summary>
	/// Implicitly convert an <see cref="ErrorValue" /> to an <see cref="Option{TValue}" /> of
	/// Error.
	/// </summary>
	/// <param name="value">The error value.</param>
	public static implicit operator Option<TValue>(ErrorValue value) => Error(value);

	/// <summary>
	/// Implicitly convert a <typeparamref name="TValue" /> to an <see cref="Option{TValue}" /> of
	/// Some.
	/// </summary>
	/// <param name="value">The value.</param>
	public static implicit operator Option<TValue>(TValue value) => Some(value);

	/// <summary>
	/// Explicitly convert an <see cref="Option{TValue}" /> to an <see cref="ErrorValue" />.
	/// </summary>
	/// <param name="value">The option.</param>
	public static explicit operator ErrorValue(Option<TValue> value) => value.ErrorValue;

	/// <summary>
	/// Explicitly convert an <see cref="Option{TValue}" /> to a <typeparamref name="TValue" />.
	/// </summary>
	/// <param name="value">The option.</param>
	public static explicit operator TValue(Option<TValue> value) => value.Value;

	/// <summary>
	/// The error value of this option.
	/// </summary>
	public ErrorValue ErrorValue => Inner.ErrorValue;

	/// <summary>
	/// Errors that are ignored instead of being returned as the option value.
	/// </summary>
	public IEnumerable<ErrorValue> IgnoredErrors => Inner.IgnoredErrors;

	/// <summary>
	/// Whether this option is an Error.
	/// </summary>
	public bool IsError => Inner.IsError;

	/// <summary>
	/// Whether this option is a None.
	/// </summary>
	public bool IsNone => Inner.IsNone;

	/// <summary>
	/// Whether this option is a Some.
	/// </summary>
	public bool IsSome => Inner.IsSome;

	/// <summary>
	/// The type of option.
	/// </summary>
	public OptionType Type => Inner.Type;

	/// <summary>
	/// The value of this option.
	/// </summary>
	public TValue Value => Inner.Value;

	private Option<TValue, ErrorValue> Inner { get; }

	internal Option(Option<TValue, ErrorValue> inner) => Inner = inner;

	/// <summary>
	/// Deconstruct the option into its components.
	/// </summary>
	/// <param name="type">The type of option.</param>
	/// <param name="value">The value if Some, otherwise default.</param>
	/// <param name="error">The error if Error, otherwise default.</param>
	public void Deconstruct(out OptionType type, out TValue? value, out ErrorValue? error)
	{
		Inner.Deconstruct(out type, out value, out var innerError);
		error = innerError;
	}

	/// <summary>
	/// Convert this option to a boxed <see cref="IVoid{TError}" />.
	/// </summary>
	/// <returns>A boxed <see cref="IVoid{TError}" />.</returns>
	public IVoid<ErrorValue> ToBoxed() => Inner.ToBoxed();
}

/// <summary>
/// Represents an option with a potential value of <typeparamref name="TValue" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the option.</typeparam>
/// <typeparam name="TError">The type of the error of the option.</typeparam>
public readonly struct Option<TValue, TError>
{
	/// <summary>
	/// Create an <see cref="Option{TValue, TError}" /> of Error.
	/// </summary>
	/// <param name="error">The error value.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An <see cref="Option{TValue, TError}" /> of Error.</returns>
	public static Option<TValue, TError> Error(
		TError error, IEnumerable<TError>? ignoredErrors = null
	) => new(OptionType.Error, default, error, ignoredErrors);

	/// <summary>
	/// Create an <see cref="Option{TValue, TError}" /> of None.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An <see cref="Option{TValue, TError}" /> of None.</returns>
	public static Option<TValue, TError> None(IEnumerable<TError>? ignoredErrors = null)
		=> new(OptionType.None, default, default, ignoredErrors);

	/// <summary>
	/// Create an <see cref="Option{TValue, TError}" /> of Some.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An <see cref="Option{TValue, TError}" /> of Some.</returns>
	public static Option<TValue, TError> Some(
		TValue value, IEnumerable<TError>? ignoredErrors = null
	) => new(OptionType.Some, value, default, ignoredErrors);

	/// <summary>
	/// Implicitly convert a <typeparamref name="TError" /> to an
	/// <see cref="Option{TValue, TError}" /> of Error.
	/// </summary>
	/// <param name="value">The error value.</param>
	public static implicit operator Option<TValue, TError>(TError value) => Error(value);

	/// <summary>
	/// Implicitly convert a <typeparamref name="TValue" /> to an
	/// <see cref="Option{TValue, TError}" /> of Some.
	/// </summary>
	/// <param name="value">The value.</param>
	public static implicit operator Option<TValue, TError>(TValue value) => Some(value);

	/// <summary>
	/// Explicitly convert an <see cref="Option{TValue, TError}" /> to a
	/// <typeparamref name="TError" />.
	/// </summary>
	/// <param name="value">The option.</param>
	public static explicit operator TError(Option<TValue, TError> value) => value.ErrorValue;

	/// <summary>
	/// Explicitly convert an <see cref="Option{TValue, TError}" /> to a
	/// <typeparamref name="TValue" />.
	/// </summary>
	/// <param name="value">The option.</param>
	public static explicit operator TValue(Option<TValue, TError> value) => value.Value;

	private readonly TError? _ErrorValue;

	/// <summary>
	/// The error value of this option.
	/// </summary>
	public TError ErrorValue
		=> _ErrorValue ?? throw new InvalidOperationException("Result is not Error.");

	private readonly IEnumerable<TError>? _IgnoredErrors;

	/// <summary>
	/// Errors that are ignored instead of being returned as the option value.
	/// </summary>
	public IEnumerable<TError> IgnoredErrors => _IgnoredErrors ?? [];

	/// <summary>
	/// Whether this option is an Error.
	/// </summary>
	public bool IsError => Type == OptionType.Error;

	/// <summary>
	/// Whether this option is a None.
	/// </summary>
	public bool IsNone => Type == OptionType.None;

	/// <summary>
	/// Whether this option is a Some.
	/// </summary>
	public bool IsSome => Type == OptionType.Some;

	/// <summary>
	/// The type of option.
	/// </summary>
	public OptionType Type { get; }

	private readonly TValue? _SomeValue;

	/// <summary>
	/// The value of this option.
	/// </summary>
	public TValue Value
		=> _SomeValue ?? throw new InvalidOperationException("Result is not Some.");

	internal Option(
		OptionType type, TValue? value, TError? error, IEnumerable<TError>? ignoredErrors
	)
	{
		Type = type;
		_SomeValue = value;
		_ErrorValue = error;
		_IgnoredErrors = ignoredErrors;
	}

	/// <summary>
	/// Deconstruct the option into its components.
	/// </summary>
	/// <param name="type">The type of option.</param>
	/// <param name="value">The value if Some, otherwise default.</param>
	/// <param name="error">The error if Error, otherwise default.</param>
	public void Deconstruct(out OptionType type, out TValue? value, out TError? error)
		=> (error, type, value) = (_ErrorValue, Type, _SomeValue);

	/// <summary>
	/// Convert this option to a boxed <see cref="IVoid{TError}" />.
	/// </summary>
	/// <returns>A boxed <see cref="IVoid{TError}" />.</returns>
	public IVoid<TError> ToBoxed() => Type switch
	{
		OptionType.Error => new BoxedError<TValue, TError>(_ErrorValue!, _IgnoredErrors),
		OptionType.None => new BoxedNone<TValue, TError>(_IgnoredErrors),
		OptionType.Some => new BoxedSome<TValue, TError>(_SomeValue!, _IgnoredErrors),
		_ => throw new InvalidOperationException($"Unknown OptionType: {Type}")
	};
}

/// <summary>
/// The type of option.
/// </summary>
public enum OptionType : byte
{
	/// <summary>
	/// The option is an Error.
	/// </summary>
	Error = 255,

	/// <summary>
	/// The option is a None.
	/// </summary>
	None = 2,

	/// <summary>
	/// The option is a Some.
	/// </summary>
	Some = 1
}
