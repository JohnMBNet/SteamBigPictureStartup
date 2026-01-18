<#
.SYNOPSIS
    Builds the Steam Big Picture Startup installer executable.

.DESCRIPTION
    Compiles the C# installer project into a self-contained single-file executable.

.NOTES
    Requires .NET 8 SDK: https://dotnet.microsoft.com/download/dotnet/8.0
    Output: publish\SteamBigPictureSetup.exe
#>

$projectPath = Join-Path $PSScriptRoot "SteamBigPictureInstaller"
$outputPath = Join-Path $PSScriptRoot "publish"

Write-Host "Building Steam Big Picture Startup Installer..." -ForegroundColor Cyan
Write-Host ""

# Clean previous build
if (Test-Path $outputPath) {
    Remove-Item $outputPath -Recurse -Force
}

# Build and publish
dotnet publish $projectPath `
    -c Release `
    -r win-x64 `
    -o $outputPath `
    --self-contained true `
    -p:PublishSingleFile=true `
    -p:IncludeNativeLibrariesForSelfExtract=true

if ($LASTEXITCODE -eq 0) {
    $exePath = Join-Path $outputPath "SteamBigPictureSetup.exe"
    $size = (Get-Item $exePath).Length / 1MB

    Write-Host ""
    Write-Host "Build successful!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Output: $exePath"
    Write-Host ("Size:   {0:N2} MB" -f $size) -ForegroundColor DarkGray
} else {
    Write-Host ""
    Write-Host "Build failed. Ensure .NET 8 SDK is installed." -ForegroundColor Red
    exit 1
}
