# Changelog

All notable changes to this project will be documented in this file.

## [Unreleased]

## [2.0.0-alpha.6] - 2024-11-18

### Changed

- Fix stack overflow error when accessing Exception value of error options of type TError.

## [2.0.0-alpha.1] - 2024-11-18

### Changed

- Updated usage pattern
  - Interfaces will be the primary usage.
  - Implementations are now internal.
  - IOption no longer has the generic overload of TError.
  - IEnd, IError, INone and ISome no longer inherit from IOption. Internal implementions of each of them do inherit from IOption, so all generated references of those interfaces will still be castable to IOption.

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
