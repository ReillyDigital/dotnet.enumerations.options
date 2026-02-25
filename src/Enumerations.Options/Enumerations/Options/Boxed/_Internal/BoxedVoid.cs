namespace ReillyDigital.Enumerations.Options.Boxed._Internal;

/// <summary>
/// Represents the simplest return type of nothing or anything.
/// </summary>
/// <typeparam name="TError"><The type of the error of the options.></typeparam>
/// <param name="ignoredErrors">
/// Errors that are ignored instead of being returned as the option value.
/// </param>
internal sealed class BoxedVoid<TError>(IEnumerable<TError>? ignoredErrors = null)
	: IIgnoredErrorSet<TError>, IVoid<TError>
{
	/// <summary>
	/// Static default reference for this option.
	/// </summary>
	public static readonly BoxedVoid<TError> Ref = new();

	/// <inheritdoc />
	public IEnumerable<TError> IgnoredErrors => ignoredErrors ?? [];

	/// <inheritdoc />
	public override bool Equals(object? obj) => obj is BoxedVoid<TError>;

	/// <inheritdoc />
	public override int GetHashCode() => base.GetHashCode();
}
