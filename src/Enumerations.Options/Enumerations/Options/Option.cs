namespace ReillyDigital.Enumerations.Options;

using System.Linq;

/// <summary>
/// Represents an option with a potential value of <typeparamref name="TValue" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the option.</typeparam>
public readonly struct Option<TValue>
{
	/// <summary>
	/// Create a boxed <see cref="IError{TValue}" />.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A boxed <see cref="IError{TValue}" />.</returns>
	public static IError<TValue> BoxedError(IEnumerable<string>? ignoredErrors = null)
		=> IOption<TValue>.Error(ignoredErrors);

	/// <summary>
	/// Create a boxed <see cref="IError{TValue}" />.
	/// </summary>
	/// <param name="error">The error value.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A boxed <see cref="IError{TValue}" />.</returns>
	public static IError<TValue> BoxedError(string error, IEnumerable<string>? ignoredErrors = null)
		=> IOption<TValue>.Error(error, ignoredErrors);

	/// <summary>
	/// Create a boxed <see cref="INone{TValue}" />.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A boxed <see cref="INone{TValue}" />.</returns>
	public static INone<TValue> BoxedNone(IEnumerable<string>? ignoredErrors = null)
		=> IOption<TValue>.None(ignoredErrors);

	/// <summary>
	/// Create a boxed <see cref="ISome{TValue}" />.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A boxed <see cref="ISome{TValue}" />.</returns>
	public static ISome<TValue> BoxedSome(TValue value, IEnumerable<string>? ignoredErrors = null)
		=> IOption<TValue>.Some(value, ignoredErrors);

	/// <summary>
	/// Create an <see cref="Option{TValue}" /> of Error.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An <see cref="Option{TValue}" /> of Error.</returns>
	public static Option<TValue> Error(IEnumerable<string>? ignoredErrors = null)
		=> Error("", ignoredErrors);

	/// <summary>
	/// Create an <see cref="Option{TValue}" /> of Error.
	/// </summary>
	/// <param name="error">The error value.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An <see cref="Option{TValue}" /> of Error.</returns>
	public static Option<TValue> Error(
		string error, IEnumerable<string>? ignoredErrors = null
	) => new(Option<TValue, string>.Error(error ?? "", ignoredErrors));

	/// <summary>
	/// Create an <see cref="Option{TValue}" /> of None.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An <see cref="Option{TValue}" /> of None.</returns>
	public static Option<TValue> None(IEnumerable<string>? ignoredErrors = null)
		=> new(Option<TValue, string>.None(ignoredErrors));

	/// <summary>
	/// Create an <see cref="Option{TValue}" /> of Some.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An <see cref="Option{TValue}" /> of Some.</returns>
	public static Option<TValue> Some(TValue value, IEnumerable<string>? ignoredErrors = null)
		=> new(Option<TValue, string>.Some(value, ignoredErrors));

	/// <summary>
	/// Convert a boxed <see cref="IOption{TValue}" /> to an <see cref="Option{TValue}" />.
	/// </summary>
	/// <param name="option">The boxed option.</param>
	/// <returns>An <see cref="Option{TValue}" />.</returns>
	public static Option<TValue> Unbox(IOption<TValue> option) => option switch
	{
		IError error => OptionError<TValue>(error.Value),
		INone => None<TValue>(),
		ISome<TValue> some => Some<TValue>(some.Value),
		_ => throw new InvalidOperationException("Invalid option type."),
	};

	/// <summary>
	/// Implicitly convert a <see cref="string" /> to an <see cref="Option{TValue}" /> of
	/// Error.
	/// </summary>
	/// <param name="value">The error value.</param>
	public static implicit operator Option<TValue>(string value) => Error(value);

	/// <summary>
	/// Implicitly convert a <typeparamref name="TValue" /> to an <see cref="Option{TValue}" /> of
	/// Some.
	/// </summary>
	/// <param name="value">The value.</param>
	public static implicit operator Option<TValue>(TValue value) => Some(value);

	/// <summary>
	/// Explicitly convert an <see cref="Option{TValue}" /> to a <see cref="string" />.
	/// </summary>
	/// <param name="value">The option.</param>
	public static explicit operator string(Option<TValue> value) => value.ErrorValue;

	/// <summary>
	/// Explicitly convert an <see cref="Option{TValue}" /> to a <typeparamref name="TValue" />.
	/// </summary>
	/// <param name="value">The option.</param>
	public static explicit operator TValue(Option<TValue> value) => value.Value;

	/// <summary>
	/// The error value of this option.
	/// </summary>
	public string ErrorValue => Inner.ErrorValue;

	/// <summary>
	/// Errors that are ignored instead of being returned as the option value.
	/// </summary>
	public IEnumerable<string> IgnoredErrors => Inner.IgnoredErrors;

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

	/// <summary>
	/// The inner option with string error type.
	/// </summary>
	private Option<TValue, string> Inner { get; }

	/// <summary>
	/// Constructor for this option.
	/// </summary>
	/// <param name="inner">The inner option.</param>
	internal Option(Option<TValue, string> inner) => Inner = inner;

	/// <summary>
	/// Deconstruct the option into its components.
	/// </summary>
	/// <param name="type">The type of option.</param>
	/// <param name="value">The value if Some, otherwise default.</param>
	/// <param name="error">The error if Error, otherwise default.</param>
	public void Deconstruct(out OptionType type, out TValue? value, out string error)
	{
		Inner.Deconstruct(out type, out value, out var innerError);
		error = innerError ?? "";
	}

	/// <summary>
	/// Executes the specified callback if this option is of type <see cref="OptionType.Error" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public Option<TValue> IfError(Action callback) => IfError(_ => callback());

	/// <summary>
	/// Executes the specified callback if this option is of type <see cref="OptionType.Error" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the error.</param>
	/// <returns>The current option.</returns>
	public Option<TValue> IfError(Action<string> callback)
	{
		if (IsError)
		{
			callback(ErrorValue);
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if this option has ignored errors.
	/// </summary>
	/// <param name="callback">The callback to execute with the errors.</param>
	/// <returns>The current option.</returns>
	public Option<TValue> IfIgnoredErrors(Action<IEnumerable<string>> callback)
	{
		if (IgnoredErrors.Any())
		{
			callback(IgnoredErrors);
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if this option is of type <see cref="OptionType.None" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public Option<TValue> IfNone(Action callback)
	{
		if (IsNone)
		{
			callback();
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if this option is of type <see cref="OptionType.Some" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public Option<TValue> IfSome(Action callback) => IfSome(_ => callback());

	/// <summary>
	/// Executes the specified callback if this option is of type <see cref="OptionType.Some" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the value.</param>
	/// <returns>The current option.</returns>
	public Option<TValue> IfSome(Action<TValue> callback)
	{
		if (IsSome)
		{
			callback(Value);
		}
		return this;
	}

	/// <summary>
	/// Convert this option to a boxed <see cref="IOption{TValue}" />.
	/// </summary>
	/// <returns>A boxed <see cref="IOption{TValue}" />.</returns>
	public IOption<TValue> ToBoxed() => (IOption<TValue>)Inner.ToBoxed();
}

/// <summary>
/// Represents an option with a potential value of <typeparamref name="TValue" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the option.</typeparam>
/// <typeparam name="TError">The type of the error of the option.</typeparam>
public readonly struct Option<TValue, TError>
{
	/// <summary>
	/// Create a boxed <see cref="IError{TValue, TError}" />.
	/// </summary>
	/// <param name="error">The error value.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A boxed <see cref="IError{TValue, TError}" />.</returns>
	public static IError<TValue, TError> BoxedError(
		TError error, IEnumerable<TError>? ignoredErrors = null
	) => IOption<TValue, TError>.Error(error, ignoredErrors);

	/// <summary>
	/// Create a boxed <see cref="INone{TValue, TError}" />.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A boxed <see cref="INone{TValue, TError}" />.</returns>
	public static INone<TValue, TError> BoxedNone(IEnumerable<TError>? ignoredErrors = null)
		=> IOption<TValue, TError>.None(ignoredErrors);

	/// <summary>
	/// Create a boxed <see cref="ISome{TValue, TError}" />.
	/// </summary>
	/// <param name="value">The value.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A boxed <see cref="ISome{TValue, TError}" />.</returns>
	public static ISome<TValue, TError> BoxedSome(
		TValue value, IEnumerable<TError>? ignoredErrors = null
	) => IOption<TValue, TError>.Some(value, ignoredErrors);

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
	/// Convert a boxed <see cref="IOption{TValue, TError}" /> to an
	/// <see cref="Option{TValue, TError}" />.
	/// </summary>
	/// <param name="option">The boxed option.</param>
	/// <returns>An <see cref="Option{TValue, TError}" />.</returns>
	public static Option<TValue, TError> Unbox(IOption<TValue, TError> option) => option switch
	{
		IError<TValue, TError> error => Error(error.Value),
		INone => None(),
		ISome<TValue> some => Some(some.Value),
		_ => throw new InvalidOperationException("Invalid option type."),
	};

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

	/// <summary>
	/// The error if Error, otherwise default.
	/// </summary>
	private readonly TError? _ErrorValue;

	/// <summary>
	/// The error value of this option.
	/// </summary>
	public TError ErrorValue
		=> Type == OptionType.Error
			? _ErrorValue!
			: throw new InvalidOperationException("Result is not Error.");

	/// <summary>
	/// Errors that are ignored instead of being returned as the option value.
	/// </summary>
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

	/// <summary>
	/// The value if Some, otherwise default.
	/// </summary>
	private readonly TValue? _SomeValue;

	/// <summary>
	/// The value of this option.
	/// </summary>
	public TValue Value
		=> Type == OptionType.Some
			? _SomeValue!
			: throw new InvalidOperationException("Result is not Some.");

	/// <summary>
	/// Constructor for this option.
	/// </summary>
	/// <param name="type">The type of option.</param>
	/// <param name="value">The value if Some, otherwise default.</param>
	/// <param name="error">The error if Error, otherwise default.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
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
	/// Executes the specified callback if this option is of type <see cref="OptionType.Error" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public Option<TValue, TError> IfError(Action callback) => IfError(_ => callback());

	/// <summary>
	/// Executes the specified callback if this option is of type <see cref="OptionType.Error" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the error.</param>
	/// <returns>The current option.</returns>
	public Option<TValue, TError> IfError(Action<TError> callback)
	{
		if (IsError)
		{
			callback(ErrorValue);
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if this option has ignored errors.
	/// </summary>
	/// <param name="callback">The callback to execute with the errors.</param>
	/// <returns>The current option.</returns>
	public Option<TValue, TError> IfIgnoredErrors(Action<IEnumerable<TError>> callback)
	{
		if (IgnoredErrors.Any())
		{
			callback(IgnoredErrors);
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if this option is of type <see cref="OptionType.None" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public Option<TValue, TError> IfNone(Action callback)
	{
		if (IsNone)
		{
			callback();
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if this option is of type <see cref="OptionType.Some" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current option.</returns>
	public Option<TValue, TError> IfSome(Action callback) => IfSome(_ => callback());

	/// <summary>
	/// Executes the specified callback if this option is of type <see cref="OptionType.Some" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the value.</param>
	/// <returns>The current option.</returns>
	public Option<TValue, TError> IfSome(Action<TValue> callback)
	{
		if (IsSome)
		{
			callback(Value);
		}
		return this;
	}

	/// <summary>
	/// Convert this option to a boxed <see cref="IOption{TValue, TError}" />.
	/// </summary>
	/// <returns>A boxed <see cref="IOption{TValue, TError}" />.</returns>
	public IOption<TValue, TError> ToBoxed() => Type switch
	{
		OptionType.Error => new BoxedError<TValue, TError>(_ErrorValue, _IgnoredErrors),
		OptionType.None => new BoxedNone<TValue, TError>(_IgnoredErrors),
		OptionType.Some => new BoxedSome<TValue, TError>(
			_SomeValue ?? throw new InvalidOperationException("Some value cannot be null."),
			_IgnoredErrors
		),
		_ => throw new InvalidOperationException($"Unknown OptionType: {Type}")
	};
}
