using Microsoft.Win32;
using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks.System;

public class GamingMode : ITweak
{
    public string Name => "Отключить Игровой режим";
    public string Description => "Отключает игровой режим в системе";

    public bool GetTweakIsApplied()
    {
        return Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers", "HwSchMode", -1) is int HwSchMode && HwSchMode == 1 &&
            Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "AllowAutoGameMode", -1) is int AllowAutoGameMode && AllowAutoGameMode == 0 &&
            Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "AutoGameModeEnabled", -1) is int AutoGameModeEnabled && AutoGameModeEnabled == 0 &&
            Registry.GetValue(@"HKEY_CURRENT_USER\System\GameConfigStore", "GameDVR_FSEBehaviorMode", -1) is int GameDVR_FSEBehaviorMode && GameDVR_FSEBehaviorMode == 0;
    }

    public bool RebootRequires { get; }

    public void ApplyTweak()
    {
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers", "HwSchMode", "1",
            RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "AllowAutoGameMode", 0,
            RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "AutoGameModeEnabled", 0,
            RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_CURRENT_USER\System\GameConfigStore", "GameDVR_FSEBehaviorMode", 0,
            RegistryValueKind.DWord);
    }

    public void RestoreToFactory()
    {
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GraphicsDrivers", "HwSchMode", "2",
            RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "AllowAutoGameMode", 1,
            RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "AutoGameModeEnabled", 1,
            RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_CURRENT_USER\System\GameConfigStore", "GameDVR_FSEBehaviorMode", 2,
            RegistryValueKind.DWord);
    }
}