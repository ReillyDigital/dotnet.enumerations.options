namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a void result that can be either a success or an error.
/// </summary>
public readonly struct Void
{
	/// <summary>
	/// Create a <see cref="Void" /> of Error.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="Void" /> of Error.</returns>
	public static Void Error(IEnumerable<ErrorValue>? ignoredErrors = null)
		=> Error(new ErrorValue(), ignoredErrors);

	/// <summary>
	/// Create a <see cref="Void" /> of Error.
	/// </summary>
	/// <param name="error">The error value.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="Void" /> of Error.</returns>
	public static Void Error(ErrorValue error, IEnumerable<ErrorValue>? ignoredErrors = null)
		=> new(Void<ErrorValue>.Error(error, ignoredErrors));

	/// <summary>
	/// Create a <see cref="Void" /> of Success.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="Void" /> of Success.</returns>
	public static Void Success(IEnumerable<ErrorValue>? ignoredErrors = null)
		=> new(Void<ErrorValue>.Success(ignoredErrors));

	/// <summary>
	/// Implicitly convert an <see cref="ErrorValue" /> to a <see cref="Void" /> of Error.
	/// </summary>
	/// <param name="value">The error value.</param>
	public static implicit operator Void(ErrorValue value) => Error(value);

	/// <summary>
	/// Explicitly convert a <see cref="Void" /> to an <see cref="ErrorValue" />.
	/// </summary>
	/// <param name="value">The void.</param>
	public static explicit operator ErrorValue(Void value) => value.ErrorValue;

	/// <summary>
	/// The error value of this void.
	/// </summary>
	public ErrorValue ErrorValue => Inner.ErrorValue;

	/// <summary>
	/// Errors that are ignored instead of being returned as the option value.
	/// </summary>
	public IEnumerable<ErrorValue> IgnoredErrors => Inner.IgnoredErrors;

	/// <summary>
	/// Whether this void is an Error.
	/// </summary>
	public bool IsError => Inner.IsError;

	/// <summary>
	/// Whether this void is a Success.
	/// </summary>
	public bool IsVoid => Inner.IsVoid;

	/// <summary>
	/// The type of void.
	/// </summary>
	public VoidType Type => Inner.Type;

	private Void<ErrorValue> Inner { get; }

	internal Void(Void<ErrorValue> inner) => Inner = inner;

	/// <summary>
	/// Deconstruct the void into its components.
	/// </summary>
	/// <param name="type">The type of void.</param>
	/// <param name="error">The error if Error, otherwise default.</param>
	public void Deconstruct(out VoidType type, out ErrorValue? error)
	{
		Inner.Deconstruct(out type, out var innerError);
		error = innerError;
	}

	/// <summary>
	/// Convert this void to a boxed <see cref="IVoid{TError}" />.
	/// </summary>
	/// <returns>A boxed <see cref="IVoid{TError}" />.</returns>
	public IVoid<ErrorValue> ToBoxed() => Inner.ToBoxed();
}

/// <summary>
/// Represents a void result that can be either a success or an error.
/// </summary>
/// <typeparam name="TError">The type of the error.</typeparam>
public readonly struct Void<TError>
{
	/// <summary>
	/// Create a <see cref="Void{TError}" /> of Error.
	/// </summary>
	/// <param name="error">The error value.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="Void{TError}" /> of Error.</returns>
	public static Void<TError> Error(TError error, IEnumerable<TError>? ignoredErrors = null)
		=> new(VoidType.Error, error, ignoredErrors);

	/// <summary>
	/// Create a <see cref="Void{TError}" /> of Success.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="Void{TError}" /> of Success.</returns>
	public static Void<TError> Success(IEnumerable<TError>? ignoredErrors = null)
		=> new(VoidType.Void, default, ignoredErrors);

	/// <summary>
	/// Implicitly convert a <typeparamref name="TError" /> to a <see cref="Void{TError}" /> of
	/// Error.
	/// </summary>
	/// <param name="value">The error value.</param>
	public static implicit operator Void<TError>(TError value) => Error(value);

	/// <summary>
	/// Implicitly convert a <see cref="Void{TError}" /> to a <typeparamref name="TError" />.
	/// </summary>
	/// <param name="value">The void.</param>
	public static implicit operator TError(Void<TError> value) => value.ErrorValue;

	/// <summary>
	/// Whether this void is an Error.
	/// </summary>
	public bool IsError => Type == VoidType.Error;

	/// <summary>
	/// Whether this void is a Success.
	/// </summary>
	public bool IsVoid => Type == VoidType.Void;

	/// <summary>
	/// The type of void.
	/// </summary>
	public VoidType Type { get; }

	private readonly TError? _ErrorValue;

	/// <summary>
	/// The error value of this void.
	/// </summary>
	public TError ErrorValue
		=> _ErrorValue ?? throw new InvalidOperationException("Result is not Error.");

	private readonly IEnumerable<TError>? _IgnoredErrors;

	/// <summary>
	/// Errors that are ignored instead of being returned as the option value.
	/// </summary>
	public IEnumerable<TError> IgnoredErrors => _IgnoredErrors ?? [];

	internal Void(VoidType type, TError? error, IEnumerable<TError>? ignoredErrors)
	{
		Type = type;
		_ErrorValue = error;
		_IgnoredErrors = ignoredErrors;
	}

	/// <summary>
	/// Deconstruct the void into its components.
	/// </summary>
	/// <param name="type">The type of void.</param>
	/// <param name="error">The error if Error, otherwise default.</param>
	public void Deconstruct(out VoidType type, out TError? error)
		=> (error, type) = (_ErrorValue, Type);

	/// <summary>
	/// Convert this void to a boxed <see cref="IVoid{TError}" />.
	/// </summary>
	/// <returns>A boxed <see cref="IVoid{TError}" />.</returns>
	public IVoid<TError> ToBoxed() => Type switch
	{
		VoidType.Error => new BoxedError<Void, TError>(_ErrorValue!, _IgnoredErrors),
		VoidType.Void => new BoxedVoid<TError>(_IgnoredErrors),
		_ => throw new InvalidOperationException($"Unknown VoidType: {Type}")
	};
}

/// <summary>
/// The type of void.
/// </summary>
public enum VoidType : byte
{
	/// <summary>
	/// The void is an Error.
	/// </summary>
	Error = 255,

	/// <summary>
	/// The void is a Success.
	/// </summary>
	Void = 0
}
