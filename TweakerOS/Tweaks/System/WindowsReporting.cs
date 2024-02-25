using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.System;

public class WindowsReporting : ITweak
{
    public string Name => "Отключить журнал об ошибках Windows";
    public string Description => "Отключает отчёты об ошибках Windows";

    public bool GetTweakIsApplied()
    {
        return Utilities.ServiceExists("wercplsupport") && !Utilities.IsServiceRunning("wercplsupport");
    }

    public bool RebootRequires { get; }

    public void ApplyTweak()
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

    public void RestoreToFactory()
    {
        Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\Windows Error Reporting", "Disabled");
        Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\PCHealth\ErrorReporting", "DoReport");
        Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\Windows\Windows Error Reporting", "Disabled");
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\wercplsupport", "Start", "3",
            RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WerSvc", "Start", "2",
            RegistryValueKind.DWord);
        Utilities.StartService("WerSvc");
        Utilities.StartService("wercplsupport");
    }
}