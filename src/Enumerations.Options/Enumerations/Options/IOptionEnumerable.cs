namespace ReillyDigital.Enumerations.Options;

using System.Collections;

/// <summary>
/// Represents a collection of options with a value of <see cref="TValue" />. Errors will be of
/// type <see cref="string" />.
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
		IEnumerable<string?>? ignoredErrors = null
	) => new BoxedEnumerableError<TValue, string?>(default, ignoredErrors: ignoredErrors);

	/// <summary>
	/// Create a reference of <see cref="IOptionEnumerableError{TValue}" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An option of <see cref="IOptionEnumerableError{TValue}" />.</returns>
	public new static IOptionEnumerableError<TValue> Error(
		string? value, IEnumerable<string?>? ignoredErrors = null
	) => new BoxedEnumerableError<TValue, string?>(value, ignoredErrors: ignoredErrors);

	/// <summary>
	/// Create a reference of <see cref="IOptionEnumerableError{TValue}" />.
	/// </summary>
	/// <param name="value">The value of the error.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>An option of <see cref="IOptionEnumerableError{TValue}" />.</returns>
	public static IOptionEnumerableError<TValue> Error<TError>(
		TError value, IEnumerable<TError>? ignoredErrors = null
	) => new BoxedEnumerableError<TValue, TError>(value, ignoredErrors: ignoredErrors);

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

	/// <inheritdoc cref="IEnumerator{IOption{TValue}}" />
	public new IEnumerator<IOption<TValue>> GetEnumerator();

	/// <inheritdoc />
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	/// <inheritdoc />
	IEnumerator<IOption<TValue>> IEnumerable<IOption<TValue>>.GetEnumerator() => GetEnumerator();
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
	) => new BoxedEnumerableError<TValue, TError>(value, ignoredErrors: ignoredErrors);

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

	/// <inheritdoc cref="IEnumerator{IOption{TValue, TError}}" />
	public new IEnumerator<IOption<TValue, TError>> GetEnumerator();

	/// <inheritdoc />
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	/// <inheritdoc />
	IEnumerator<IOption<TValue, TError>> IEnumerable<IOption<TValue, TError>>.GetEnumerator()
		=> GetEnumerator();
}
