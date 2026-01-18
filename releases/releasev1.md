# v1.0.0

**Released:** January 18, 2026

## Overview

Initial release of Steam Big Picture Startup — a lightweight utility that automatically launches Steam in Big Picture mode when Windows starts.

## Features

- Automatic Steam Big Picture launch on Windows login
- Silent execution with no visible console window
- Multi-path Steam detection with Registry fallback
- PowerShell-based setup script

## Installation

```powershell
.\setup.ps1
```

## Uninstallation

```powershell
.\setup.ps1 -Uninstall
```

## Requirements

- Windows 10/11
- PowerShell 5.1+
- Steam client
