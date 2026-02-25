namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Extension methods for converting <see cref="IEnumerable{IOption{TValue}}" /> to
/// <see cref="BoxedOptionEnumerable{TValue}" />.
/// </summary>
public static partial class IEnumerableExtensions
{
	/// <summary>
	/// Converts the collection to a <see cref="BoxedOptionEnumerable{TValue}" />.
	/// </summary>
	/// <param name="self">The source collection.</param>
	/// <returns>A new <see cref="BoxedOptionEnumerable{TValue}" />.</returns>
	public static BoxedOptionEnumerable<TValue> AsBoxedOptionEnumerable<TValue>(
		this IEnumerable<IOption<TValue>> self
	) => new(self);

	/// <summary>
	/// Converts the collection to a <see cref="BoxedOptionEnumerable{TValue, TError}" />.
	/// </summary>
	/// <param name="self">The source collection.</param>
	/// <returns>A new <see cref="BoxedOptionEnumerable{TValue, TError}" />.</returns>
	public static BoxedOptionEnumerable<TValue, TError> AsBoxedOptionEnumerable<TValue, TError>(
		this IEnumerable<IOption<TValue, TError>> self
	) => new(self);

	/// <summary>
	/// Converts the collection to a <see cref="BoxedOptionEnumerable{TValue}" /> with optional
	/// ignored errors.
	/// </summary>
	/// <param name="self">The source collection.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A new <see cref="BoxedOptionEnumerable{TValue}" />.</returns>
	public static BoxedOptionEnumerable<TValue> AsBoxedOptionEnumerable<TValue>(
		this IEnumerable<IOption<TValue>> self, IEnumerable<string>? ignoredErrors
	) => new(self, ignoredErrors);

	/// <summary>
	/// Converts the collection to a <see cref="BoxedOptionEnumerable{TValue, TError}" /> with
	/// optional ignored errors.
	/// </summary>
	/// <param name="self">The source collection.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A new <see cref="BoxedOptionEnumerable{TValue, TError}" />.</returns>
	public static BoxedOptionEnumerable<TValue, TError> AsBoxedOptionEnumerable<TValue, TError>(
		this IEnumerable<IOption<TValue, TError>> self, IEnumerable<TError>? ignoredErrors
	) => new(self, ignoredErrors);
}
