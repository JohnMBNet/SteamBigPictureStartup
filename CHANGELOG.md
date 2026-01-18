# Changelog

All notable changes to this project will be documented in this file.

## [1.1.0] - 2026-01-18

### Added
- Standalone installer executable (`SteamBigPictureSetup.exe`)
- Interactive console menu for install/uninstall
- Command-line flags for silent installation (`--install`, `--uninstall`)
- Self-contained executable with no runtime dependencies

### Changed
- Reorganized project structure
- Release notes moved to `releases/` folder

## [1.0.0] - 2026-01-18

### Added
- Initial release
- PowerShell startup script for Steam Big Picture mode
- PowerShell setup script (`setup.ps1`)
- Multi-path Steam detection
- Windows Registry fallback for Steam path
- Silent execution (no console window on startup)
