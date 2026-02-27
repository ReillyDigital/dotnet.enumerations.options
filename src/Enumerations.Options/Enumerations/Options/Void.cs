namespace ReillyDigital.Enumerations.Options;

using System.Linq;

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
	public static Void Error(IEnumerable<string>? ignoredErrors = null)
		=> Error("", ignoredErrors);

	/// <summary>
	/// Create a <see cref="Void" /> of Error.
	/// </summary>
	/// <param name="error">The error value.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="Void" /> of Error.</returns>
	public static Void Error(string error, IEnumerable<string>? ignoredErrors = null)
		=> new(Void<string>.Error(error ?? "", ignoredErrors));

	/// <summary>
	/// Create a <see cref="Void" /> of Success.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="Void" /> of Success.</returns>
	public static Void Success(IEnumerable<string>? ignoredErrors = null)
		=> new(Void<string>.Success(ignoredErrors));

	/// <summary>
	/// Implicitly convert a <see cref="string" /> to a <see cref="Void" /> of Error.
	/// </summary>
	/// <param name="value">The error value.</param>
	public static implicit operator Void(string value) => Error(value);

	/// <summary>
	/// Explicitly convert a <see cref="Void" /> to a <see cref="string" />.
	/// </summary>
	/// <param name="value">The void.</param>
	public static explicit operator string(Void value) => value.ErrorValue;

	/// <summary>
	/// The error value of this void.
	/// </summary>
	public string ErrorValue => Inner.ErrorValue;

	/// <summary>
	/// Errors that are ignored instead of being returned as the option value.
	/// </summary>
	public IEnumerable<string> IgnoredErrors => Inner.IgnoredErrors;

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

	private Void<string> Inner { get; }

	/// <summary>
	/// Constructor for this void.
	/// </summary>
	/// <param name="inner">The inner void.</param>
	internal Void(Void<string> inner) => Inner = inner;

	/// <summary>
	/// Deconstruct the void into its components.
	/// </summary>
	/// <param name="type">The type of void.</param>
	/// <param name="error">The error if Error, otherwise default.</param>
	public void Deconstruct(out VoidType type, out string error)
	{
		Inner.Deconstruct(out type, out var innerError);
		error = innerError ?? "";
	}

	/// <summary>
	/// Executes the specified callback if this void is of type <see cref="VoidType.Error" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current void.</returns>
	public Void IfError(Action callback) => IfError(_ => callback());

	/// <summary>
	/// Executes the specified callback if this void is of type <see cref="VoidType.Error" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the error.</param>
	/// <returns>The current void.</returns>
	public Void IfError(Action<string> callback)
	{
		if (IsError)
		{
			callback(ErrorValue);
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if this void has ignored errors.
	/// </summary>
	/// <param name="callback">The callback to execute with the errors.</param>
	/// <returns>The current void.</returns>
	public Void IfIgnoredErrors(Action<IEnumerable<string>> callback)
	{
		if (IgnoredErrors.Any())
		{
			callback(IgnoredErrors);
		}
		return this;
	}

	/// <summary>
	/// Convert this void to a boxed <see cref="IVoid{TError}" />.
	/// </summary>
	/// <returns>A boxed <see cref="IVoid{TError}" />.</returns>
	public IVoid<string> ToBoxed() => Inner.ToBoxed();
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

	/// <summary>
	/// Constructor for this void.
	/// </summary>
	/// <param name="type">The type of void.</param>
	/// <param name="error">The error if Error, otherwise default.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
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
	/// Executes the specified callback if this void is of type <see cref="VoidType.Error" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current void.</returns>
	public Void<TError> IfError(Action callback) => IfError(_ => callback());

	/// <summary>
	/// Executes the specified callback if this void is of type <see cref="VoidType.Error" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the error.</param>
	/// <returns>The current void.</returns>
	public Void<TError> IfError(Action<TError> callback)
	{
		if (IsError)
		{
			callback(ErrorValue);
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if this void has ignored errors.
	/// </summary>
	/// <param name="callback">The callback to execute with the errors.</param>
	/// <returns>The current void.</returns>
	public Void<TError> IfIgnoredErrors(Action<IEnumerable<TError>> callback)
	{
		if (IgnoredErrors.Any())
		{
			callback(IgnoredErrors);
		}
		return this;
	}

	/// <summary>
	/// Convert this void to a boxed <see cref="IVoid{TError}" />.
	/// </summary>
	/// <returns>A boxed <see cref="IVoid{TError}" />.</returns>
	public IVoid<TError> ToBoxed() => Type switch
	{
		VoidType.Error => new BoxedError<Void, TError>(_ErrorValue, _IgnoredErrors),
		VoidType.Void => new BoxedVoid<TError>(_IgnoredErrors),
		_ => throw new InvalidOperationException($"Unknown VoidType: {Type}")
	};
}
