using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.System;

public class SmartScreen : ITweak
{
    public string Name => "Отключить SmartScreen";
    public string Description => "SmartScreen — это функция встроенной системы безопасности операционной системы Windows. Она предназначена для защиты компьютера от вредоносного программного обеспечения, скачанного из сети";

    public bool GetTweakIsApplied()
    {
        return Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Attachments",
            "SaveZoneInformation", -1) is int SaveZoneInformation && SaveZoneInformation == 1 &&
            Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System",
            "ShellSmartScreenLevel", "-1") is string ShellSmartScreenLevel && ShellSmartScreenLevel == "Warn" &&
            Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer",
            "SmartScreenEnabled", "-1") is string SmartScreenEnabled && SmartScreenEnabled == "Off" &&
            Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\AppHost", "PreventOverride",
            -1) is int PreventOverride && PreventOverride == 0;


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
        
    }
}