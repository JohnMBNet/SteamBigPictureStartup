# Steam Big Picture Mode Startup Script
# Place this script (or a shortcut to it) in your startup folder:
# %APPDATA%\Microsoft\Windows\Start Menu\Programs\Startup

$steamPath = "${env:ProgramFiles(x86)}\Steam\steam.exe"

# Check if Steam exists at the default location
if (Test-Path $steamPath) {
    Start-Process -FilePath $steamPath -ArgumentList "-bigpicture"
} else {
    # Try alternative common location
    $altPath = "$env:ProgramFiles\Steam\steam.exe"
    if (Test-Path $altPath) {
        Start-Process -FilePath $altPath -ArgumentList "-bigpicture"
    } else {
        Write-Error "Steam not found. Please update the script with your Steam installation path."
    }
}
