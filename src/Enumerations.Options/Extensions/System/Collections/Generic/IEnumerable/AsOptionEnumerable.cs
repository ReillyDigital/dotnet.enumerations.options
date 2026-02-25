namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Extension methods for adding option-related functionality to the
/// <see cref="IEnumerable{TValue}" /> class.
/// </summary>
public static partial class IEnumerableExtensions
{
	/// <summary>
	/// Extension method to get the current collection as an
	/// <see cref="OptionEnumerable{TValue}" />.
	/// </summary>
	/// <param name="self">The source collection.</param>
	/// <returns>
	/// A new <see cref="OptionEnumerable{TValue}" /> containing the items from this collection.
	/// </returns>
	public static OptionEnumerable<TValue> AsOptionEnumerable<TValue>(
		this IEnumerable<Option<TValue>> self
	) => new(self);

	/// <summary>
	/// Extension method to get the current collection as an
	/// <see cref="OptionEnumerable{TValue}" /> with optional ignored errors.
	/// </summary>
	/// <param name="self">The source collection.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>
	/// A new <see cref="OptionEnumerable{TValue}" /> containing the items from this collection.
	/// </returns>
	public static OptionEnumerable<TValue> AsOptionEnumerable<TValue>(
		this IEnumerable<Option<TValue>> self, IEnumerable<string>? ignoredErrors
	) => new(self, ignoredErrors);

	/// <summary>
	/// Extension method to get the current collection as an
	/// <see cref="OptionEnumerable{TValue, TError}" />.
	/// </summary>
	/// <param name="self">The source collection.</param>
	/// <returns>
	/// A new <see cref="OptionEnumerable{TValue, TError}" /> containing the items from this
	/// collection.
	/// </returns>
	public static OptionEnumerable<TValue, TError> AsOptionEnumerable<TValue, TError>(
		this IEnumerable<Option<TValue, TError>> self
	) => new(self);

	/// <summary>
	/// Extension method to get the current collection as an
	/// <see cref="OptionEnumerable{TValue, TError}" /> with optional ignored errors.
	/// </summary>
	/// <param name="self">The source collection.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>
	/// A new <see cref="OptionEnumerable{TValue, TError}" /> containing the items from this
	/// collection.
	/// </returns>
	public static OptionEnumerable<TValue, TError> AsOptionEnumerable<TValue, TError>(
		this IEnumerable<Option<TValue, TError>> self, IEnumerable<TError>? ignoredErrors
	) => new(self, ignoredErrors);
}
