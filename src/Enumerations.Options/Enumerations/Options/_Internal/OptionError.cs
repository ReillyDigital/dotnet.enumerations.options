#pragma warning disable SYSLIB0050

namespace ReillyDigital.Enumerations.Options._Internal;

using System.Text.Json;

/// <summary>
/// Represents an option which has an error of <see cref="TError" />.
/// </summary>
internal readonly struct OptionError<TValue, TError>(TError value) : IError<TValue>, IError<TValue, TError>
{
	/// <inheritdoc />
	public TError Value => value;

	/// <inheritdoc />
	Exception IError.Value => Value switch
	{
		Exception exception => exception,
		_ => new(
			$"Value is an error of type {typeof(TError).FullName}.",
			typeof(TError).IsSerializable ? new(JsonSerializer.Serialize(Value)) : null
		)
	};

	/// <inheritdoc />
	Exception IError<TValue>.Value => ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	TValue? IOption<TValue>.Value => throw ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	TValue? IOption<TValue, TError>.Value => throw ((IError)this).Value;
}
