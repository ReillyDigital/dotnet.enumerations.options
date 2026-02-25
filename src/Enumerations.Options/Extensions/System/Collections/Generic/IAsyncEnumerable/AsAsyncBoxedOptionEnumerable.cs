namespace ReillyDigital.Enumerations.Options;

using System.Collections.Generic;

/// <summary>
/// Extension methods for converting <see cref="IAsyncEnumerable{IOption{TValue}}" /> to
/// <see cref="AsyncBoxedOptionEnumerable{TValue}" />.
/// </summary>
public static partial class IAsyncEnumerableExtensions
{
	/// <summary>
	/// Converts the collection to an <see cref="AsyncBoxedOptionEnumerable{TValue}" />.
	/// </summary>
	/// <param name="self">The source collection.</param>
	/// <returns>A new <see cref="AsyncBoxedOptionEnumerable{TValue}" />.</returns>
	public static AsyncBoxedOptionEnumerable<TValue> AsAsyncBoxedOptionEnumerable<TValue>(
		this IAsyncEnumerable<IOption<TValue>> self
	) => new(self);

	/// <summary>
	/// Converts the collection to an <see cref="AsyncBoxedOptionEnumerable{TValue, TError}" />.
	/// </summary>
	/// <param name="self">The source collection.</param>
	/// <returns>A new <see cref="AsyncBoxedOptionEnumerable{TValue, TError}" />.</returns>
	public static AsyncBoxedOptionEnumerable<TValue, TError>
		AsAsyncBoxedOptionEnumerable<TValue, TError>(
			this IAsyncEnumerable<IOption<TValue, TError>> self
		) => new(self);

	/// <summary>
	/// Converts the collection to an <see cref="AsyncBoxedOptionEnumerable{TValue}" /> with
	/// optional ignored errors.
	/// </summary>
	/// <param name="self">The source collection.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A new <see cref="AsyncBoxedOptionEnumerable{TValue}" />.</returns>
	public static AsyncBoxedOptionEnumerable<TValue> AsAsyncBoxedOptionEnumerable<TValue>(
		this IAsyncEnumerable<IOption<TValue>> self, IEnumerable<string>? ignoredErrors
	) => new(self, ignoredErrors);

	/// <summary>
	/// Converts the collection to an <see cref="AsyncBoxedOptionEnumerable{TValue, TError}" />
	/// with optional ignored errors.
	/// </summary>
	/// <param name="self">The source collection.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	/// <returns>A new <see cref="AsyncBoxedOptionEnumerable{TValue, TError}" />.</returns>
	public static AsyncBoxedOptionEnumerable<TValue, TError>
		AsAsyncBoxedOptionEnumerable<TValue, TError>(
			this IAsyncEnumerable<IOption<TValue, TError>> self,
			IEnumerable<TError>? ignoredErrors
		) => new(self, ignoredErrors);
}

