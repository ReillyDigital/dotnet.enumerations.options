#pragma warning disable SYSLIB0050

namespace ReillyDigital.Enumerations.Options;

using System.Text.Json;

/// <summary>
/// Represents an option which has an error of <see cref="Exception" />.
/// </summary>
public interface IError : IError<Exception> { }

/// <summary>
/// Represents an option which has an error of <see cref="TError" />.
/// </summary>
public interface IError<TError>
{
	/// <summary>
	/// The option error of <see cref="TError" />.
	/// </summary>
	public TError Value { get; }

	/// <summary>
	/// Gets the value of the error if it is of the type <see cref="Exception" />. Otherwise, gets a new
	/// <see cref="Exception" /> with an inner <see cref="Exception" /> of the serialized <see cref="Value" /> if the
	/// value is serializable.
	/// </summary>
	public Exception AsException() => Value switch
	{
		Exception exception => exception,
		_ => new(
			$"Value is an error of type {typeof(TError).FullName}.",
			typeof(TError).IsSerializable ? new(JsonSerializer.Serialize(Value)) : null
		)
	};
}
