using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.System;

public class WindowsAutoUpdate : ITweak
{
    public string Name => "Отключить автообновление Windows";
    public string Description => "Отключает автоматические обновления Windows";

    public bool GetTweakIsApplied()
    {
        throw new NotImplementedException();
    }

    public bool RebootRequires { get; }

    public void ApplyTweak()
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

    public void RestoreToFactory()
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
}