# Steam Big Picture Startup

<p align="center">
  <strong>Automatically launch Steam in Big Picture mode when Windows starts</strong>
</p>

<p align="center">
  <a href="#installation">Installation</a> •
  <a href="#features">Features</a> •
  <a href="#uninstallation">Uninstallation</a> •
  <a href="#building-from-source">Build</a> •
  <a href="#license">License</a>
</p>

---

## Features

- **Automatic Launch** — Steam Big Picture mode starts when you log into Windows
- **Smart Detection** — Finds Steam in common install locations and Windows Registry
- **Silent Operation** — No console windows or pop-ups during startup
- **Easy Setup** — One-click installer with GUI menu
- **Portable** — Self-contained executable with no dependencies

## Requirements

- Windows 10 or Windows 11
- Steam client installed

## Installation

### Quick Start

1. Navigate to the `install` folder
2. Run **`SteamBigPictureSetup.exe`**
3. Select **Install** from the menu

That's it! Steam will launch in Big Picture mode on your next login.

### Command Line

```cmd
install\SteamBigPictureSetup.exe --install
```

### Alternative: PowerShell Script

If you prefer not to use the executable:

```powershell
.\setup.ps1
```

## Uninstallation

### Using the Installer

1. Run **`install\SteamBigPictureSetup.exe`**
2. Select **Uninstall**

### Command Line

```cmd
install\SteamBigPictureSetup.exe --uninstall
```

### PowerShell

```powershell
.\setup.ps1 -Uninstall
```

## How It Works

The installer creates a small PowerShell script in your local app data folder and adds a shortcut to the Windows Startup folder. On login, the script silently launches Steam with the `-bigpicture` flag.

**Install location:** `%LocalAppData%\SteamBigPictureStartup`

**Steam detection paths:**
- `C:\Program Files (x86)\Steam`
- `C:\Program Files\Steam`
- `D:\Steam`
- `D:\Games\Steam`
- Windows Registry (`HKCU:\Software\Valve\Steam`)

## Building from Source

Requires [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

```powershell
.\build.ps1
```

Output: `publish\SteamBigPictureSetup.exe`

## Project Structure

```
SteamBigPictureStartup/
├── install/
│   └── SteamBigPictureSetup.exe    # Pre-built installer
├── releases/
│   └── *.md                        # Release notes
├── SteamBigPictureInstaller/       # C# source code
├── StartSteamBigPicture.ps1        # Standalone PowerShell script
├── setup.ps1                       # PowerShell installer
└── build.ps1                       # Build script
```

## Troubleshooting

**Steam doesn't launch?**
- Ensure Steam is installed in a standard location
- Check if Steam is already running

**Execution policy error?**
- The installer uses `-ExecutionPolicy Bypass` automatically
- Or run: `Set-ExecutionPolicy RemoteSigned -Scope CurrentUser`

## License

[MIT License](LICENSE) — feel free to use, modify, and distribute.

---

<p align="center">
  Made for couch gaming enthusiasts
</p>
