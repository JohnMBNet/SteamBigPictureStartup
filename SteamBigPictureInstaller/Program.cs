// Steam Big Picture Startup Installer
// Copyright (c) 2026 JohnMBNet
// Licensed under the MIT License

namespace SteamBigPictureInstaller;

class Program
{
    private const string AppName = "Steam Big Picture Startup";
    private const string AppVersion = "1.1.0";
    private const string ShortcutName = "Steam Big Picture.lnk";
    private const string ScriptFileName = "StartSteamBigPicture.ps1";

    private static readonly string StartupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
    private static readonly string InstallFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "SteamBigPictureStartup");

    static void Main(string[] args)
    {
        Console.Title = AppName + " Setup";

        // Handle command line arguments for silent install/uninstall
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

        // Interactive mode
        ShowMenu();
    }

    static void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            PrintHeader();

            bool isInstalled = IsInstalled();

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
  ║         Steam Big Picture Startup Setup               ║
  ║                     v{AppVersion}                            ║
  ╚═══════════════════════════════════════════════════════╝");
        Console.ResetColor();
    }

    static bool IsInstalled()
    {
        string shortcutPath = Path.Combine(StartupFolder, ShortcutName);
        return File.Exists(shortcutPath);
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

            // Write the PowerShell script
            string scriptPath = Path.Combine(InstallFolder, ScriptFileName);
            File.WriteAllText(scriptPath, GetStartupScript());

            // Create shortcut in startup folder
            string shortcutPath = Path.Combine(StartupFolder, ShortcutName);
            CreateShortcut(shortcutPath, scriptPath);

            if (!silent)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  ✓ Installation complete!");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine($"  Script installed to:");
                Console.WriteLine($"    {scriptPath}");
                Console.WriteLine();
                Console.WriteLine($"  Startup shortcut created at:");
                Console.WriteLine($"    {shortcutPath}");
                Console.WriteLine();
                Console.WriteLine("  Steam will now launch in Big Picture mode on startup.");
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

            // Remove shortcut from startup folder
            string shortcutPath = Path.Combine(StartupFolder, ShortcutName);
            if (File.Exists(shortcutPath))
            {
                File.Delete(shortcutPath);
            }

            // Remove install directory
            if (Directory.Exists(InstallFolder))
            {
                Directory.Delete(InstallFolder, recursive: true);
            }

            if (!silent)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  ✓ Uninstallation complete!");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("  Steam Big Picture will no longer start automatically.");
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
        // Use PowerShell to create the shortcut (avoids COM interop complexity)
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

# Steam not found in common locations
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
