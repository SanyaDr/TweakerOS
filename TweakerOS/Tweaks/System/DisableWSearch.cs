using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.System;

/// <summary>
/// Отлючить индексаци файлов
/// </summary>
public class DisableWSearch : ITweak
{
    public string Name => "Отключить индексацию файлов";
    public string Description => "Отключает индексирование файлов";

    public bool GetTweakIsApplied()
    {
        return Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WSearch", "Start", -1) is int Start && Start == 4;
    }

    public bool RebootRequires { get; }

    public void ApplyTweak()
    {
        Utilities.StopService("WSearch");
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WSearch", "Start", "4",
            RegistryValueKind.DWord);
    }

    public void RestoreToFactory()
    {
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WSearch", "Start", "2",
           RegistryValueKind.DWord);
        Utilities.StartService("WSearch");
    }
}