#pragma warning disable SYSLIB0050

namespace ReillyDigital.Enumerations.Options._Internal;

using System.Text.Json;

/// <summary>
/// Represents a collection of ignored errors.
/// </summary>
internal interface IIgnoredErrorSet
{
	/// <summary>
	/// Additional errors of <see cref="Exception" /> which are ignored instead of being returned as
	/// the option value.
	/// </summary>
	public IEnumerable<Exception> IgnoredErrors { get; }
}

/// <summary>
/// Represents a collection of ignored errors.
/// </summary>
/// <typeparam name="TError">The type of the errors.</typeparam>
internal interface IIgnoredErrorSet<out TError> : IIgnoredErrorSet
{
	/// <summary>
	/// Additional errors of <see cref="TError" /> which are ignored instead of being returned as
	/// the option value.
	/// </summary>
	public new IEnumerable<TError> IgnoredErrors { get; }

	/// <inheritdoc />
	IEnumerable<Exception> IIgnoredErrorSet.IgnoredErrors => IgnoredErrors.Select(
		(error) => error switch
		{
			Exception exception => exception,
			_ => new(
				$"Value is an error of type {typeof(TError).FullName}.",
				typeof(TError).IsSerializable ? new(JsonSerializer.Serialize(error)) : null
			)
		}
	);
}
