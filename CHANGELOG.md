# Changelog

All notable changes to this project will be documented in this file.

## [Unreleased]

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
