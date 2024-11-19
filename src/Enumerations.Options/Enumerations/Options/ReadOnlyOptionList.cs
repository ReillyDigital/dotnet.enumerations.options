namespace ReillyDigital.Enumerations.Options;

/// <summary>
/// Represents a read-only collection of options with a value of <see cref="TValue" /> that can be individually
/// accessed by index. Errors will be of type <see cref="Exception" />.
/// </summary>
public sealed class ReadOnlyOptionList<TValue> : IOptionEnumerable<TValue>, IReadOnlyList<IOption<TValue>>
{
	/// <inheritdoc />
	public IOption<TValue> this[int index] => List[index];

	/// <summary>
	/// Gets the number of elements contained in the collection.
	/// </summary>
	/// <returns>The number of elements contained in the Collection.</returns>
	public int Count => List.Count;

	/// <summary>
	/// The enumerator for the collection.
	/// </summary>
	private IEnumerator<IOption<TValue>> IEnumerator => List.GetEnumerator();

	/// <summary>
	/// The backing option list for this collection.
	/// </summary>
	private OptionList<TValue> List { get; }

	/// <inheritdoc />
	IEnumerator<IOption<TValue>> IOptionEnumerable<TValue>.IEnumerator => IEnumerator;

	/// <summary>
	/// Gets a value indicating whether the collection is read-only.
	/// </summary>
	/// <returns>true</returns>
	public bool IsReadOnly => true;

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyOptionList(IEnumerable<IOption<TValue>> values) => List = new(values);

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyOptionList(List<IOption<TValue>> values) => List = new OptionList<TValue>(values);

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyOptionList(OptionList<TValue> values) => List = values;

	/// <inheritdoc />
	public IEnumerable<IOption<TValue>> AsEnumerable() => List.AsEnumerable();

	/// <summary>
	/// Determines whether the collection contains a specific value.
	/// </summary>
	/// <param name="item">The item to locate in the collection.</param>
	/// <returns>true if item is found in the List; otherwise, false.</returns>
	public bool Contains(IOption<TValue> item) => List.Contains(item);

	/// <summary>
	/// Copies the entire collection to a compatible one-dimensional array, starting at the specified index of the
	/// target array.
	/// </summary>
	/// <param name="array">
	/// The one-dimensional Array that is the destination of the elements copied from this collection. The Array must
	/// have zero-based indexing. Copies the entire collection to a compatible one-dimensional array, starting at the
	/// specified index of the target array.
	/// </param>
	/// <param name="arrayIndex">
	/// The zero-based index in array at which copying begins. Copies the entire collection to a compatible
	/// one-dimensional array, starting at the specified index of the target array.
	/// </param>
	/// <exception cref="ArgumentNullException" />
	/// <exception cref="ArgumentOutOfRangeException" />
	/// <exception cref="ArgumentException" />
	public void CopyTo(IOption<TValue>[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);

	/// <inheritdoc />
	public void ForEach(Action<IOption<TValue>> handler) => List.ForEach(handler);

	/// <inheritdoc />
	public void ForEach<TResult>(Func<IOption<TValue>, TResult> handler) => List.ForEach(handler);

	/// <inheritdoc />
	public IEnumerator<IOption<TValue>> GetEnumerator() => IEnumerator;

	/// <summary>
	/// Searches for the specified item and returns the zero-based index of the first occurrence within the entire
	/// collection.
	/// </summary>
	/// <param name="item">
	/// The item to locate in the collection. Searches for the specified item and returns the zero-based index of the
	/// first occurrence within the collection.
	/// </param>
	/// <returns>
	/// The zero-based index of the first occurrence of item within the collection, if found; otherwise, -1.
	/// </returns>
	public int IndexOf(IOption<TValue> item) => List.IndexOf(item);
}

/// <summary>
/// Represents a read-only collection of options with a value of <see cref="TValue" /> that can be individually
/// accessed by index. Errors will be of type <see cref="TError" />.
/// </summary>
public sealed class ReadOnlyOptionList<TValue, TError> : IOptionEnumerable<TValue, TError>, IReadOnlyList<IOption<TValue, TError>>
{
	/// <inheritdoc />
	public IOption<TValue, TError> this[int index] => List[index];

	/// <summary>
	/// Gets the number of elements contained in the collection.
	/// </summary>
	/// <returns>The number of elements contained in the Collection.</returns>
	public int Count => List.Count;

	/// <summary>
	/// The enumerator for the collection.
	/// </summary>
	private IEnumerator<IOption<TValue, TError>> IEnumerator => List.GetEnumerator();

	/// <summary>
	/// The backing option list for this collection.
	/// </summary>
	private OptionList<TValue, TError> List { get; }

	/// <inheritdoc />
	IEnumerator<IOption<TValue, TError>> IOptionEnumerable<TValue, TError>.IEnumerator => IEnumerator;

	/// <summary>
	/// Gets a value indicating whether the collection is read-only.
	/// </summary>
	/// <returns>true</returns>
	public bool IsReadOnly => true;

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyOptionList(IEnumerable<IOption<TValue, TError>> values) => List = new(values);

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyOptionList(List<IOption<TValue, TError>> values) => List = new OptionList<TValue, TError>(values);

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	public ReadOnlyOptionList(OptionList<TValue, TError> values) => List = values;

	/// <inheritdoc />
	public IEnumerable<IOption<TValue, TError>> AsEnumerable() => List.AsEnumerable();

	/// <summary>
	/// Determines whether the collection contains a specific value.
	/// </summary>
	/// <param name="item">The item to locate in the collection.</param>
	/// <returns>true if item is found in the List; otherwise, false.</returns>
	public bool Contains(IOption<TValue, TError> item) => List.Contains(item);

	/// <summary>
	/// Copies the entire collection to a compatible one-dimensional array, starting at the specified index of the
	/// target array.
	/// </summary>
	/// <param name="array">
	/// The one-dimensional Array that is the destination of the elements copied from this collection. The Array must
	/// have zero-based indexing. Copies the entire collection to a compatible one-dimensional array, starting at the
	/// specified index of the target array.
	/// </param>
	/// <param name="arrayIndex">
	/// The zero-based index in array at which copying begins. Copies the entire collection to a compatible
	/// one-dimensional array, starting at the specified index of the target array.
	/// </param>
	/// <exception cref="ArgumentNullException" />
	/// <exception cref="ArgumentOutOfRangeException" />
	/// <exception cref="ArgumentException" />
	public void CopyTo(IOption<TValue, TError>[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);

	/// <inheritdoc />
	public void ForEach(Action<IOption<TValue, TError>> handler) => List.ForEach(handler);

	/// <inheritdoc />
	public void ForEach<TResult>(Func<IOption<TValue, TError>, TResult> handler) => List.ForEach(handler);

	/// <inheritdoc />
	public IEnumerator<IOption<TValue, TError>> GetEnumerator() => IEnumerator;

	/// <summary>
	/// Searches for the specified item and returns the zero-based index of the first occurrence within the entire
	/// collection.
	/// </summary>
	/// <param name="item">
	/// The item to locate in the collection. Searches for the specified item and returns the zero-based index of the
	/// first occurrence within the collection.
	/// </param>
	/// <returns>
	/// The zero-based index of the first occurrence of item within the collection, if found; otherwise, -1.
	/// </returns>
	public int IndexOf(IOption<TValue, TError> item) => List.IndexOf(item);
}
