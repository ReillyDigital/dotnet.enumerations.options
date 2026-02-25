namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a read-only collection of options with a value of <see cref="TValue" /> that can be
/// individually accessed by index. Errors will be of type <see cref="string" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public sealed class ReadOnlyBoxedOptionList<TValue>
	: IOptionEnumerable<TValue>, IReadOnlyList<IOption<TValue>>
{
	/// <inheritdoc />
	public IOption<TValue> this[int index] => List[index];

	/// <summary>
	/// Gets the number of elements contained in the collection.
	/// </summary>
	/// <returns>The number of elements contained in the Collection.</returns>
	public int Count => List.Count;

	/// <inheritdoc />
	public IEnumerable<string> IgnoredErrors => List.IgnoredErrors;

	/// <summary>
	/// Gets a value indicating whether the collection is read-only.
	/// </summary>
	/// <returns>true</returns>
	public bool IsReadOnly => true;

	/// <summary>
	/// The backing option list for this collection.
	/// </summary>
	private BoxedOptionList<TValue> List { get; }

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyBoxedOptionList(IEnumerable<IOption<TValue>> values) => List = [..values];

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyBoxedOptionList(List<IOption<TValue>> values) => List = [..values];

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyBoxedOptionList(BoxedOptionList<TValue> values) => List = values;

	/// <inheritdoc />
	public IEnumerable<IOption<TValue>> AsEnumerable() => List.AsEnumerable();

	/// <summary>
	/// Determines whether the collection contains a specific value.
	/// </summary>
	/// <param name="item">The item to locate in the collection.</param>
	/// <returns>true if item is found in the List; otherwise, false.</returns>
	public bool Contains(IOption<TValue> item) => List.Contains(item);

	/// <summary>
	/// Copies the entire collection to a compatible one-dimensional array, starting at the
	/// specified index of the target array.
	/// </summary>
	/// <param name="array">
	/// The one-dimensional Array that is the destination of the elements copied from this
	/// collection. The Array must have zero-based indexing. Copies the entire collection to a
	/// compatible one-dimensional array, starting at the specified index of the target array.
	/// </param>
	/// <param name="arrayIndex">
	/// The zero-based index in array at which copying begins. Copies the entire collection to a
	/// compatible one-dimensional array, starting at the specified index of the target array.
	/// </param>
	/// <exception cref="ArgumentNullException" />
	/// <exception cref="ArgumentOutOfRangeException" />
	/// <exception cref="ArgumentException" />
	public void CopyTo(IOption<TValue>[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);

	/// <inheritdoc />
	public void ForEach(Action<IOption<TValue>> handler) => List.ForEach(handler);

	/// <inheritdoc />
	public void ForEach<TResult>(Func<IOption<TValue>, TResult> handler)
		=> List.ForEach(handler);

	/// <inheritdoc />
	public IEnumerator<IOption<TValue>> GetEnumerator() => List.GetEnumerator();

	/// <summary>
	/// Searches for the specified item and returns the zero-based index of the first occurrence
	/// within the entire collection.
	/// </summary>
	/// <param name="item">
	/// The item to locate in the collection. Searches for the specified item and returns the zero
	/// based index of the first occurrence within the collection.
	/// </param>
	/// <returns>
	/// The zero-based index of the first occurrence of item within the collection, if found;
	/// otherwise, -1.
	/// </returns>
	public int IndexOf(IOption<TValue> item) => List.IndexOf(item);
}

/// <summary>
/// Represents a read-only collection of options with a value of <see cref="TValue" /> that can be
/// individually accessed by index. Errors will be of type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public sealed class ReadOnlyBoxedOptionList<TValue, TError>
	: IOptionEnumerable<TValue, TError>, IReadOnlyList<IOption<TValue, TError>>
{
	/// <inheritdoc />
	public IOption<TValue, TError> this[int index] => List[index];

	/// <summary>
	/// Gets the number of elements contained in the collection.
	/// </summary>
	/// <returns>The number of elements contained in the Collection.</returns>
	public int Count => List.Count;

	/// <inheritdoc />
	public IEnumerable<TError> IgnoredErrors => List.IgnoredErrors;

	/// <summary>
	/// Gets a value indicating whether the collection is read-only.
	/// </summary>
	/// <returns>true</returns>
	public bool IsReadOnly => true;

	/// <summary>
	/// The backing option list for this collection.
	/// </summary>
	private BoxedOptionList<TValue, TError> List { get; }

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyBoxedOptionList(IEnumerable<IOption<TValue, TError>> values)
		=> List = [..values];

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyBoxedOptionList(List<IOption<TValue, TError>> values) => List = [..values];

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyBoxedOptionList(BoxedOptionList<TValue, TError> values) => List = values;

	/// <inheritdoc />
	public IEnumerable<IOption<TValue, TError>> AsEnumerable() => List.AsEnumerable();

	/// <summary>
	/// Determines whether the collection contains a specific value.
	/// </summary>
	/// <param name="item">The item to locate in the collection.</param>
	/// <returns>true if item is found in the List; otherwise, false.</returns>
	public bool Contains(IOption<TValue, TError> item) => List.Contains(item);

	/// <summary>
	/// Copies the entire collection to a compatible one-dimensional array, starting at the
	/// specified index of the target array.
	/// </summary>
	/// <param name="array">
	/// The one-dimensional Array that is the destination of the elements copied from this
	/// collection. The Array must have zero-based indexing. Copies the entire collection to a
	/// compatible one-dimensional array, starting at the specified index of the target array.
	/// </param>
	/// <param name="arrayIndex">
	/// The zero-based index in array at which copying begins. Copies the entire collection to a
	/// compatible one-dimensional array, starting at the specified index of the target array.
	/// </param>
	/// <exception cref="ArgumentNullException" />
	/// <exception cref="ArgumentOutOfRangeException" />
	/// <exception cref="ArgumentException" />
	public void CopyTo(IOption<TValue, TError>[] array, int arrayIndex)
		=> List.CopyTo(array, arrayIndex);

	/// <inheritdoc />
	public void ForEach(Action<IOption<TValue, TError>> handler) => List.ForEach(handler);

	/// <inheritdoc />
	public void ForEach<TResult>(Func<IOption<TValue, TError>, TResult> handler)
		=> List.ForEach(handler);

	/// <inheritdoc />
	public IEnumerator<IOption<TValue, TError>> GetEnumerator() => List.GetEnumerator();

	/// <summary>
	/// Searches for the specified item and returns the zero-based index of the first occurrence
	/// within the entire collection.
	/// </summary>
	/// <param name="item">
	/// The item to locate in the collection. Searches for the specified item and returns the
	/// zero-based index of the first occurrence within the collection.
	/// </param>
	/// <returns>
	/// The zero-based index of the first occurrence of item within the collection, if found;
	/// otherwise, -1.
	/// </returns>
	public int IndexOf(IOption<TValue, TError> item) => List.IndexOf(item);
}
