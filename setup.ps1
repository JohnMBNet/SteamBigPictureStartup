# Steam Big Picture Startup - Setup Script
# Creates or removes a startup shortcut for Steam Big Picture mode

param(
    [switch]$Uninstall
)

$shortcutName = "Steam Big Picture.lnk"
$startupFolder = [Environment]::GetFolderPath("Startup")
$shortcutPath = Join-Path $startupFolder $shortcutName
$scriptPath = Join-Path $PSScriptRoot "StartSteamBigPicture.ps1"

if ($Uninstall) {
    # Remove the shortcut
    if (Test-Path $shortcutPath) {
        Remove-Item $shortcutPath -Force
        Write-Host "Uninstalled successfully. Steam Big Picture will no longer start automatically." -ForegroundColor Green
    } else {
        Write-Host "Shortcut not found. Nothing to uninstall." -ForegroundColor Yellow
    }
} else {
    # Verify the main script exists
    if (-not (Test-Path $scriptPath)) {
        Write-Error "StartSteamBigPicture.ps1 not found in the current directory."
        exit 1
    }

    # Create the shortcut
    $WshShell = New-Object -ComObject WScript.Shell
    $shortcut = $WshShell.CreateShortcut($shortcutPath)
    $shortcut.TargetPath = "powershell.exe"
    $shortcut.Arguments = "-ExecutionPolicy Bypass -WindowStyle Hidden -File `"$scriptPath`""
    $shortcut.WorkingDirectory = $PSScriptRoot
    $shortcut.Description = "Launch Steam in Big Picture mode"
    $shortcut.Save()

    Write-Host "Setup complete!" -ForegroundColor Green
    Write-Host ""
    Write-Host "A shortcut has been created at:" -ForegroundColor Cyan
    Write-Host "  $shortcutPath" -ForegroundColor White
    Write-Host ""
    Write-Host "Steam will now launch in Big Picture mode when you log in." -ForegroundColor Cyan
    Write-Host ""
    Write-Host "To uninstall, run: .\setup.ps1 -Uninstall" -ForegroundColor Gray
}
