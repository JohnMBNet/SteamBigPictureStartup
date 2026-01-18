# Changelog

All notable changes to this project will be documented in this file.

## [1.2.0] - 2026-01-18

### Added
- Automatic Steam startup conflict resolution
- Detects and disables Steam's default "Run at startup" setting
- Backs up Steam startup registry value before modification
- Restores original Steam startup settings on uninstall
- Status display shows if Steam startup is currently enabled
- Progress feedback during installation/uninstallation

### Fixed
- Issue where Steam would start in normal mode alongside Big Picture mode

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
