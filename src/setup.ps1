<#
.SYNOPSIS
    Installs or uninstalls Steam Big Picture Startup.

.DESCRIPTION
    Creates or removes a startup shortcut that launches Steam in Big Picture mode on login.
    Automatically handles conflicts with Steam's default startup setting.

.PARAMETER Uninstall
    Remove the startup shortcut and restore Steam's original startup setting.

.EXAMPLE
    .\setup.ps1
    Installs the startup shortcut.

.EXAMPLE
    .\setup.ps1 -Uninstall
    Removes the startup shortcut.

.NOTES
    Author: JohnMBNet
    License: MIT
    Version: 1.3.0
#>

param(
    [switch]$Uninstall
)

$shortcutName = "Steam Big Picture.lnk"
$startupFolder = [Environment]::GetFolderPath("Startup")
$shortcutPath = Join-Path $startupFolder $shortcutName
$scriptPath = Join-Path $PSScriptRoot "StartSteamBigPicture.ps1"
$backupPath = Join-Path $PSScriptRoot ".steam_startup_backup"
$steamRunKey = "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run"

function Disable-SteamStartup {
    try {
        $steamValue = Get-ItemProperty -Path $steamRunKey -Name "Steam" -ErrorAction SilentlyContinue
        if ($steamValue) {
            # Backup the Steam startup value
            $steamValue.Steam | Out-File -FilePath $backupPath -Force

            # Remove Steam from startup
            Remove-ItemProperty -Path $steamRunKey -Name "Steam" -ErrorAction SilentlyContinue

            Write-Host "  • Disabled Steam's default startup" -ForegroundColor DarkGray
        }
    } catch {
        Write-Host "  • Warning: Could not disable Steam startup: $_" -ForegroundColor Yellow
    }
}

function Restore-SteamStartup {
    try {
        if (Test-Path $backupPath) {
            $steamValue = Get-Content $backupPath -Raw
            if ($steamValue) {
                Set-ItemProperty -Path $steamRunKey -Name "Steam" -Value $steamValue.Trim()
                Remove-Item $backupPath -Force
                Write-Host "  • Restored Steam's default startup" -ForegroundColor DarkGray
            }
        }
    } catch {
        Write-Host "  • Warning: Could not restore Steam startup: $_" -ForegroundColor Yellow
    }
}

if ($Uninstall) {
    Write-Host ""
    Write-Host "  Uninstalling..." -ForegroundColor Cyan
    Write-Host ""

    # Restore Steam's default startup
    Restore-SteamStartup

    # Remove shortcut
    if (Test-Path $shortcutPath) {
        Remove-Item $shortcutPath -Force
        Write-Host "  • Removed startup shortcut" -ForegroundColor DarkGray
    }

    Write-Host ""
    Write-Host "  Uninstalled successfully." -ForegroundColor Green
    Write-Host "  Steam's original startup settings have been restored."
    Write-Host ""
} else {
    if (-not (Test-Path $scriptPath)) {
        Write-Error "StartSteamBigPicture.ps1 not found."
        exit 1
    }

    Write-Host ""
    Write-Host "  Installing..." -ForegroundColor Cyan
    Write-Host ""

    # Disable Steam's default startup to prevent conflict
    Disable-SteamStartup

    # Create shortcut
    $WshShell = New-Object -ComObject WScript.Shell
    $shortcut = $WshShell.CreateShortcut($shortcutPath)
    $shortcut.TargetPath = "powershell.exe"
    $shortcut.Arguments = "-ExecutionPolicy Bypass -WindowStyle Hidden -File `"$scriptPath`""
    $shortcut.WorkingDirectory = $PSScriptRoot
    $shortcut.Description = "Launch Steam in Big Picture mode"
    $shortcut.Save()

    Write-Host "  • Created startup shortcut" -ForegroundColor DarkGray

    Write-Host ""
    Write-Host "  Installed successfully!" -ForegroundColor Green
    Write-Host ""
    Write-Host "  Shortcut created at:" -ForegroundColor Cyan
    Write-Host "  $shortcutPath"
    Write-Host ""
    Write-Host "  Steam will launch in Big Picture mode on next login."
    Write-Host ""
    Write-Host "  To uninstall: .\setup.ps1 -Uninstall" -ForegroundColor DarkGray
    Write-Host ""
}
