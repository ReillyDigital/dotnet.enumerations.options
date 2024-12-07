#pragma warning disable SYSLIB0050

namespace ReillyDigital.Enumerations.Options._Internal;

using System.Text.Json;

/// <summary>
/// Represents a collection of options which has an error of <see cref="Exception" />.
/// </summary>
internal readonly struct OptionEnumerableError<TValue, TError>(TError value)
	: IOptionEnumerableError<TValue>, IOptionEnumerableError<TValue, TError>
{
	/// <summary>
	/// The option error of <see cref="TError" />.
	/// </summary>
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
	IEnumerator<IOption<TValue>> IOptionEnumerable<TValue>.IEnumerator => throw ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	IEnumerator<IOption<TValue, TError>> IOptionEnumerable<TValue, TError>.IEnumerator => throw ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	TValue? IOption<TValue>.Value => throw ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	TValue? IOption<TValue, TError>.Value => throw ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	IEnumerable<IOption<TValue>> IOptionEnumerable<TValue>.AsEnumerable() => throw ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	IEnumerable<IOption<TValue, TError>> IOptionEnumerable<TValue, TError>.AsEnumerable() => throw ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	void IOptionEnumerable<TValue>.ForEach(Action<IOption<TValue>> handler) => throw ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	void IOptionEnumerable<TValue, TError>.ForEach(Action<IOption<TValue, TError>> handler) =>
		throw ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	void IOptionEnumerable<TValue>.ForEach<TResult>(Func<IOption<TValue>, TResult> handler) =>
		throw ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	void IOptionEnumerable<TValue, TError>.ForEach<TResult>(Func<IOption<TValue, TError>, TResult> handler) =>
		throw ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => throw ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	IEnumerator<IOption<TValue>> IEnumerable<IOption<TValue>>.GetEnumerator() => throw ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	IEnumerator<IOption<TValue, TError>> IEnumerable<IOption<TValue, TError>>.GetEnumerator() =>
		throw ((IError)this).Value;
}
