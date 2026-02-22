#pragma warning disable SYSLIB0050

namespace ReillyDigital.Enumerations.Options;

using System.Text.Json;

/// <summary>
/// Represents the simplest return type of nothing or anything.
/// </summary>
public interface IVoid
{
	/// <summary>
	/// Create a reference of <see cref="IError" />.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="IVoid" /> of <see cref="IError" />.</returns>
	public static IError Error(IEnumerable<ErrorValue>? ignoredErrors = null)
		=> Error(new(), ignoredErrors);

	/// <summary>
	/// Create a reference of <see cref="IError" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="IVoid" /> of <see cref="IError" />.</returns>
	public static IError Error(ErrorValue value, IEnumerable<ErrorValue>? ignoredErrors = null)
		=> new BoxedError<IVoid, ErrorValue>(value, ignoredErrors: ignoredErrors);

	/// <summary>
	/// Additional errors of <see cref="ErrorValue" /> which are ignored instead of being returned
	/// as the option value.
	/// </summary>
	public IEnumerable<ErrorValue> IgnoredErrors
		=> this is IIgnoredErrorSet ignoredErrorSet ? ignoredErrorSet.IgnoredErrors : [];

	/// <summary>
	/// Create a reference of <see cref="IVoid" />.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="IVoid" />.</returns>
	public static IVoid Void(IEnumerable<ErrorValue>? ignoredErrors = null)
		=> new BoxedVoid<ErrorValue>(ignoredErrors);

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
	public IVoid IfError(Action<ErrorValue> callback)
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
	public IVoid IfIgnoredErrors(Action<IEnumerable<ErrorValue>> callback)
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
		=> new BoxedVoid<TError>(ignoredErrors);

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
	) => new BoxedError<IVoid, TError>(value, ignoredErrors: ignoredErrors);

	/// <summary>
	/// Additional errors of <see cref="TError" /> which are ignored instead of being returned as
	/// the option value.
	/// </summary>
	public new IEnumerable<TError> IgnoredErrors
		=> this is IIgnoredErrorSet<TError> ignoredErrorSet ? ignoredErrorSet.IgnoredErrors : [];


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
	IEnumerable<ErrorValue> IVoid.IgnoredErrors => IgnoredErrors.Select(
		(error) => error switch
		{
			ErrorValue errorValue => errorValue,
			_ => new ErrorValue(
				typeof(TError).IsSerializable
					? JsonSerializer.Serialize(error)
					: $"Value is an error of type {typeof(TError).FullName}."
			)
		}
	);
}
