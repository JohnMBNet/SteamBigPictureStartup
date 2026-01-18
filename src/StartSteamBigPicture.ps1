<#
.SYNOPSIS
    Launches Steam in Big Picture mode.

.DESCRIPTION
    This script finds the Steam installation and launches it in Big Picture mode.
    It checks multiple common installation paths and falls back to the Windows Registry.

.NOTES
    Author: JohnMBNet
    License: MIT
#>

$steamPaths = @(
    "${env:ProgramFiles(x86)}\Steam\steam.exe",
    "$env:ProgramFiles\Steam\steam.exe",
    "D:\Steam\steam.exe",
    "D:\Games\Steam\steam.exe"
)

# Check common installation paths
foreach ($path in $steamPaths) {
    if (Test-Path $path) {
        Start-Process -FilePath $path -ArgumentList "-bigpicture"
        exit 0
    }
}

# Fallback: Check Windows Registry
$regPath = "HKCU:\Software\Valve\Steam"
if (Test-Path $regPath) {
    $steamDir = (Get-ItemProperty -Path $regPath -Name SteamPath -ErrorAction SilentlyContinue).SteamPath
    if ($steamDir) {
        $steamExe = Join-Path $steamDir "steam.exe"
        if (Test-Path $steamExe) {
            Start-Process -FilePath $steamExe -ArgumentList "-bigpicture"
            exit 0
        }
    }
}

Write-Error "Steam not found. Please verify Steam is installed."
exit 1
