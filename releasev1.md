# Release v1.0.0

**Release Date:** January 18, 2026

## Overview

Initial release of Steam Big Picture Startup - a lightweight PowerShell utility that automatically launches Steam in Big Picture mode when Windows starts.

## Features

- **Automatic Steam Big Picture Launch** - Starts Steam directly into Big Picture mode on Windows login
- **Silent Execution** - Runs without displaying a console window
- **Multi-Path Support** - Checks both `Program Files (x86)` and `Program Files` for Steam installation
- **Easy Setup** - One-command installation via `setup.ps1`
- **Clean Uninstall** - Remove with `setup.ps1 -Uninstall`

## Files Included

- `StartSteamBigPicture.ps1` - Main startup script
- `setup.ps1` - Installation and uninstallation utility
- `README.md` - Documentation and usage instructions
- `releasev1.md` - This release notes file

## Installation

```powershell
.\setup.ps1
```

## System Requirements

- Windows 10 or Windows 11
- PowerShell 5.1 or later
- Steam client installed

## Known Limitations

- Steam must be installed in a standard location (`Program Files (x86)\Steam` or `Program Files\Steam`)
- Custom Steam installation paths require manual script modification

## Future Improvements

- Custom Steam path configuration
- Optional delay before launching Steam
- System tray notification on launch

---

*This is the first stable release. Please report any issues on the project repository.*
