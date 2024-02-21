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

    public bool GetIsChanged()
    {
        throw new NotImplementedException();
    }

    public bool ExplorerRebootRequires { get; }

    public void Enable()
    {
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WSearch", "Start", "2",
            RegistryValueKind.DWord);
        Utilities.StartService("WSearch");    }

    public void Disable()
    {
        Utilities.StopService("WSearch");
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WSearch", "Start", "4",
            RegistryValueKind.DWord);
    }
}