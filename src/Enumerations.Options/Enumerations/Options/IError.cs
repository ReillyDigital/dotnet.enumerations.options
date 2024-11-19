#pragma warning disable SYSLIB0050

namespace ReillyDigital.Enumerations.Options;

using System.Text.Json;

/// <summary>
/// Represents an option which has an error of <see cref="Exception" />.
/// </summary>
public interface IError : IVoid
{
	/// <summary>
	/// The option error of <see cref="Exception" />.
	/// </summary>
	public Exception Value { get; }
}

/// <summary>
/// Represents an option which has an error of <see cref="Exception" />.
/// </summary>
public interface IError<out TValue> : IError, IOption<TValue>
{
	/// <summary>
	/// The option error of <see cref="Exception" />.
	/// </summary>
	public new Exception Value { get; }
}

/// <summary>
/// Represents an option which has an error of <see cref="TError" />.
/// </summary>
public interface IError<out TValue, out TError> : IError, IOption<TValue, TError>
{
	/// <summary>
	/// The option error of <see cref="TError" />.
	/// </summary>
	public new TError Value { get; }

	/// <inheritdoc />
	Exception IError.Value => Value switch
	{
		Exception exception => exception,
		_ => new(
			$"Value is an error of type {typeof(TError).FullName}.",
			typeof(TError).IsSerializable ? new(JsonSerializer.Serialize(Value)) : null
		)
	};
}
