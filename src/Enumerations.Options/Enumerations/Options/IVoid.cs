#pragma warning disable SYSLIB0050

namespace ReillyDigital.Enumerations.Options;

using System.Text.Json;

/// <summary>
/// Represents the simplest return type of nothing or anything.
/// </summary>
public interface IVoid
{
	/// <summary>
	/// Create a reference of <see cref="IVoid" />.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="IVoid" />.</returns>
	public static IVoid Void(IEnumerable<Exception>? ignoredErrors = null)
		=> ignoredErrors is null
			? _Internal.Void<Exception>.Ref
			: new Void<Exception>(ignoredErrors: ignoredErrors);

	/// <summary>
	/// Create a reference of <see cref="IError" />.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="IVoid" /> of <see cref="IError" />.</returns>
	public static IError Error(IEnumerable<Exception>? ignoredErrors = null)
		=> Error(new(), ignoredErrors);

	/// <summary>
	/// Create a reference of <see cref="IError" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="IVoid" /> of <see cref="IError" />.</returns>
	public static IError Error(Exception value, IEnumerable<Exception>? ignoredErrors = null)
		=> new OptionError<IVoid, Exception>(value, ignoredErrors: ignoredErrors);

	/// <summary>
	/// Create a reference of <see cref="IError" />.
	/// </summary>
	/// <param name="message">The error message.</param>
	/// <param name="innerException">An optional inner exception.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="IVoid" /> of <see cref="IError" />.</returns>
	public static IError Error(
		string message, Exception? innerException = null, IEnumerable<Exception>? ignoredErrors = null
	) => Error(new(message, innerException), ignoredErrors: ignoredErrors);

	/// <summary>
	/// Additional errors of <see cref="Exception" /> which are ignored instead of being returned as
	/// the option value.
	/// </summary>
	public IEnumerable<Exception> IgnoredErrors { get; }

	/// <summary>
	/// Executes the specified callback if this reference is of type <see cref="IError" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current reference.</returns>
	public IVoid IfError(Action callback) => IfError((_) => callback());

	/// <summary>
	/// Executes the specified callback if this reference is of type <see cref="IError" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the error.</param>
	/// <returns>The current reference.</returns>
	public IVoid IfError(Action<Exception> callback)
	{
		if (this is IError error)
		{
			callback(error.Value);
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if this reference has ignored errors.
	/// </summary>
	/// <param name="callback">The callback to execute with the error.</param>
	/// <returns>The current reference.</returns>
	public IVoid IfIgnoredErrors(Action<IEnumerable<Exception>> callback)
	{
		if (IgnoredErrors.Any())
		{
			callback(IgnoredErrors);
		}
		return this;
	}
}

/// <summary>
/// Represents the simplest return type of nothing or anything.
/// </summary>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public interface IVoid<out TError> : IVoid
{
	/// <summary>
	/// Create a reference of <see cref="IVoid{TError}" />.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="IVoid{TError}" />.</returns>
	public static IVoid<TError> Void(IEnumerable<TError>? ignoredErrors = null)
		=> ignoredErrors is null
			? _Internal.Void<TError>.Ref
			: new Void<TError>(ignoredErrors: ignoredErrors);

	/// <summary>
	/// Create a reference of <see cref="IError{TValue, TError}" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="IVoid" /> of <see cref="IError{TValue, TError}" />.</returns>
	public static IError<IVoid, TError> Error(
		TError value, IEnumerable<TError>? ignoredErrors = null
	) => new OptionError<IVoid, TError>(value, ignoredErrors: ignoredErrors);

	/// <summary>
	/// Additional errors of <see cref="TError" /> which are ignored instead of being returned as
	/// the option value.
	/// </summary>
	public new IEnumerable<TError> IgnoredErrors { get; }

	/// <summary>
	/// Executes the specified callback if this reference is of type <see cref="IError{TValue, TError}" />.
	/// </summary>
	/// <param name="callback">The callback to execute.</param>
	/// <returns>The current reference.</returns>
	public new IVoid<TError> IfError(Action callback) => IfError((_) => callback());

	/// <summary>
	/// Executes the specified callback if this reference is of type <see cref="IError{TValue, TError}" />.
	/// </summary>
	/// <param name="callback">The callback to execute with the error.</param>
	/// <returns>The current reference.</returns>
	public IVoid<TError> IfError(Action<TError> callback)
	{
		if (this is IError<IVoid, TError> error)
		{
			callback(error.Value);
		}
		return this;
	}

	/// <summary>
	/// Executes the specified callback if this reference has ignored errors.
	/// </summary>
	/// <param name="callback">The callback to execute with the error.</param>
	/// <returns>The current reference.</returns>
	public IVoid<TError> IfIgnoredErrors(Action<IEnumerable<TError>> callback)
	{
		if (IgnoredErrors.Any())
		{
			callback(IgnoredErrors);
		}
		return this;
	}

	/// <inheritdoc />
	IEnumerable<Exception> IVoid.IgnoredErrors => IgnoredErrors.Select(
		(error) => error switch
		{
			Exception exception => exception,
			_ => new(
				$"Value is an error of type {typeof(TError).FullName}.",
				typeof(TError).IsSerializable ? new(JsonSerializer.Serialize(error)) : null
			)
		}
	);
}
