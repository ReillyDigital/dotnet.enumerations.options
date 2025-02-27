namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a collection of options with a value of <see cref="TValue" />. Errors will be of
/// type <see cref="Exception" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public interface IAsyncOptionEnumerable<out TValue> : IAsyncEnumerable<IOption<TValue>>, IVoid
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
	public static IOptionEnumerableError<TValue> Error<TError>(
		TError value, IEnumerable<TError>? ignoredErrors = null
	) => new OptionEnumerableError<TValue, TError>(value, ignoredErrors: ignoredErrors);

	/// <summary>
	/// Returns the collection as a <see cref="IAsyncEnumerable{TValue}" />.
	/// </summary>
	/// <returns>
	/// A <see cref="IAsyncEnumerable{TValue}" /> of <see cref="IOption{TValue}" />.
	/// </returns>
	public IAsyncEnumerable<IOption<TValue>> AsAsyncEnumerable();

	/// <summary>
	/// Iterates over each item in the collection, calling the param <see cref="handler" /> on each
	/// item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public Task ForEach(Action<IOption<TValue>> handler);

	/// <summary>
	/// Iterates over each item in the collection, calling the param <see cref="handler" /> on each
	/// item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public Task ForEach<TResult>(Func<IOption<TValue>, TResult> handler);

	/// <inheritdoc cref="IAsyncEnumerator{IOption{TValue}}" />
	public new IAsyncEnumerator<IOption<TValue>> GetAsyncEnumerator(
		CancellationToken cancellationToken = default
	);

	/// <inheritdoc />
	IAsyncEnumerator<IOption<TValue>> IAsyncEnumerable<IOption<TValue>>.GetAsyncEnumerator(
		CancellationToken cancellationToken
	) => GetAsyncEnumerator(cancellationToken);
}

/// <summary>
/// Represents a collection of options with a value of <see cref="TValue" />. Errors will be of
/// type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public interface IAsyncOptionEnumerable<out TValue, out TError>
	: IAsyncEnumerable<IOption<TValue, TError>>, IVoid<TError>
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
	/// Returns the collection as a <see cref="IAsyncEnumerable{TValue}" />.
	/// </summary>
	/// <returns>
	/// A <see cref="IAsyncEnumerable{TValue}" /> of <see cref="IOption{TValue, TError}" />.
	/// </returns>
	public IAsyncEnumerable<IOption<TValue, TError>> AsAsyncEnumerable();

	/// <summary>
	/// Iterates over each item in the collection, calling the param <see cref="handler" /> on each
	/// item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public Task ForEach(Action<IOption<TValue, TError>> handler);

	/// <summary>
	/// Iterates over each item in the collection, calling the param <see cref="handler" /> on each
	/// item.
	/// </summary>
	/// <param name="handler">The handler to be called for each item of the collection.</param>
	public Task ForEach<TResult>(Func<IOption<TValue, TError>, TResult> handler);

	/// <inheritdoc cref="IAsyncEnumerator{IOption{TValue, TError}}" />
	public new IAsyncEnumerator<IOption<TValue, TError>> GetAsyncEnumerator(
		CancellationToken cancellationToken = default
	);

	/// <inheritdoc />
	IAsyncEnumerator<IOption<TValue, TError>> IAsyncEnumerable<IOption<TValue, TError>>.GetAsyncEnumerator(
		CancellationToken cancellationToken
	) => GetAsyncEnumerator(cancellationToken);
}
