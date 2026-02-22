namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Extension methods for adding option-related functionality to the
/// <see cref="IEnumerable{TValue}" /> class.
/// </summary>
public static partial class IEnumerableExtensions
{
	/// <summary>
	/// Extension method to get the current collection as an
	/// <see cref="IOptionEnumerable{TValue}" />.
	/// </summary>
	/// <returns>
	/// A new <see cref="IOptionEnumerable{TValue}" /> containing the items from this collection.
	/// </returns>
	public static IOptionEnumerable<TValue> AsOptionEnumerable<TValue>(
		this IEnumerable<IOption<TValue>> self
	) => new BoxedOptionEnumerable<TValue>(self);

	/// <summary>
	/// Extension method to get the current collection as an
	/// <see cref="IOptionEnumerable{TValue, TError}" />.
	/// </summary>
	/// <returns>
	/// A new <see cref="IOptionEnumerable{TValue, TError}" /> containing the items from this
	/// collection.
	/// </returns>
	public static IOptionEnumerable<TValue, TError> AsOptionEnumerable<TValue, TError>(
		this IEnumerable<IOption<TValue, TError>> self
	) => new BoxedOptionEnumerable<TValue, TError>(self);
}

internal sealed class BoxedOptionEnumerable<TValue>(IEnumerable<IOption<TValue>> inner)
	: IOptionEnumerable<TValue>
{
	public IEnumerable<IOption<TValue>> AsEnumerable() => inner;

	public void ForEach(Action<IOption<TValue>> handler)
	{
		foreach (var item in inner)
		{
			handler(item);
		}
	}

	public void ForEach<TResult>(Func<IOption<TValue>, TResult> handler)
	{
		foreach (var item in inner)
		{
			handler(item);
		}
	}

	public IEnumerator<IOption<TValue>> GetEnumerator() => inner.GetEnumerator();

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		=> inner.GetEnumerator();
}

internal sealed class BoxedOptionEnumerable<TValue, TError>(IEnumerable<IOption<TValue, TError>> inner)
	: IOptionEnumerable<TValue, TError>
{
	public IEnumerable<IOption<TValue, TError>> AsEnumerable() => inner;

	public void ForEach(Action<IOption<TValue, TError>> handler)
	{
		foreach (var item in inner)
		{
			handler(item);
		}
	}

	public void ForEach<TResult>(Func<IOption<TValue, TError>, TResult> handler)
	{
		foreach (var item in inner)
		{
			handler(item);
		}
	}

	public IEnumerator<IOption<TValue, TError>> GetEnumerator() => inner.GetEnumerator();

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		=> inner.GetEnumerator();
}
