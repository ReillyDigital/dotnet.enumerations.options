namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Extension methods for adding option-related functionality to the <see cref="IEnumerable{}" /> class.
/// </summary>
public static partial class IEnumerableExtensions
{
	/// <summary>
	/// Extension method to get the current collection as an <see cref="IOptionEnumerable{}" />.
	/// </summary>
	/// <returns>
	/// A new <see cref="IOptionEnumerable{}" /> containing the items from this collection wrapped as
	/// <see cref="ISome{}" /> values.
	/// </returns>
	public static IOptionEnumerable<TValue> AsOptionEnumerable<TValue>(this IEnumerable<IOption<TValue>> self)
		=> new OptionList<TValue>(self);

	/// <summary>
	/// Extension method to get the current collection as an <see cref="IOptionEnumerable{,}" />.
	/// </summary>
	/// <returns>
	/// A new <see cref="IOptionEnumerable{,}" /> containing the items from this collection wrapped as
	/// <see cref="ISome{,}" /> values.
	/// </returns>
	public static IOptionEnumerable<TValue, TError> AsOptionEnumerable<TValue, TError>(
		this IEnumerable<IOption<TValue, TError>> self
	) => new OptionList<TValue, TError>(self);
}
