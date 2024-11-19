namespace ReillyDigital.Enumerations.Options;

using System.Collections;

/// <summary>
/// Represents a collection of options with a value of <see cref="TValue" />. Errors will be of type
/// <see cref="Exception" />.
/// </summary>
public interface IOptionEnumerable<out TValue> : IEnumerable<IOption<TValue>>, IVoid
{
	/// <summary>
	/// Create a reference of <see cref="IOptionEnumerableError{}" />.
	/// </summary>
	/// <returns>An option of <see cref="IOptionEnumerableError{}" />.</returns>
	public new static IOptionEnumerableError<TValue> Error() => new OptionEnumerableError<TValue, Exception>(new());

	/// <summary>
	/// Create a reference of <see cref="IOptionEnumerableError{}" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <returns>An option of <see cref="IOptionEnumerableError{}" />.</returns>
	public new static IOptionEnumerableError<TValue> Error(Exception value) =>
		new OptionEnumerableError<TValue, Exception>(value);

	/// <summary>
	/// Create a reference of <see cref="IOptionEnumerableError{}" />.
	/// </summary>
	/// <param name="message">The error message.</param>
	/// <param name="innerException">An optional inner exception.</param>
	/// <returns>An option of <see cref="IOptionEnumerableError{}" />.</returns>
	public new static IOptionEnumerableError<TValue> Error(string message, Exception? innerException = null) =>
		new OptionEnumerableError<TValue, Exception>(new(message, innerException));

	/// <summary>
	/// Create a reference of <see cref="IOptionEnumerableError{}" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <returns>An option of <see cref="IOptionEnumerableError{}" />.</returns>
	public new static IOptionEnumerableError<TValue> Error<TError>(TError value) =>
		new OptionEnumerableError<TValue, TError>(value);

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

/// <summary>
/// Represents a collection of options with a value of <see cref="TValue" />. Errors will be of type
/// <see cref="TError" />.
/// </summary>
public interface IOptionEnumerable<out TValue, out TError> : IEnumerable<IOption<TValue, TError>>, IVoid<TError>
{
	/// <summary>
	/// Create a reference of <see cref="IOptionEnumerableError{,}" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <returns>An option of <see cref="IOptionEnumerableError{,}" />.</returns>
	public new static IOptionEnumerableError<TValue, TError> Error(TError value) =>
		new OptionEnumerableError<TValue, TError>(value);

	/// <summary>
	/// The enumerator for the collection.
	/// </summary>
	protected IEnumerator<IOption<TValue, TError>> IEnumerator { get; }

	/// <summary>
	/// Returns the collection as a <see cref="IEnumerable{}" />.
	/// </summary>
	/// <returns>A <see cref="IEnumerable{}" /> of <see cref="IOption{,}" />.</returns>
	public IEnumerable<IOption<TValue, TError>> AsEnumerable();

	/// <summary>
	/// Iterates over each item in the collection, calling the param <see cref="handler" /> on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public void ForEach(Action<IOption<TValue, TError>> handler);

	/// <summary>
	/// Iterates over each item in the collection, calling the param <see cref="handler" /> on each item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public void ForEach<TResult>(Func<IOption<TValue, TError>, TResult> handler);

	/// <inheritdoc />
	IEnumerator IEnumerable.GetEnumerator() => IEnumerator;

	/// <inheritdoc />
	IEnumerator<IOption<TValue, TError>> IEnumerable<IOption<TValue, TError>>.GetEnumerator() => IEnumerator;
}
