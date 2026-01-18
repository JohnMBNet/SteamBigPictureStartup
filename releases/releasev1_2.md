# v1.2.0

**Released:** January 18, 2026

## What's New

### 🛡️ Steam Startup Conflict Resolution

The installer now automatically handles conflicts with Steam's built-in startup:

- **Auto-detects** if Steam is set to start automatically
- **Disables** Steam's default startup during installation
- **Backs up** original settings for safe restoration
- **Restores** Steam startup settings on uninstall

This prevents the issue where both Steam (normal mode) and this utility would try to start simultaneously.

### 🎨 Enhanced UI

- Status screen now shows if Steam's default startup is enabled
- Installation progress shows each step being performed
- Clearer messaging throughout the process

## Installation

```
install\SteamBigPictureSetup.exe
```

Or silent mode:

```cmd
install\SteamBigPictureSetup.exe --install
```

## Technical Details

The installer modifies the Windows Registry key:
```
HKCU\Software\Microsoft\Windows\CurrentVersion\Run\Steam
```

The original value is backed up to:
```
%LocalAppData%\SteamBigPictureStartup\steam_startup_backup.txt
```

## Changelog

### v1.2.0
- Added automatic Steam startup conflict resolution
- Backup and restore of Steam's original startup settings
- Enhanced installer UI with progress feedback
- Updated documentation with FAQ section
