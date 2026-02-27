namespace ReillyDigital.Enumerations.Options.Boxed;

/// <summary>
/// Represents a collection of options with a value of <see cref="TValue" /> that can be
/// individually accessed by index. Errors will be of type <see cref="string" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
public sealed class BoxedOptionList<TValue>
	: IList<IOption<TValue>>, IIgnoredErrorSet, IOptionEnumerable<TValue>
{
	/// <summary>
	/// Returns an empty collection.
	/// </summary>
	/// <returns>An empty <see cref="BoxedOptionList{TValue}" />.</returns>
	public static BoxedOptionList<TValue> Empty() => [];

	/// <inheritdoc />
	public IOption<TValue> this[int index]
	{
		get => List[index];
		set => List[index] = value;
	}

	/// <inheritdoc />
	public int Count => List.Count;

	/// <inheritdoc />
	public IEnumerable<string> IgnoredErrors { get; }

	/// <summary>
	/// The backing list for this collection.
	/// </summary>
	private List<IOption<TValue>> List { get; }

	/// <summary>
	/// Gets a value indicating whether the collection is read-only.
	/// </summary>
	/// <returns>false</returns>
	public bool IsReadOnly => false;

	/// <summary>
	/// Constructor for this collection, initially containing no items.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	public BoxedOptionList(IEnumerable<string>? ignoredErrors = null)
		=> (List, IgnoredErrors) = ([], ignoredErrors ?? []);

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	public BoxedOptionList(
		IEnumerable<IOption<TValue>> values, IEnumerable<string>? ignoredErrors = null
	) => (List, IgnoredErrors) = ([..values], ignoredErrors ?? []);

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	public BoxedOptionList(
		List<IOption<TValue>> values, IEnumerable<string>? ignoredErrors = null
	) => (List, IgnoredErrors) = (values, ignoredErrors ?? []);

	/// <summary>
	/// Adds an item to the end of the collection.
	/// </summary>
	/// <param name="item">The item to be added to the end of the collection.</param>
	public void Add(IOption<TValue> item) => List.Add(item);

	/// <summary>
	/// Adds the elements of the specified collection to the end of the List.
	/// </summary>
	/// <param name="collection">The items to be added to the collection.</param>
	/// <exception cref="ArgumentNullException" />
	public void AddRange(IEnumerable<IOption<TValue>> collection) => List.AddRange(collection);

	/// <summary>
	/// Adds the elements of the specified span to the end of the List.
	/// </summary>
	/// <param name="collection">The items to be added to the collection.</param>
	/// <exception cref="ArgumentNullException" />
	public void AddRange(ReadOnlySpan<IOption<TValue>> source) => List.AddRange(source);

	/// <inheritdoc />
	public IEnumerable<IOption<TValue>> AsEnumerable()
	{
		foreach (var each in this)
		{
			yield return each;
		}
	}

	/// <summary>
	/// Returns a read-only wrapper for the current collection.
	/// </summary>
	/// <returns>A new <see cref="ReadOnlyBoxedOptionList{TValue}" />
	/// wrapping this collection.</returns>
	public ReadOnlyBoxedOptionList<TValue> AsReadOnly() => new(this);

	/// <summary>
	/// Removes all items from the collection.
	/// </summary>
	/// <exception cref="NotSupportedException" />
	public void Clear() => List.Clear();

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
	{
		foreach (var each in this)
		{
			handler(each);
		}
	}

	/// <inheritdoc />
	public IEnumerator<IOption<TValue>> GetEnumerator() => List.GetEnumerator();

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
	public int IndexOf(IOption<TValue> item) => List.IndexOf(item);

	/// <summary>
	/// Inserts an element into the collection at the specified index.
	/// </summary>
	/// <param name="index">
	/// The zero-based index at which item should be inserted. Inserts an element into the
	/// collection at the specified index.
	/// </param>
	/// <param name="item">The item to insert into the collection.</param>
	/// <exception cref="ArgumentOutOfRangeException" />
	public void Insert(int index, IOption<TValue> item) => List.Insert(index, item);

	/// <summary>
	/// Removes the first occurrence of a specific object from the collection.
	/// </summary>
	/// <param name="item">The item to remove from the collection.</param>
	/// <returns>
	/// true if item is successfully removed; otherwise, false. This method also returns false if
	/// item was not found in the collection.
	/// </returns>
	public bool Remove(IOption<TValue> item) => List.Remove(item);

	/// <summary>
	/// Removes all the elements that match the conditions defined by the specified predicate.
	/// </summary>
	/// <param name="match">
	/// Delegate that defines the conditions of the elements to remove. Removes all the elements
	/// that match the conditions defined by the specified predicate.
	/// </param>
	/// <returns>The number of elements removed from the List.</returns>
	/// <exception cref="ArgumentNullException" />
	public int RemoveAll(Predicate<IOption<TValue>> match) => List.RemoveAll(match);

	/// <summary>
	/// Removes the element at the specified index of the collection.
	/// </summary>
	/// <param name="index">Determines the index of a specific item in the collection.</param>
	/// <exception cref="ArgumentOutOfRangeException" />
	public void RemoveAt(int index) => List.RemoveAt(index);

	/// <summary>
	/// Convert this collection to an <see cref="OptionList{TValue}" />.
	/// </summary>
	/// <returns>An <see cref="OptionList{TValue}" />.</returns>
	public OptionList<TValue> ToUnboxed() => new(List.Select(Option<TValue>.Unbox), IgnoredErrors);
}

/// <summary>
/// Represents a collection of options with a value of <see cref="TValue" /> that can be
/// individually accessed by index. Errors will be of type <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
public sealed class BoxedOptionList<TValue, TError>
	: IList<IOption<TValue, TError>>, IIgnoredErrorSet<TError>, IOptionEnumerable<TValue, TError>
{
	/// <summary>
	/// Returns an empty collection.
	/// </summary>
	/// <returns>An empty <see cref="BoxedOptionList{TValue, TError}" />.</returns>
	public static BoxedOptionList<TValue, TError> Empty() => [];

	/// <inheritdoc />
	public IOption<TValue, TError> this[int index]
	{
		get => List[index];
		set => List[index] = value;
	}

	/// <inheritdoc />
	public int Count => List.Count;

	/// <inheritdoc />
	public IEnumerable<TError> IgnoredErrors { get; }

	/// <summary>
	/// The backing list for this collection.
	/// </summary>
	private List<IOption<TValue, TError>> List { get; }

	/// <summary>
	/// Gets a value indicating whether the collection is read-only.
	/// </summary>
	/// <returns>false</returns>
	public bool IsReadOnly => false;

	/// <summary>
	/// Constructor for this collection, initially containing no items.
	/// </summary>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	public BoxedOptionList(IEnumerable<TError>? ignoredErrors = null)
		=> (List, IgnoredErrors) = ([], ignoredErrors ?? []);

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	public BoxedOptionList(
		IEnumerable<IOption<TValue, TError>> values, IEnumerable<TError>? ignoredErrors = null
	) => (List, IgnoredErrors) = ([..values], ignoredErrors ?? []);

	/// <summary>
	/// Constructor for this collection, having its items set to the provided values.
	/// </summary>
	/// <param name="values">The items of the new collection.</param>
	/// <param name="ignoredErrors">
	/// Errors that are ignored instead of being returned as the option value.
	/// </param>
	public BoxedOptionList(
		List<IOption<TValue, TError>> values, IEnumerable<TError>? ignoredErrors = null
	) => (List, IgnoredErrors) = (values, ignoredErrors ?? []);

	/// <summary>
	/// Adds an item to the end of the collection.
	/// </summary>
	/// <param name="item">The item to be added to the end of the collection.</param>
	public void Add(IOption<TValue, TError> item) => List.Add(item);

	/// <summary>
	/// Adds the elements of the specified collection to the end of the List.
	/// </summary>
	/// <param name="collection">The items to be added to the collection.</param>
	/// <exception cref="ArgumentNullException" />
	public void AddRange(IEnumerable<IOption<TValue, TError>> collection)
		=> List.AddRange(collection);

	/// <summary>
	/// Adds the elements of the specified span to the end of the List.
	/// </summary>
	/// <param name="collection">The items to be added to the collection.</param>
	/// <exception cref="ArgumentNullException" />
	public void AddRange(ReadOnlySpan<IOption<TValue, TError>> source) => List.AddRange(source);

	/// <inheritdoc />
	public IEnumerable<IOption<TValue, TError>> AsEnumerable()
	{
		foreach (var each in this)
		{
			yield return each;
		}
	}

	/// <summary>
	/// Returns a read-only wrapper for the current collection.
	/// </summary>
	/// <returns>
	/// A new <see cref="ReadOnlyBoxedOptionList{TValue, TError}" /> wrapping this collection.
	/// </returns>
	public ReadOnlyBoxedOptionList<TValue, TError> AsReadOnly() => new(this);

	/// <summary>
	/// Removes all items from the collection.
	/// </summary>
	/// <exception cref="NotSupportedException" />
	public void Clear() => List.Clear();

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
	{
		foreach (var each in this)
		{
			handler(each);
		}
	}

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

	/// <summary>
	/// Inserts an element into the collection at the specified index.
	/// </summary>
	/// <param name="index">
	/// The zero-based index at which item should be inserted. Inserts an element into the
	/// collection at the specified index.
	/// </param>
	/// <param name="item">The item to insert into the collection.</param>
	/// <exception cref="ArgumentOutOfRangeException" />
	public void Insert(int index, IOption<TValue, TError> item) => List.Insert(index, item);

	/// <summary>
	/// Removes the first occurrence of a specific object from the collection.
	/// </summary>
	/// <param name="item">The item to remove from the collection.</param>
	/// <returns>
	/// true if item is successfully removed; otherwise, false. This method also returns false if
	/// item was not found in the collection.
	/// </returns>
	public bool Remove(IOption<TValue, TError> item) => List.Remove(item);

	/// <summary>
	/// Removes all the elements that match the conditions defined by the specified predicate.
	/// </summary>
	/// <param name="match">
	/// Delegate that defines the conditions of the elements to remove. Removes all the elements
	/// that match the conditions defined by the specified predicate.
	/// </param>
	/// <returns>The number of elements removed from the List.</returns>
	/// <exception cref="ArgumentNullException" />
	public int RemoveAll(Predicate<IOption<TValue, TError>> match) => List.RemoveAll(match);

	/// <summary>
	/// Removes the element at the specified index of the collection.
	/// </summary>
	/// <param name="index">Determines the index of a specific item in the collection.</param>
	/// <exception cref="ArgumentOutOfRangeException" />
	public void RemoveAt(int index) => List.RemoveAt(index);

	/// <summary>
	/// Convert this collection to an <see cref="OptionList{TValue, TError}" />.
	/// </summary>
	/// <returns>An <see cref="OptionList{TValue, TError}" />.</returns>
	public OptionList<TValue, TError> ToUnboxed()
		=> new(List.Select(Option<TValue, TError>.Unbox), IgnoredErrors);
}
