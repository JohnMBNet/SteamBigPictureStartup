# v1.3.0

**Released:** January 18, 2026

## What's New

### 📁 Professional Project Structure

The project has been completely reorganized for a cleaner, more professional layout:

```
SteamBigPictureStartup/
│
├── 🎮 SteamBigPictureSetup.exe     ← Run this to install!
│
├── 📂 src/                          ← Source code
│   ├── SteamBigPictureInstaller/
│   ├── StartSteamBigPicture.ps1
│   ├── setup.ps1
│   └── build.ps1
│
├── 📂 docs/                         ← Documentation
│   └── Release notes
│
├── README.md
├── CHANGELOG.md
└── LICENSE
```

**Key Changes:**
- **Installer at root** — No more navigating to subfolders, just run the exe
- **Source code in `src/`** — All development files organized in one place
- **Documentation in `docs/`** — Release notes and future documentation
- **Clean root directory** — Only essential files visible

### 📄 Stunning New README

The README has been completely redesigned with:

- **Badges** — Platform, license, and version at a glance
- **Feature tables** — Side-by-side comparison of capabilities
- **Visual installer preview** — ASCII mockup of the installer UI
- **Flow diagram** — Shows how the startup process works
- **Collapsible FAQ** — Common questions answered without cluttering the page
- **Professional footer** — Clean, centered design

### 🔧 Improved Build Process

- Build script now outputs directly to project root
- Automatic cleanup of temporary build files
- Clearer build progress messages

## Installation

Just run the exe at the root:

```
SteamBigPictureSetup.exe
```

Or silent mode:

```cmd
SteamBigPictureSetup.exe --install
SteamBigPictureSetup.exe --uninstall
```

## Building from Source

```powershell
cd src
.\build.ps1
```

The exe is automatically copied to the project root.

## Full Changelog

### v1.3.0
- Reorganized project with professional folder structure
- Moved installer exe to project root for easy access
- Source files consolidated in `src/` directory
- Release notes moved to `docs/` directory
- Redesigned README with badges, tables, and visual elements
- Added collapsible FAQ section
- Updated build script to output to root
- Improved documentation throughout

### v1.2.0
- Added automatic Steam startup conflict resolution
- Backup and restore of Steam's original startup settings
- Enhanced installer UI with progress feedback

### v1.1.0
- Added standalone installer executable
- Interactive console menu
- Command-line silent install support

### v1.0.0
- Initial release
- PowerShell-based Steam Big Picture startup
