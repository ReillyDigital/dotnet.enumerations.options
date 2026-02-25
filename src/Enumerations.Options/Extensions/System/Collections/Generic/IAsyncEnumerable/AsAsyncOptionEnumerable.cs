namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Extension methods for adding option-related functionality to the
/// <see cref="IAsyncEnumerable{T}" /> class.
/// </summary>
public static partial class IAsyncEnumerableExtensions
{
	/// <summary>
	/// Extension method to get the current collection as an
	/// <see cref="AsyncOptionEnumerable{TValue}" />.
	/// </summary>
	/// <returns>
	/// A new <see cref="AsyncOptionEnumerable{TValue}" /> containing the items from this
	/// collection.
	/// </returns>
	public static AsyncOptionEnumerable<TValue> AsAsyncOptionEnumerable<TValue>(
		this IAsyncEnumerable<Option<TValue>> self
	) => new(self);

	/// <summary>
	/// Extension method to get the current collection as an
	/// <see cref="AsyncOptionEnumerable{TValue, TError}" />.
	/// </summary>
	/// <returns>
	/// A new <see cref="AsyncOptionEnumerable{TValue, TError}" /> containing the items from
	/// this collection.
	/// </returns>
	public static AsyncOptionEnumerable<TValue, TError> AsAsyncOptionEnumerable<TValue, TError>(
		this IAsyncEnumerable<Option<TValue, TError>> self
	) => new(self);
}

