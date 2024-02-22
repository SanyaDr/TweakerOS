using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.System;

public class SmartScreen : ITweak
{
    public string Name => "Отключить SmartScreen";
    public string Description => Name;

    public bool GetTweakIsApplied()
    {
        throw new NotImplementedException();
    }

    public bool RebootRequires { get; }

    public void ApplyTweak()
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

    public void RestoreToFactory()
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
        
        using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
        {
            key.OpenSubKey(
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Notifications\Settings\Windows.SystemToast.SecurityAndMaintenance",
                true).DeleteValue("Enabled", false);
        }
        
        // TODO тут был try-catch обрати внимание на обработку ошибок на верхнем уровне, или проверь как это исправить
    }
}