namespace ReillyDigital.Enumerations.Options._Internal;

/// <summary>
/// Represents an option which has an error of <see cref="TError" />.
/// </summary>
/// <typeparam name="TValue">The type of the value of the options.</typeparam>
/// <typeparam name="TError">The type of the error of the options.</typeparam>
/// <param name="value">The error of the options.</param>
/// <param name="ignoredErrors">
/// Errors that are ignored instead of being returned as the option value.
/// </param>
internal sealed class BoxedError<TValue, TError>(
	TError? value, IEnumerable<TError>? ignoredErrors = null
) : IError<TValue>, IError<TValue, TError>, IIgnoredErrorSet<TError>
{
	private static BoxedError<TValue, TError>? _ref;

	/// <summary>
	/// Static default reference for error with empty message and no ignored errors.
	/// Only supported when <typeparamref name="TError" /> is <see cref="string" />.
	/// </summary>
	public static BoxedError<TValue, TError> Ref =>
		_ref ??= typeof(TError) == typeof(string)
			? (BoxedError<TValue, TError>)(object)new BoxedError<TValue, string>("", null)
			: throw new InvalidOperationException();

	/// <inheritdoc />
	public IEnumerable<TError> IgnoredErrors => ignoredErrors ?? [];

	/// <inheritdoc />
	public TError Value => value ?? throw new InvalidOperationException();

	/// <inheritdoc />
	string IError<TValue>.Value => ((IError)this).Value;

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	TValue? IOption<TValue>.Value => throw new Exception(((IError)this).Value);

	/// <summary>
	/// Throws the value of the error as returned by <see cref="IError.Value" />.
	/// </summary>
	TValue? IOption<TValue, TError>.Value => throw new Exception(((IError)this).Value);

	/// <inheritdoc />
	public override bool Equals(object? obj)
		=> obj is BoxedError<TValue, TError> error
		&& ((error.Value?.Equals(Value) ?? false) || (error.Value is null && Value is null));

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}
