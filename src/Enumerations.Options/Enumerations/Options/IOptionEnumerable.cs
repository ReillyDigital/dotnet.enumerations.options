namespace ReillyDigital.Enumerations.Options;

using System.Collections;

/// <summary>
/// Represents a collection of options with a value of <see cref="TValue" />. Errors will be of
/// type <see cref="Exception" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public interface IOptionEnumerable<out TValue> : IEnumerable<IOption<TValue>>, IVoid
{
	/// <summary>
	/// Create a reference of <see cref="IOptionEnumerableError{TValue}" />.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An option of <see cref="IOptionEnumerableError{TValue}" />.</returns>
	public new static IOptionEnumerableError<TValue> Error(
		IEnumerable<Exception>? ignoredErrors = null
	) => new OptionEnumerableError<TValue, Exception>(new(), ignoredErrors: ignoredErrors);

	/// <summary>
	/// Create a reference of <see cref="IOptionEnumerableError{TValue}" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An option of <see cref="IOptionEnumerableError{TValue}" />.</returns>
	public new static IOptionEnumerableError<TValue> Error(
		Exception value, IEnumerable<Exception>? ignoredErrors = null
	) => new OptionEnumerableError<TValue, Exception>(value, ignoredErrors: ignoredErrors);

	/// <summary>
	/// Create a reference of <see cref="IOptionEnumerableError{TValue}" />.
	/// </summary>
	/// <param name="message">The error message.</param>
	/// <param name="innerException">An optional inner exception.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An option of <see cref="IOptionEnumerableError{TValue}" />.</returns>
	public new static IOptionEnumerableError<TValue> Error(
		string message, Exception? innerException = null, IEnumerable<Exception>? ignoredErrors = null
	) => new OptionEnumerableError<TValue, Exception>(
		new(message, innerException), ignoredErrors: ignoredErrors
	);

	/// <summary>
	/// Create a reference of <see cref="IOptionEnumerableError{TValue}" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An option of <see cref="IOptionEnumerableError{TValue}" />.</returns>
	public new static IOptionEnumerableError<TValue> Error<TError>(
		TError value, IEnumerable<TError>? ignoredErrors = null
	) => new OptionEnumerableError<TValue, TError>(value, ignoredErrors: ignoredErrors);

	/// <summary>
	/// The enumerator for the collection.
	/// </summary>
	protected IEnumerator<IOption<TValue>> IEnumerator { get; }

	/// <summary>
	/// Returns the collection as a <see cref="IEnumerable{TValue}" />.
	/// </summary>
	/// <returns>A <see cref="IEnumerable{TValue}" /> of <see cref="IOption{TValue}" />.</returns>
	public IEnumerable<IOption<TValue>> AsEnumerable();

	/// <summary>
	/// Iterates over each item in the collection, calling the param <see cref="handler" /> on each
	/// item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public void ForEach(Action<IOption<TValue>> handler);

	/// <summary>
	/// Iterates over each item in the collection, calling the param <see cref="handler" /> on each
	/// item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public void ForEach<TResult>(Func<IOption<TValue>, TResult> handler);

	/// <inheritdoc />
	IEnumerator IEnumerable.GetEnumerator() => IEnumerator;

	/// <inheritdoc />
	IEnumerator<IOption<TValue>> IEnumerable<IOption<TValue>>.GetEnumerator() => IEnumerator;
}

/// <summary>
/// Represents a collection of options with a value of <see cref="TValue" />. Errors will be of
/// type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public interface IOptionEnumerable<out TValue, out TError>
	: IEnumerable<IOption<TValue, TError>>, IVoid<TError>
{
	/// <summary>
	/// Create a reference of <see cref="IOptionEnumerableError{TValue, TError}" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An option of <see cref="IOptionEnumerableError{TValue, TError}" />.</returns>
	public new static IOptionEnumerableError<TValue, TError> Error(
		TError value, IEnumerable<TError>? ignoredErrors = null
	) => new OptionEnumerableError<TValue, TError>(value, ignoredErrors: ignoredErrors);

	/// <summary>
	/// The enumerator for the collection.
	/// </summary>
	protected IEnumerator<IOption<TValue, TError>> IEnumerator { get; }

	/// <summary>
	/// Returns the collection as a <see cref="IEnumerable{TValue}" />.
	/// </summary>
	/// <returns>A <see cref="IEnumerable{TValue}" /> of <see cref="IOption{TValue, TError}" />.</returns>
	public IEnumerable<IOption<TValue, TError>> AsEnumerable();

	/// <summary>
	/// Iterates over each item in the collection, calling the param <see cref="handler" /> on each
	/// item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public void ForEach(Action<IOption<TValue, TError>> handler);

	/// <summary>
	/// Iterates over each item in the collection, calling the param <see cref="handler" /> on each
	/// item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public void ForEach<TResult>(Func<IOption<TValue, TError>, TResult> handler);

	/// <inheritdoc />
	IEnumerator IEnumerable.GetEnumerator() => IEnumerator;

	/// <inheritdoc />
	IEnumerator<IOption<TValue, TError>> IEnumerable<IOption<TValue, TError>>.GetEnumerator()
		=> IEnumerator;
}
