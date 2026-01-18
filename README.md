# Steam Big Picture Startup

A simple PowerShell utility that automatically launches Steam in Big Picture mode when Windows starts.

## Features

- Automatically starts Steam in Big Picture mode on Windows startup
- Checks multiple common Steam installation paths
- Silent execution (no console window flash)
- Easy one-click setup

## Requirements

- Windows 10/11
- PowerShell 5.1 or later
- Steam installed in a standard location

## Installation

### Automatic Setup (Recommended)

1. Open PowerShell as Administrator
2. Navigate to the project directory:
   ```powershell
   cd "D:\Development\SteamBigPictureStartup"
   ```
3. Run the setup script:
   ```powershell
   .\setup.ps1
   ```

The setup script will create a shortcut in your Windows Startup folder that launches Steam Big Picture mode silently on login.

### Manual Setup

1. Press `Win + R` and type `shell:startup` to open the Startup folder
2. Create a new shortcut with the following target:
   ```
   powershell.exe -ExecutionPolicy Bypass -WindowStyle Hidden -File "D:\Development\SteamBigPictureStartup\StartSteamBigPicture.ps1"
   ```
3. Name the shortcut "Steam Big Picture"

## Uninstallation

### Automatic

Run the setup script with the `-Uninstall` flag:
```powershell
.\setup.ps1 -Uninstall
```

### Manual

1. Press `Win + R` and type `shell:startup`
2. Delete the "Steam Big Picture" shortcut

## Files

| File | Description |
|------|-------------|
| `StartSteamBigPicture.ps1` | Main script that launches Steam Big Picture |
| `setup.ps1` | Installation/uninstallation utility |
| `README.md` | This documentation file |
| `releasev1.md` | Release notes for version 1.0 |

## Troubleshooting

**Steam not launching:**
- Verify Steam is installed at `C:\Program Files (x86)\Steam\steam.exe`
- If Steam is installed elsewhere, edit `StartSteamBigPicture.ps1` and update the `$steamPath` variable

**PowerShell execution policy error:**
- The setup script uses `-ExecutionPolicy Bypass` to avoid this issue
- Alternatively, run `Set-ExecutionPolicy RemoteSigned -Scope CurrentUser` in an elevated PowerShell

## License

MIT License - Feel free to use and modify as needed.
