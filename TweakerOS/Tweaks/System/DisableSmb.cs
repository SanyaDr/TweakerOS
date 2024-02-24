using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.System;

/// <summary>
/// Отключение службы SMB1
/// </summary>
public class DisableSmb : ITweak
{
    public string Name => "Отключить службу SMB";
    public string Description => "Отключить службу для локальных сетей SMB V1, она является небезопасной";
    public bool GetTweakIsApplied()
    {
        return Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters",
            $"SMB1", -1) is int SMB1 && SMB1 == 0;
    }

    public bool RebootRequires { get; }

    public void ApplyTweak()
    {
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters",
            $"SMB1", 0, RegistryValueKind.DWord);

    }

    public void RestoreToFactory()
    {
        Utilities.TryDeleteRegistryValue(true, @"SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters",
            $"SMB1");
    }
}