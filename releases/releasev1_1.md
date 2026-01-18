# v1.1.0

**Released:** January 18, 2026

## What's New

- **Standalone Installer** — Pre-built `SteamBigPictureSetup.exe` included
- **No Dependencies** — Self-contained executable, no .NET runtime required
- **Interactive Menu** — GUI-style install/uninstall options
- **Silent Mode** — Command-line flags for scripted installations
- **Reorganized Project** — Release notes moved to `releases/` folder

## Installation

### GUI
```
install\SteamBigPictureSetup.exe
```

### Silent
```cmd
install\SteamBigPictureSetup.exe --install
install\SteamBigPictureSetup.exe --uninstall
```

## Installer Features

- Interactive console menu
- Displays current installation status
- Creates startup script in `%LocalAppData%\SteamBigPictureStartup`
- Adds shortcut to Windows Startup folder
- Smart Steam detection via multiple paths and Registry

## Requirements

- Windows 10/11
- Steam client

## Building from Source

Requires .NET 8 SDK:

```powershell
.\build.ps1
```
