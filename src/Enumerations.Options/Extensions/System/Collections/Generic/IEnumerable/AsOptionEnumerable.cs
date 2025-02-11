namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Extension methods for adding option-related functionality to the <see cref="IEnumerable{TValue}" />
/// class.
/// </summary>
public static partial class IEnumerableExtensions
{
	/// <summary>
	/// Extension method to get the current collection as an <see cref="IOptionEnumerable{TValue}" />.
	/// </summary>
	/// <returns>
	/// A new <see cref="IOptionEnumerable{TValue}" /> containing the items from this collection wrapped
	/// as <see cref="ISome{TValue}" /> values.
	/// </returns>
	public static IOptionEnumerable<TValue> AsOptionEnumerable<TValue>(
		this IEnumerable<IOption<TValue>> self
	) => new OptionList<TValue>(self);

	/// <summary>
	/// Extension method to get the current collection as an <see cref="IOptionEnumerable{TValue, TError}" />.
	/// </summary>
	/// <returns>
	/// A new <see cref="IOptionEnumerable{TValue, TError}" /> containing the items from this collection wrapped
	/// as <see cref="ISome{TValue, TError}" /> values.
	/// </returns>
	public static IOptionEnumerable<TValue, TError> AsOptionEnumerable<TValue, TError>(
		this IEnumerable<IOption<TValue, TError>> self
	) => new OptionList<TValue, TError>(self);
}
