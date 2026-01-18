<div align="center">

# 🎮 Steam Big Picture Startup

### Launch Steam in Big Picture mode automatically when Windows starts

[![Windows](https://img.shields.io/badge/Platform-Windows%2010%2F11-0078D6?style=for-the-badge&logo=windows)](https://www.microsoft.com/windows)
[![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)](LICENSE)
[![Version](https://img.shields.io/badge/Version-1.3.0-blue?style=for-the-badge)]()

<br>

<img src="https://store.steampowered.com/public/shared/images/header/logo_steam.svg" width="200" alt="Steam Logo">

<br>

**Perfect for couch gaming setups, HTPCs, and Steam Machines**

[Installation](#-installation) • [Features](#-features) • [How It Works](#-how-it-works) • [FAQ](#-faq) • [Building](#%EF%B8%8F-building-from-source)

---

</div>

<br>

## ✨ Features

<table>
<tr>
<td width="50%">

### 🚀 Seamless Experience
- **One-click installer** — Simple GUI setup
- **Silent mode** — Scriptable installation
- **Auto-detects Steam** — Works with any install location
- **Clean uninstall** — Removes all traces

</td>
<td width="50%">

### 🛡️ Smart & Safe
- **Handles conflicts** — Disables Steam's default startup
- **Backs up settings** — Restores on uninstall
- **No admin required** — User-level installation
- **Open source** — Fully transparent

</td>
</tr>
</table>

<br>

## 📦 Installation

### Quick Start

<table>
<tr>
<td>

**1.** Download or clone this repository

**2.** Run **`SteamBigPictureSetup.exe`**

**3.** Select **Install**

**4.** Done! ✓

</td>
<td>

```
┌───────────────────────────────────────────────┐
│     Steam Big Picture Startup Setup           │
│                  v1.3.0                       │
├───────────────────────────────────────────────┤
│                                               │
│  Status: NOT INSTALLED                        │
│                                               │
│  Select an option:                            │
│                                               │
│    [1] Install                                │
│    [2] Uninstall                              │
│    [3] Exit                                   │
│                                               │
│  Enter choice (1-3): _                        │
└───────────────────────────────────────────────┘
```

</td>
</tr>
</table>

### Command Line

```powershell
# Silent install
.\SteamBigPictureSetup.exe --install

# Silent uninstall
.\SteamBigPictureSetup.exe --uninstall
```

### Alternative: PowerShell Script

```powershell
# Install
.\src\setup.ps1

# Uninstall
.\src\setup.ps1 -Uninstall
```

<br>

## 🔧 How It Works

```
┌─────────────────┐     ┌──────────────────┐     ┌─────────────────┐
│   Windows       │────▶│   PowerShell     │────▶│     Steam       │
│   Startup       │     │   Script         │     │   Big Picture   │
└─────────────────┘     └──────────────────┘     └─────────────────┘
```

1. **On Install:**
   - Disables Steam's default startup (backed up for later)
   - Creates a lightweight PowerShell script
   - Adds a shortcut to Windows Startup folder

2. **On Login:**
   - Windows runs the startup shortcut
   - Script launches Steam with `-bigpicture` flag
   - Steam opens directly to Big Picture mode

3. **On Uninstall:**
   - Removes the startup shortcut and script
   - Restores Steam's original startup setting

<br>

## 📍 Steam Detection

The installer automatically finds Steam in these locations:

| Priority | Location |
|:--------:|----------|
| 1 | `C:\Program Files (x86)\Steam` |
| 2 | `C:\Program Files\Steam` |
| 3 | `D:\Steam` |
| 4 | `D:\Games\Steam` |
| 5 | Windows Registry *(fallback)* |

<br>

## ❓ FAQ

<details>
<summary><b>What if Steam is already set to start automatically?</b></summary>
<br>
The installer automatically detects and disables Steam's default startup to prevent conflicts. Your original settings are backed up and restored when you uninstall.
</details>

<details>
<summary><b>Does this require administrator privileges?</b></summary>
<br>
No! Everything is installed at the user level — no admin rights needed.
</details>

<details>
<summary><b>Where are files installed?</b></summary>
<br>

| Component | Location |
|-----------|----------|
| Startup script | `%LocalAppData%\SteamBigPictureStartup` |
| Startup shortcut | `%AppData%\Microsoft\Windows\Start Menu\Programs\Startup` |

</details>

<details>
<summary><b>Steam isn't launching — what do I do?</b></summary>
<br>

1. Make sure Steam is installed in a standard location
2. Check that Steam isn't already running
3. Verify the shortcut exists in your Startup folder (`Win + R` → `shell:startup`)

</details>

<details>
<summary><b>Can I use this on multiple user accounts?</b></summary>
<br>
Yes! Run the installer for each Windows user account that needs Big Picture mode on startup.
</details>

<br>

## 🏗️ Building from Source

**Requirements:** [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

```powershell
cd src
.\build.ps1
```

Output: `SteamBigPictureSetup.exe` (copied to root)

<br>

## 📁 Project Structure

```
SteamBigPictureStartup/
│
├── 🎮 SteamBigPictureSetup.exe     ← Run this to install
│
├── 📂 src/
│   ├── 📂 SteamBigPictureInstaller/
│   │   ├── Program.cs              ← Installer source
│   │   └── *.csproj                ← Project file
│   ├── StartSteamBigPicture.ps1    ← Standalone script
│   ├── setup.ps1                   ← PowerShell installer
│   └── build.ps1                   ← Build script
│
├── 📂 docs/
│   └── *.md                        ← Release notes
│
├── 📄 README.md                    ← You are here
├── 📄 CHANGELOG.md                 ← Version history
└── 📄 LICENSE                      ← MIT License
```

<br>

## 📜 License

<div align="center">

**MIT License** — Free to use, modify, and distribute.

See [LICENSE](LICENSE) for details.

<br>

---

<br>

Made with ☕ for couch gamers everywhere

<br>

**[⬆ Back to top](#-steam-big-picture-startup)**

</div>
