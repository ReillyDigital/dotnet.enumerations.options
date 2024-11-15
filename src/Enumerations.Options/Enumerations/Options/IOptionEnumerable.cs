namespace ReillyDigital.Enumerations.Options;

using System.Collections;

/// <summary>
/// Represents a collection of options with a value of <see cref="TValue" />. Errors will be of type
/// <see cref="Exception" />.
/// </summary>
public interface IOptionEnumerable<out TValue> : IEnumerable<IOption<TValue>>, IVoid
{
	/// <summary>
	/// The enumerator for the collection.
	/// </summary>
	protected IEnumerator<IOption<TValue>> IEnumerator { get; }

	/// <summary>
	/// Returns the collection as a <see cref="IEnumerable{}" />.
	/// </summary>
	/// <returns>A <see cref="IEnumerable{}" /> of <see cref="IOption{}" />.</returns>
	public IEnumerable<IOption<TValue>> AsEnumerable();

	/// <summary>
	/// Iterates over each item in the collection, calling the param <see cref="handler" /> on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public void ForEach(Action<IOption<TValue>> handler);

	/// <summary>
	/// Iterates over each item in the collection, calling the param <see cref="handler" /> on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public void ForEach<TResult>(Func<IOption<TValue>, TResult> handler);

	/// <inheritdoc />
	IEnumerator IEnumerable.GetEnumerator() => IEnumerator;

	/// <inheritdoc />
	IEnumerator<IOption<TValue>> IEnumerable<IOption<TValue>>.GetEnumerator() => IEnumerator;
}
