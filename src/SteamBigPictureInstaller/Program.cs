// Steam Big Picture Startup Installer
// Copyright (c) 2026 JohnMBNet
// Licensed under the MIT License

using Microsoft.Win32;

namespace SteamBigPictureInstaller;

class Program
{
    private const string AppName = "Steam Big Picture Startup";
    private const string AppVersion = "1.3.0";
    private const string ShortcutName = "Steam Big Picture.lnk";
    private const string ScriptFileName = "StartSteamBigPicture.ps1";
    private const string SteamRunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
    private const string SteamRunValueName = "Steam";
    private const string BackupFileName = "steam_startup_backup.txt";

    private static readonly string StartupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
    private static readonly string InstallFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "SteamBigPictureStartup");

    static void Main(string[] args)
    {
        Console.Title = AppName + " Setup";

        if (args.Length > 0)
        {
            switch (args[0].ToLower())
            {
                case "/install":
                case "-install":
                case "--install":
                    Install(silent: true);
                    return;
                case "/uninstall":
                case "-uninstall":
                case "--uninstall":
                    Uninstall(silent: true);
                    return;
            }
        }

        ShowMenu();
    }

    static void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            PrintHeader();

            bool isInstalled = IsInstalled();
            bool steamStartupEnabled = IsSteamStartupEnabled();

            Console.WriteLine();
            if (isInstalled)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  Status: INSTALLED");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  Status: NOT INSTALLED");
                Console.ResetColor();
            }

            if (steamStartupEnabled && !isInstalled)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("  Note:   Steam's default startup is enabled (will be disabled on install)");
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine("  Select an option:");
            Console.WriteLine();
            Console.WriteLine("    [1] Install");
            Console.WriteLine("    [2] Uninstall");
            Console.WriteLine("    [3] Exit");
            Console.WriteLine();
            Console.Write("  Enter choice (1-3): ");

            var key = Console.ReadKey();
            Console.WriteLine();

            switch (key.KeyChar)
            {
                case '1':
                    Install(silent: false);
                    break;
                case '2':
                    Uninstall(silent: false);
                    break;
                case '3':
                    return;
            }
        }
    }

    static void PrintHeader()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($@"
  ╔═══════════════════════════════════════════════════════╗
  ║       Steam Big Picture Startup Setup                 ║
  ║                    v{AppVersion}                            ║
  ╚═══════════════════════════════════════════════════════╝");
        Console.ResetColor();
    }

    static bool IsInstalled()
    {
        string shortcutPath = Path.Combine(StartupFolder, ShortcutName);
        return File.Exists(shortcutPath);
    }

    static bool IsSteamStartupEnabled()
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(SteamRunKey, false);
            return key?.GetValue(SteamRunValueName) != null;
        }
        catch
        {
            return false;
        }
    }

    static void DisableSteamStartup(bool silent)
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(SteamRunKey, true);
            if (key != null)
            {
                var steamValue = key.GetValue(SteamRunValueName);
                if (steamValue != null)
                {
                    // Backup the Steam startup value
                    string backupPath = Path.Combine(InstallFolder, BackupFileName);
                    File.WriteAllText(backupPath, steamValue.ToString() ?? "");

                    // Remove Steam from startup
                    key.DeleteValue(SteamRunValueName, false);

                    if (!silent)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("  • Disabled Steam's default startup");
                        Console.ResetColor();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (!silent)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  • Warning: Could not disable Steam startup: {ex.Message}");
                Console.ResetColor();
            }
        }
    }

    static void RestoreSteamStartup(bool silent)
    {
        try
        {
            string backupPath = Path.Combine(InstallFolder, BackupFileName);
            if (File.Exists(backupPath))
            {
                string steamStartupValue = File.ReadAllText(backupPath);
                if (!string.IsNullOrEmpty(steamStartupValue))
                {
                    using var key = Registry.CurrentUser.OpenSubKey(SteamRunKey, true);
                    key?.SetValue(SteamRunValueName, steamStartupValue);

                    if (!silent)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("  • Restored Steam's default startup");
                        Console.ResetColor();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (!silent)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  • Warning: Could not restore Steam startup: {ex.Message}");
                Console.ResetColor();
            }
        }
    }

    static void Install(bool silent)
    {
        try
        {
            if (!silent)
            {
                Console.Clear();
                PrintHeader();
                Console.WriteLine();
                Console.WriteLine("  Installing...");
                Console.WriteLine();
            }

            // Create install directory
            if (!Directory.Exists(InstallFolder))
            {
                Directory.CreateDirectory(InstallFolder);
            }

            // Disable Steam's default startup to prevent conflict
            DisableSteamStartup(silent);

            // Write the PowerShell script
            string scriptPath = Path.Combine(InstallFolder, ScriptFileName);
            File.WriteAllText(scriptPath, GetStartupScript());

            if (!silent)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  • Created startup script");
                Console.ResetColor();
            }

            // Create shortcut in startup folder
            string shortcutPath = Path.Combine(StartupFolder, ShortcutName);
            CreateShortcut(shortcutPath, scriptPath);

            if (!silent)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  • Created startup shortcut");
                Console.ResetColor();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  ✓ Installation complete!");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("  Steam will launch in Big Picture mode on next login.");
                Console.WriteLine();
                Console.WriteLine("  Press any key to continue...");
                Console.ReadKey();
            }
        }
        catch (Exception ex)
        {
            if (!silent)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗ Installation failed: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("  Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Environment.Exit(1);
            }
        }
    }

    static void Uninstall(bool silent)
    {
        try
        {
            if (!silent)
            {
                Console.Clear();
                PrintHeader();
                Console.WriteLine();
                Console.WriteLine("  Uninstalling...");
                Console.WriteLine();
            }

            // Restore Steam's default startup before removing files
            RestoreSteamStartup(silent);

            // Remove shortcut from startup folder
            string shortcutPath = Path.Combine(StartupFolder, ShortcutName);
            if (File.Exists(shortcutPath))
            {
                File.Delete(shortcutPath);
                if (!silent)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("  • Removed startup shortcut");
                    Console.ResetColor();
                }
            }

            // Remove install directory
            if (Directory.Exists(InstallFolder))
            {
                Directory.Delete(InstallFolder, recursive: true);
                if (!silent)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("  • Removed installed files");
                    Console.ResetColor();
                }
            }

            if (!silent)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  ✓ Uninstallation complete!");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("  Steam's original startup settings have been restored.");
                Console.WriteLine();
                Console.WriteLine("  Press any key to continue...");
                Console.ReadKey();
            }
        }
        catch (Exception ex)
        {
            if (!silent)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  ✗ Uninstallation failed: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("  Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Environment.Exit(1);
            }
        }
    }

    static void CreateShortcut(string shortcutPath, string scriptPath)
    {
        string psCommand = $@"
$WshShell = New-Object -ComObject WScript.Shell
$Shortcut = $WshShell.CreateShortcut('{shortcutPath}')
$Shortcut.TargetPath = 'powershell.exe'
$Shortcut.Arguments = '-ExecutionPolicy Bypass -WindowStyle Hidden -File ""{scriptPath}""'
$Shortcut.WorkingDirectory = '{InstallFolder}'
$Shortcut.Description = 'Launch Steam in Big Picture mode'
$Shortcut.Save()
";

        var startInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{psCommand.Replace("\"", "\\\"")}\"",
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        using var process = System.Diagnostics.Process.Start(startInfo);
        process?.WaitForExit();

        if (process?.ExitCode != 0)
        {
            throw new Exception("Failed to create startup shortcut");
        }
    }

    static string GetStartupScript()
    {
        return @"# Steam Big Picture Mode Startup Script
# Auto-generated by Steam Big Picture Startup Installer

$steamPaths = @(
    ""${env:ProgramFiles(x86)}\Steam\steam.exe"",
    ""$env:ProgramFiles\Steam\steam.exe"",
    ""D:\Steam\steam.exe"",
    ""D:\Games\Steam\steam.exe""
)

foreach ($path in $steamPaths) {
    if (Test-Path $path) {
        Start-Process -FilePath $path -ArgumentList ""-bigpicture""
        exit
    }
}

$regPath = ""HKCU:\Software\Valve\Steam""
if (Test-Path $regPath) {
    $steamDir = (Get-ItemProperty -Path $regPath -Name SteamPath -ErrorAction SilentlyContinue).SteamPath
    if ($steamDir) {
        $steamExe = Join-Path $steamDir ""steam.exe""
        if (Test-Path $steamExe) {
            Start-Process -FilePath $steamExe -ArgumentList ""-bigpicture""
            exit
        }
    }
}
";
    }
}
