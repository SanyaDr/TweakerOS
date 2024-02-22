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
        throw new NotImplementedException();
    }

    public bool RebootRequires { get; }

    public void ApplyTweak()
    {
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WSearch", "Start", "2",
            RegistryValueKind.DWord);
        Utilities.StartService("WSearch");    }

    public void RestoreToFactory()
    {
        Utilities.StopService("WSearch");
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WSearch", "Start", "4",
            RegistryValueKind.DWord);
    }
}