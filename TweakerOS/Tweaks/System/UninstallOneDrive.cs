using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.System;

public class UninstallOneDrive : ITweak
{
    public string Name => "Удалить OneDrive с устройства";
    public string Description => "Твик удаляет OneDrive с вашего компьютера";

    public bool GetIsChanged()
    {
        throw new NotImplementedException();
    }

    public bool ExplorerRebootRequires { get; }

    public void Enable()
    {
        try
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\OneDrive",
                "DisableFileSyncNGSC", "0", RegistryValueKind.DWord);

            using (RegistryKey localMachine =
                   RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey key = localMachine.CreateSubKey(@"SOFTWARE\Microsoft\OneDrive"))
                {
                    key.DeleteValue("PreventNetworkTrafficPreUserSignIn", false);
                }
            }

            string oneDriveInstaller;
            if (Environment.Is64BitOperatingSystem)
            {
                oneDriveInstaller = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory),
                    "Windows\\SysWOW64\\OneDriveSetup.exe");
            }
            else
            {
                oneDriveInstaller = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory),
                    "Windows\\System32\\OneDriveSetup.exe");
            }

            Process.Start(oneDriveInstaller);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\OneDrive",
                "DisableFileSyncNGSC", "0", RegistryValueKind.DWord);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex.Message.ToString()}", "SystemTweakcs.IninstallOneDrive", MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
    }

    public void Disable()
    {
        DeleteCMDHelper.RunCmdCommand("taskkill /f /im OneDrive.exe");
        Thread.Sleep(5000);

        string x86 = Environment.ExpandEnvironmentVariables("%SYSTEMROOT%\\System32\\OneDriveSetup.exe");
        string x64 = Environment.ExpandEnvironmentVariables("%SYSTEMROOT%\\SysWOW64\\OneDriveSetup.exe");

        string uninstallCommand = x64 + " /uninstall";
        if (!File.Exists(x64))
        {
            uninstallCommand = x86 + " /uninstall";
        }

        DeleteCMDHelper.RunCmdCommand(uninstallCommand);
        Thread.Sleep(5000);

        DeleteCMDHelper.RunCmdCommand("rd /S /Q \"%USERPROFILE%\\OneDrive\"");
        DeleteCMDHelper.RunCmdCommand("rd /S /Q \"C:\\OneDriveTemp\"");
        DeleteCMDHelper.RunCmdCommand("rd /S /Q \"%LOCALAPPDATA%\\Microsoft\\OneDrive\"");
        DeleteCMDHelper.RunCmdCommand("rd /S /Q \"%LOCALAPPDATA%\\OneDrive\"");
        DeleteCMDHelper.RunCmdCommand("rd /S /Q \"%PROGRAMDATA%\\Microsoft OneDrive\"");

        DeleteCMDHelper.RunCmdCommand(
            "REG DELETE \"HKEY_CLASSES_ROOT\\CLSID\\{018D5C66-4533-4307-9B53-224DE2ED1FE6}\" /f");
        DeleteCMDHelper.RunCmdCommand(
            "REG DELETE \"HKEY_CLASSES_ROOT\\Wow6432Node\\CLSID\\{018D5C66-4533-4307-9B53-224DE2ED1FE6}\" /f");


        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\OneDrive", "DisableFileSyncNGSC",
            "1", RegistryValueKind.DWord);

        using (RegistryKey localMachine =
               RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
        {
            using (RegistryKey key = localMachine.CreateSubKey(@"SOFTWARE\Microsoft\OneDrive"))
            {
                key.SetValue("PreventNetworkTrafficPreUserSignIn", 1);
            }
        }

        // удалить папки OneDrive
        string[] oneDriveFolders =
        {
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\OneDrive",
            Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)) + "OneDriveTemp",
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\OneDrive",
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Microsoft OneDrive"
        };

        foreach (string x in oneDriveFolders)
        {
            if (Directory.Exists(x))
            {
                try
                {
                    Directory.Delete(x, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message.ToString()}", "SystemTweakcs.UninstallOneDrive",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        // удалить запланированные задачи
        Utilities.RunCommand(@"SCHTASKS /Delete /TN ""OneDrive Standalone Update Task"" /F");
        Utilities.RunCommand(@"SCHTASKS /Delete /TN ""OneDrive Standalone Update Task v2"" /F");

        // удалить OneDrive из проводника Windows
        string rootKey = @"CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}";
        Registry.ClassesRoot.CreateSubKey(rootKey);
        int byteArray = BitConverter.ToInt32(BitConverter.GetBytes(0xb090010d), 0);
        var reg = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64);

        try
        {
            using (var key = Registry.ClassesRoot.OpenSubKey(rootKey, true))
            {
                key.SetValue("System.IsPinnedToNameSpaceTree", 0, RegistryValueKind.DWord);
            }

            using (var key = Registry.ClassesRoot.OpenSubKey(rootKey + "\\ShellFolder", true))
            {
                if (key != null)
                {
                    key.SetValue("Attributes", byteArray, RegistryValueKind.DWord);
                }
            }

            var reg2 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            using (var key = reg2.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
            {
                key.DeleteValue("OneDriveSetup", false);
            }

            // 64-bit Windows modifications
            if (Environment.Is64BitOperatingSystem)
            {
                using (var key = reg.OpenSubKey(rootKey, true))
                {
                    if (key != null)
                    {
                        key.SetValue("System.IsPinnedToNameSpaceTree", 0, RegistryValueKind.DWord);
                    }
                }

                using (var key = reg.OpenSubKey(rootKey + "\\ShellFolder", true))
                {
                    if (key != null)
                    {
                        key.SetValue("Attributes", byteArray, RegistryValueKind.DWord);
                    }
                }
            }

            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\OneDrive",
                "DisableFileSyncNGSC", "1", RegistryValueKind.DWord);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex.Message.ToString()}", "SystemTweakcs.UninstallOneDrive", MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
    }
}