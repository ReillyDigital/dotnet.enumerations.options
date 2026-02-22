#pragma warning disable SYSLIB0050

namespace ReillyDigital.Enumerations.Options;

using System.Text.Json;

/// <summary>
/// Represents an option which has an error of <see cref="ErrorValue" />.
/// </summary>
public interface IError : IVoid
{
	/// <summary>
	/// The option error of <see cref="ErrorValue" />.
	/// </summary>
	public ErrorValue Value { get; }
}

/// <summary>
/// Represents an option which has an error of <see cref="ErrorValue" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public interface IError<out TValue> : IError, IOption<TValue>
{
	/// <summary>
	/// The option error of <see cref="ErrorValue" />.
	/// </summary>
	public new ErrorValue Value { get; }
}

/// <summary>
/// Represents an option which has an error of <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public interface IError<out TValue, out TError> : IError, IOption<TValue, TError>
{
	/// <summary>
	/// The option error of <see cref="TError" />.
	/// </summary>
	public new TError Value { get; }

	/// <inheritdoc />
	ErrorValue IError.Value => Value switch
	{
		ErrorValue errorValue => errorValue,
		_ => new(
			typeof(TError).IsSerializable
				? JsonSerializer.Serialize(Value)
				: $"Value is an error of type {typeof(TError).FullName}."
		)
	};
}
