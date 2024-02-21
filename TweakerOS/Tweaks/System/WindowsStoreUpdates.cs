using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.System;

public class WindowsStoreUpdates : ITweak
{
    public string Name => "Отключение обновлений Windows Store";
    public string Description => Name;

    public bool GetIsChanged()
    {
        throw new NotImplementedException();
    }

    public bool ExplorerRebootRequires { get; }

    public void Enable()
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

    public void Disable()
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
}