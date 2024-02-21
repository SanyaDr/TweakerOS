using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.System;

/// <summary>
/// Отключение службы SMB1
/// </summary>
public class DisableSmb: ITweak
{
    public string Name => "Отключить службу SMB";
    public string Description => "Отключить службу для локальных сетей SMB, она является небезопасной";
    public bool GetIsChanged()
    {
        throw new NotImplementedException();
    }

    public bool ExplorerRebootRequires { get; }

    public void Enable()
    {
        Utilities.TryDeleteRegistryValue(true, @"SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters",
            $"SMB1");    }

    public void Disable()
    {
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters",
            $"SMB1", 0, RegistryValueKind.DWord);
    }
}