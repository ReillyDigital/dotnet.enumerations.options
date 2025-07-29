namespace ReillyDigital.Enumerations.Options;

using System.Runtime.CompilerServices;

/// <summary>
/// Represents a read-only stream of options with a value of <see cref="TValue" /> that are
/// accessed by subscribing to events of each possible option type, triggered when an item of that
/// type is added to the stream. Errors are of
/// type <see cref="Exception" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public class ReadOnlyOptionStream<TValue>(OptionStream<TValue> optionStream) : IVoid
{
	/// <inheritdoc cref="OptionStream{TValue}.Current" />
	public IOption<TValue>? Current => optionStream.Current;

	/// <inheritdoc cref="OptionStream{TValue}.Read" />
	public async Task<IOption<TValue>> Read(CancellationToken cancellationToken = default)
		=> await optionStream.Read(cancellationToken);

	/// <inheritdoc cref="OptionStream{TValue}.ReadToEnd" />
	public async IAsyncEnumerable<IOption<TValue>> ReadToEnd(
		[EnumeratorCancellation] CancellationToken cancellationToken = default
	)
	{
		await foreach (var each in optionStream.ReadToEnd(cancellationToken: cancellationToken))
		{
			yield return each;
		}
	}
}

/// <summary>
/// Represents a read-only stream of options with a value of <see cref="TValue" /> that are	
/// accessed by subscribing to events of each possible option type, triggered when an item of that
/// type is added to the stream. Errors are of
/// type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public class ReadOnlyOptionStream<TValue, TError>(OptionStream<TValue, TError> optionStream)
	: IVoid<TError>
{
	/// <inheritdoc cref="OptionStream{TValue, TError}.Current" />
	public IOption<TValue, TError>? Current => optionStream.Current;

	/// <inheritdoc cref="OptionStream{TValue, TError}.Read" />
	public async Task<IOption<TValue, TError>> Read(CancellationToken cancellationToken = default)
		=> await optionStream.Read(cancellationToken);

	/// <inheritdoc cref="OptionStream{TValue, TError}.ReadToEnd" />
	public async IAsyncEnumerable<IOption<TValue, TError>> ReadToEnd(
		[EnumeratorCancellation] CancellationToken cancellationToken = default
	)
	{
		await foreach (var each in optionStream.ReadToEnd(cancellationToken: cancellationToken))
		{
			yield return each;
		}
	}
}
