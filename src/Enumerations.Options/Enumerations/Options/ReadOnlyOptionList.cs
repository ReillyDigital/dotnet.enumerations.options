namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a read-only collection of options with a value of <see cref="TValue" /> that can be
/// individually accessed by index. Errors will be of type <see cref="ErrorValue" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public sealed class ReadOnlyOptionList<TValue> : IReadOnlyList<Option<TValue>>
{
	/// <inheritdoc />
	public Option<TValue> this[int index] => List[index];

	/// <summary>
	/// Gets the number of elements contained in the collection.
	/// </summary>
	/// <returns>The number of elements contained in the Collection.</returns>
	public int Count => List.Count;

	/// <summary>
	/// Errors that are ignored instead of being returned as the option value.
	/// </summary>
	public IEnumerable<ErrorValue> IgnoredErrors => List.IgnoredErrors;

	/// <summary>
	/// Gets a value indicating whether the collection is read-only.
	/// </summary>
	/// <returns>true</returns>
	public bool IsReadOnly => true;

	/// <summary>
	/// The backing option list for this collection.
	/// </summary>
	private OptionList<TValue> List { get; }

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyOptionList(IEnumerable<Option<TValue>> values) => List = [..values];

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyOptionList(List<Option<TValue>> values) => List = [..values];

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyOptionList(OptionList<TValue> values) => List = values;

	/// <summary>
	/// Returns an enumerable of the items in this collection.
	/// </summary>
	/// <returns>An enumerable of the items in this collection.</returns>
	public IEnumerable<Option<TValue>> AsEnumerable() => List.AsEnumerable();

	/// <summary>
	/// Determines whether the collection contains a specific value.
	/// </summary>
	/// <param name="item">The item to locate in the collection.</param>
	/// <returns>true if item is found in the List; otherwise, false.</returns>
	public bool Contains(Option<TValue> item) => List.Contains(item);

	/// <summary>
	/// Copies the entire collection to a compatible one-dimensional array, starting at the
	/// specified index of the target array.
	/// </summary>
	/// <param name="array">
	/// The one-dimensional Array that is the destination of the elements copied from this
	/// collection. The Array must have zero-based indexing.
	/// </param>
	/// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
	/// <exception cref="ArgumentNullException" />
	/// <exception cref="ArgumentOutOfRangeException" />
	/// <exception cref="ArgumentException" />
	public void CopyTo(Option<TValue>[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);

	/// <summary>
	/// Performs the specified action on each element of the collection.
	/// </summary>
	/// <param name="handler">The action to perform on each element.</param>
	public void ForEach(Action<Option<TValue>> handler) => List.ForEach(handler);

	/// <summary>
	/// Performs the specified function on each element of the collection.
	/// </summary>
	/// <param name="handler">The function to perform on each element.</param>
	/// <typeparam name="TResult">The return type of the function.</typeparam>
	public void ForEach<TResult>(Func<Option<TValue>, TResult> handler) => List.ForEach(handler);

	/// <inheritdoc />
	public IEnumerator<Option<TValue>> GetEnumerator() => List.GetEnumerator();

	/// <summary>
	/// Searches for the specified item and returns the zero-based index of the first occurrence
	/// within the entire collection.
	/// </summary>
	/// <param name="item">The item to locate in the collection.</param>
	/// <returns>
	/// The zero-based index of the first occurrence of item within the collection, if found;
	/// otherwise, -1.
	/// </returns>
	public int IndexOf(Option<TValue> item) => List.IndexOf(item);

	/// <inheritdoc />
	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		=> List.GetEnumerator();
}

/// <summary>
/// Represents a read-only collection of options with a value of <see cref="TValue" /> that can be
/// individually accessed by index. Errors will be of type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public sealed class ReadOnlyOptionList<TValue, TError> : IReadOnlyList<Option<TValue, TError>>
{
	/// <inheritdoc />
	public Option<TValue, TError> this[int index] => List[index];

	/// <summary>
	/// Gets the number of elements contained in the collection.
	/// </summary>
	/// <returns>The number of elements contained in the Collection.</returns>
	public int Count => List.Count;

	/// <summary>
	/// Errors that are ignored instead of being returned as the option value.
	/// </summary>
	public IEnumerable<TError> IgnoredErrors => List.IgnoredErrors;

	/// <summary>
	/// Gets a value indicating whether the collection is read-only.
	/// </summary>
	/// <returns>true</returns>
	public bool IsReadOnly => true;

	/// <summary>
	/// The backing option list for this collection.
	/// </summary>
	private OptionList<TValue, TError> List { get; }

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyOptionList(IEnumerable<Option<TValue, TError>> values) => List = [..values];

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyOptionList(List<Option<TValue, TError>> values) => List = [..values];

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyOptionList(OptionList<TValue, TError> values) => List = values;

	/// <summary>
	/// Returns an enumerable of the items in this collection.
	/// </summary>
	/// <returns>An enumerable of the items in this collection.</returns>
	public IEnumerable<Option<TValue, TError>> AsEnumerable() => List.AsEnumerable();

	/// <summary>
	/// Determines whether the collection contains a specific value.
	/// </summary>
	/// <param name="item">The item to locate in the collection.</param>
	/// <returns>true if item is found in the List; otherwise, false.</returns>
	public bool Contains(Option<TValue, TError> item) => List.Contains(item);

	/// <summary>
	/// Copies the entire collection to a compatible one-dimensional array, starting at the
	/// specified index of the target array.
	/// </summary>
	/// <param name="array">
	/// The one-dimensional Array that is the destination of the elements copied from this
	/// collection. The Array must have zero-based indexing.
	/// </param>
	/// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
	/// <exception cref="ArgumentNullException" />
	/// <exception cref="ArgumentOutOfRangeException" />
	/// <exception cref="ArgumentException" />
	public void CopyTo(Option<TValue, TError>[] array, int arrayIndex)
		=> List.CopyTo(array, arrayIndex);

	/// <summary>
	/// Performs the specified action on each element of the collection.
	/// </summary>
	/// <param name="handler">The action to perform on each element.</param>
	public void ForEach(Action<Option<TValue, TError>> handler) => List.ForEach(handler);

	/// <summary>
	/// Performs the specified function on each element of the collection.
	/// </summary>
	/// <param name="handler">The function to perform on each element.</param>
	/// <typeparam name="TResult">The return type of the function.</typeparam>
	public void ForEach<TResult>(Func<Option<TValue, TError>, TResult> handler)
		=> List.ForEach(handler);

	/// <inheritdoc />
	public IEnumerator<Option<TValue, TError>> GetEnumerator() => List.GetEnumerator();

	/// <summary>
	/// Searches for the specified item and returns the zero-based index of the first occurrence
	/// within the entire collection.
	/// </summary>
	/// <param name="item">The item to locate in the collection.</param>
	/// <returns>
	/// The zero-based index of the first occurrence of item within the collection, if found;
	/// otherwise, -1.
	/// </returns>
	public int IndexOf(Option<TValue, TError> item) => List.IndexOf(item);

	/// <inheritdoc />
	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		=> List.GetEnumerator();
}
