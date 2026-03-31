# Changelog

All notable changes to this project will be documented in this file.

## [Unreleased]

## [2.0.0-alpha.15] - 2026-03-30

### Changed

- Improved implicit/explicit cast operators.

## [2.0.0-alpha.14] - 2026-03-17

### Changed

- Remove boxed interface derivations on non-boxed types.

## [2.0.0-alpha.13] - 2026-03-17

### Changed

- Fixed return types for OptionEnumerable helper methods which were incorrectly returning boxed types.

## [2.0.0-alpha.12] - 2026-02-28

### Changed

- Nuget README fix to point to root README.

## [2.0.0-alpha.11] - 2026-02-28

### Added

- `BoxedSome`, `BoxedNone`, and `BoxedError` factory methods on struct types for direct boxed creation.
- `Unbox()` static method on struct types for converting boxed interfaces back to structs.
- Singleton `Ref` values for boxed `None` and `Error` types to reduce heap allocations.

### Changed

- Boxed interface types moved to `ReillyDigital.Enumerations.Options.Boxed` sub-namespace.
- Removed `ErrorValue` wrapper type; base error value is now `string` directly.
- Updated sample project with `SimpleOptionScenario`, `BoxedOptionScenario`, and `CovariantOptionScenario`.
- Updated README with improved description and examples.
- Removed deprecated `Summary` property from csproj.

## [2.0.0-alpha.10] - 2026-02-22

### Changed

- Major rework to add concrete Option and Void type for zero-allocation usage.
- Providing `ToBoxed()` method on new concrete types to produce the old interface-based values.
- Internal concrete values for the interface boxing now renamed w/ `Boxed` prefix.
- Helper methods in `OptionsFunctions` now use the new concrete types.

## [2.0.0-alpha.9] - 2025-08-24

### Changed

- Fix return type of `Some<T>` helper functions to return a `ISome<T>` instead of a `IOption<T>`.
- Use MIT license for pending 2.0 release.

## [2.0.0-alpha.8] - 2025-07-29

### Changed

- Refactored ignored errors to avoid exceptions being thrown when accessed form stream/bus types.

## [2.0.0-alpha.7] - 2025-03-18

### Added

- Added parameters for `ignoredErrors` for all option types.
- `IAsyncOptionEnumerable` interface for being an async equivalent to the `IOptionEnumerable` interface.
- New `OptionStream` type added that is now more appropriate for its use case of single receiver, and is consumed via `Read` and `ReadToEnd` methods instead of events. Read-only equivalent is also added.

### Changed

- `End` and `None` option types on `IOption` interface are now functions instead of values to support passing new `ignoredErrors` prameter.
- `OptionStream` is renamed to `OptionBus` as that naming is more appropriate for its ability to have multiple senders/receivers. Read-only equivalent is also changed.

## [2.0.0-alpha.6] - 2024-11-18

### Changed

- Fix stack overflow error when accessing Exception value of error options of type `TError`.

## [2.0.0-alpha.1] - 2024-11-18

### Changed

- Updated usage pattern
  - Interfaces will be the primary usage.
  - Implementations are now internal.
  - `IOption` no longer has the generic overload of `TError`.
  - `IEnd`, `IError`, `INone` and `ISome` no longer inherit from `IOption`. Internal implementions of each of them do inherit from `IOption`, so all generated references of those interfaces will still be castable to `IOption`.

## [1.0.3] - 2024-11-15

### Changed

- Simplified types to remove `TError` generic definition for all except `Error<TValue, TError>`.

## [1.0.2] - 2024-11-12

### Changed

- Simplified types to remove `Value` property from `End`, `IOption` and `None`. Only `Error` and `Some` will have this property.

## [1.0.1] - 2024-08-28

### Added

- Added missed extension class for `IOptionEnumerable`.

## [1.0.0] - 2024-08-21

### Added

- Initial release.
