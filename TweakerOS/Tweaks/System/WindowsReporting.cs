using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.System;

public class WindowsReporting : ITweak
{
    public string Name => "Отключить журнал об ошибках Windows";
    public string Description => "Отключает отчёты об ошибках Windows";

    public bool GetIsChanged()
    {
        throw new NotImplementedException();
    }

    public bool ExplorerRebootRequires { get; }

    public void Enable()
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

    public void Disable()
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
        new NotImplementedException();
    }
}