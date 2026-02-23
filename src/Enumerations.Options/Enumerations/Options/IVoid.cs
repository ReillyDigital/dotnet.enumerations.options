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
	public static IError Error(IEnumerable<string?>? ignoredErrors = null)
		=> Error(default, ignoredErrors);

	/// <summary>
	/// Create a reference of <see cref="IError" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="IVoid" /> of <see cref="IError" />.</returns>
	public static IError Error(string? value, IEnumerable<string?>? ignoredErrors = null)
		=> new BoxedError<IVoid, string?>(value, ignoredErrors: ignoredErrors);

	/// <summary>
	/// Additional errors of <see cref="string" /> which are ignored instead of being returned
	/// as the option value.
	/// </summary>
	public IEnumerable<string?> IgnoredErrors
		=> this is IIgnoredErrorSet ignoredErrorSet ? ignoredErrorSet.IgnoredErrors : [];

	/// <summary>
	/// Create a reference of <see cref="IVoid" />.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A <see cref="IVoid" />.</returns>
	public static IVoid Void(IEnumerable<string?>? ignoredErrors = null)
		=> ignoredErrors is null ? BoxedVoid<string?>.Ref : new BoxedVoid<string?>(ignoredErrors);

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
	public IVoid IfError(Action<string?> callback)
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
	public IVoid IfIgnoredErrors(Action<IEnumerable<string?>> callback)
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
		=> ignoredErrors is null ? BoxedVoid<TError>.Ref : new BoxedVoid<TError>(ignoredErrors);

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
	IEnumerable<string?> IVoid.IgnoredErrors => IgnoredErrors.Select(
		(error) => error switch
		{
			string s => s,
			null => (string?)null,
			_ => typeof(TError).IsSerializable
				? JsonSerializer.Serialize(error)
				: $"Value is an error of type {typeof(TError).FullName}."
		}
	);
}
