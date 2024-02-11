using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TweakerWin.TweakHelper;

namespace TweakerWin.Tweaks
{
    internal class SystemTweaks
    {
// #pragma warning disable CS8602 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.



        public void DisableNTFSTimeStamp()
        {
            Utilities.RunCommand("fsutil behavior set disablelastaccess 1");
        }

        public void EnableNTFSTimeStamp()
        {
            Utilities.RunCommand("fsutil behavior set disablelastaccess 2");
        }

        public void DisableErrorReporting()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting",
                "Disabled", 1);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\PCHealth\ErrorReporting", "DoReport", 0);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Windows Error Reporting", "Disabled", 1);

            Utilities.StopService("WerSvc");
            Utilities.StopService("wercplsupport");
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WerSvc", "Start", "4",
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\wercplsupport", "Start", "4",
                RegistryValueKind.DWord);
        }

        public void EnableErrorReporting()
        {
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting", true)
                .DeleteValue("Disabled", false);
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\PCHealth\ErrorReporting", true)
                .DeleteValue("DoReport", false);
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\Windows Error Reporting", true)
                .DeleteValue("Disabled", false);

            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\wercplsupport", "Start", "3",
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WerSvc", "Start", "2",
                RegistryValueKind.DWord);
            Utilities.StartService("WerSvc");
            Utilities.StartService("wercplsupport");
        }

        public void UninstallOneDrive()
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

        internal static void InstallOneDrive()
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

        internal static void DisableCortana()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\SearchSettings",
                "IsDeviceSearchHistoryEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "AllowCortana",
                "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search",
                "DisableWebSearch", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search",
                "ConnectedSearchUseWeb", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search",
                "ConnectedSearchUseWebOverMeteredConnections", "0", RegistryValueKind.DWord);

            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Search",
                "HistoryViewEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Search",
                "DeviceHistoryEnabled", "0", RegistryValueKind.DWord);

            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search",
                "AllowSearchToUseLocation", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search",
                "BingSearchEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "CortanaConsent",
                "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search",
                "AllowCloudSearch", "0", RegistryValueKind.DWord);
        }

        internal static void EnableCortana()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "AllowCortana",
                "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search",
                "DisableWebSearch", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search",
                "ConnectedSearchUseWeb", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search",
                "ConnectedSearchUseWebOverMeteredConnections", "1", RegistryValueKind.DWord);

            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Search",
                "HistoryViewEnabled", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Search",
                "DeviceHistoryEnabled", "1", RegistryValueKind.DWord);

            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search",
                "AllowSearchToUseLocation", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search",
                "BingSearchEnabled", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "CortanaConsent",
                "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search",
                "AllowCloudSearch", "1", RegistryValueKind.DWord);
        }

        internal static void EnableGamingMode()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers", "HwSchMode", "2",
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "AllowAutoGameMode", 1,
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "AutoGameModeEnabled", 1,
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\System\GameConfigStore", "GameDVR_FSEBehaviorMode", 2,
                RegistryValueKind.DWord);
        }

        internal static void DisableGamingMode()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers", "HwSchMode", "1",
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "AllowAutoGameMode", 0,
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "AutoGameModeEnabled", 0,
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\System\GameConfigStore", "GameDVR_FSEBehaviorMode", 0,
                RegistryValueKind.DWord);
        }

        internal static void DisableXboxLive()
        {
            Utilities.StopService("XboxNetApiSvc");
            Utilities.StopService("XblAuthManager");
            Utilities.StopService("XblGameSave");
            Utilities.StopService("XboxGipSvc");
            Utilities.StopService("xbgm");

            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XboxNetApiSvc", "Start", "4",
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XblAuthManager", "Start", "4",
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XblGameSave", "Start", "4",
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XboxGipSvc", "Start", "4",
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\xbgm", "Start", "4",
                RegistryValueKind.DWord);
        }

        internal static void EnableXboxLive()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XboxNetApiSvc", "Start", "2",
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XblAuthManager", "Start", "2",
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XblGameSave", "Start", "2",
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\XboxGipSvc", "Start", "2",
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\xbgm", "Start", "2",
                RegistryValueKind.DWord);

            Utilities.StartService("XboxNetApiSvc");
            Utilities.StartService("XblAuthManager");
            Utilities.StartService("XblGameSave");
            Utilities.StartService("XboxGipSvc");
            Utilities.StartService("xbgm");
        }

        internal static void DisableAutomaticUpdates()
        {
            Registry.SetValue(
                @"HKEY_USERS\S-1-5-20\Software\Microsoft\Windows\CurrentVersion\DeliveryOptimization\Settings",
                "DownloadMode", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WindowsUpdate\UX\Settings", "UxOption", "1",
                RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU",
                "NoAutoUpdate", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU", "AUOptions",
                "2", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU",
                "NoAutoRebootWithLoggedOnUsers", "1", RegistryValueKind.DWord);
            Registry.SetValue(
                @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\DeliveryOptimization\Config",
                "DODownloadMode", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Speech", "AllowSpeechModelUpdate", 0);
        }

        internal static void EnableAutomaticUpdates()
        {
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\DeliveryOptimization",
                "SystemSettingsDownloadMode");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\WindowsUpdate\UX\Settings", "UxOption");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU",
                "AUOptions");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU",
                "NoAutoUpdate");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU",
                "NoAutoRebootWithLoggedOnUsers");
            Utilities.TryDeleteRegistryValue(true,
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\DeliveryOptimization\Config", "DODownloadMode");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Speech", "AllowSpeechModelUpdate");

            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Schedule\Maintenance",
                "MaintenanceDisabled");
        }

        internal static void DisableStoreUpdates()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager",
                "SilentInstalledAppsEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\CloudContent",
                "DisableSoftLanding", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager",
                "PreInstalledAppsEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\CloudContent",
                "DisableWindowsConsumerFeatures", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager",
                "OemPreInstalledAppsEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\WindowsStore", "AutoDownload", "2",
                RegistryValueKind.DWord);
        }

        internal static void EnableStoreUpdates()
        {
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager",
                "SilentInstalledAppsEnabled");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\CloudContent",
                "DisableSoftLanding");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager",
                "PreInstalledAppsEnabled");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\CloudContent",
                "DisableWindowsConsumerFeatures");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager",
                "OemPreInstalledAppsEnabled");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\WindowsStore", "AutoDownload");
        }

        internal static void DisableSmartScreen()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Attachments",
                "SaveZoneInformation", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Policies\Attachments",
                "ScanWithAntiVirus", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "ShellSmartScreenLevel",
                "Warn", RegistryValueKind.String);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "EnableSmartScreen",
                "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer",
                "SmartScreenEnabled", "Off", RegistryValueKind.String);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\PhishingFilter", "EnabledV9",
                "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\AppHost", "PreventOverride",
                0, RegistryValueKind.DWord);

            using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                key.CreateSubKey(
                        @"SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings\Windows.SystemToast.SecurityAndMaintenance")
                    .SetValue("Enabled", 0);
            }
        }

        internal static void EnableSmartScreen()
        {
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\Policies\Attachments",
                "SaveZoneInformation");
            Utilities.TryDeleteRegistryValue(true, @"Software\Microsoft\Windows\CurrentVersion\Policies\Attachments",
                "ScanWithAntiVirus");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\System",
                "ShellSmartScreenLevel");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\System", "EnableSmartScreen");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer",
                "SmartScreenEnabled");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\Internet Explorer\PhishingFilter", "EnabledV9");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\AppHost",
                "PreventOverride");

            try
            {
                using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    key.OpenSubKey(
                        @"SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings\Windows.SystemToast.SecurityAndMaintenance",
                        true).DeleteValue("Enabled", false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message.ToString()}", "SystemTweaks.EnableSmartScreen", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }


#pragma warning restore CS8602 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
    }
}