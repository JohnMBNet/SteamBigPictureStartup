<#
.SYNOPSIS
    Builds the Steam Big Picture Startup installer executable.

.DESCRIPTION
    Compiles the C# installer project into a self-contained single-file executable.
    The output is copied to the project root for easy access.

.NOTES
    Requires .NET 8 SDK: https://dotnet.microsoft.com/download/dotnet/8.0
    Output: ..\SteamBigPictureSetup.exe
#>

$projectPath = Join-Path $PSScriptRoot "SteamBigPictureInstaller"
$publishPath = Join-Path $PSScriptRoot "publish"
$rootPath = Split-Path $PSScriptRoot -Parent
$finalExePath = Join-Path $rootPath "SteamBigPictureSetup.exe"

Write-Host ""
Write-Host "  Building Steam Big Picture Startup Installer..." -ForegroundColor Cyan
Write-Host ""

# Clean previous build
if (Test-Path $publishPath) {
    Remove-Item $publishPath -Recurse -Force
}

# Build and publish
dotnet publish $projectPath `
    -c Release `
    -r win-x64 `
    -o $publishPath `
    --self-contained true `
    -p:PublishSingleFile=true `
    -p:IncludeNativeLibrariesForSelfExtract=true

if ($LASTEXITCODE -eq 0) {
    $builtExePath = Join-Path $publishPath "SteamBigPictureSetup.exe"

    # Copy to project root
    Copy-Item $builtExePath $finalExePath -Force

    # Clean up publish folder
    Remove-Item $publishPath -Recurse -Force

    $size = (Get-Item $finalExePath).Length / 1MB

    Write-Host ""
    Write-Host "  Build successful!" -ForegroundColor Green
    Write-Host ""
    Write-Host "  Output: $finalExePath"
    Write-Host ("  Size:   {0:N2} MB" -f $size) -ForegroundColor DarkGray
    Write-Host ""
} else {
    Write-Host ""
    Write-Host "  Build failed. Ensure .NET 8 SDK is installed." -ForegroundColor Red
    Write-Host ""
    exit 1
}
