<#
.SYNOPSIS
    Installs or uninstalls Steam Big Picture Startup.

.DESCRIPTION
    Creates or removes a startup shortcut that launches Steam in Big Picture mode on login.

.PARAMETER Uninstall
    Remove the startup shortcut.

.EXAMPLE
    .\setup.ps1
    Installs the startup shortcut.

.EXAMPLE
    .\setup.ps1 -Uninstall
    Removes the startup shortcut.

.NOTES
    Author: JohnMBNet
    License: MIT
#>

param(
    [switch]$Uninstall
)

$shortcutName = "Steam Big Picture.lnk"
$startupFolder = [Environment]::GetFolderPath("Startup")
$shortcutPath = Join-Path $startupFolder $shortcutName
$scriptPath = Join-Path $PSScriptRoot "StartSteamBigPicture.ps1"

if ($Uninstall) {
    if (Test-Path $shortcutPath) {
        Remove-Item $shortcutPath -Force
        Write-Host "Uninstalled successfully." -ForegroundColor Green
        Write-Host "Steam Big Picture will no longer start automatically."
    } else {
        Write-Host "Not installed. Nothing to remove." -ForegroundColor Yellow
    }
} else {
    if (-not (Test-Path $scriptPath)) {
        Write-Error "StartSteamBigPicture.ps1 not found."
        exit 1
    }

    $WshShell = New-Object -ComObject WScript.Shell
    $shortcut = $WshShell.CreateShortcut($shortcutPath)
    $shortcut.TargetPath = "powershell.exe"
    $shortcut.Arguments = "-ExecutionPolicy Bypass -WindowStyle Hidden -File `"$scriptPath`""
    $shortcut.WorkingDirectory = $PSScriptRoot
    $shortcut.Description = "Launch Steam in Big Picture mode"
    $shortcut.Save()

    Write-Host "Installed successfully!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Shortcut created at:" -ForegroundColor Cyan
    Write-Host "  $shortcutPath"
    Write-Host ""
    Write-Host "Steam will launch in Big Picture mode on next login."
    Write-Host ""
    Write-Host "To uninstall: .\setup.ps1 -Uninstall" -ForegroundColor DarkGray
}
