using Microsoft.Win32;
using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks.Performance
{
    class GameBar : ITweak
    {
        public string Name => "Отключить Game Bar";

        public string Description => "Включает или отключает Game Bar в Windows.";

        public void Disable()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AppCaptureEnabled", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AudioCaptureEnabled", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "CursorCaptureEnabled", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "UseNexusForGameBarEnabled", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "ShowStartupPanel", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\System\GameConfigStore", "GameDVR_Enabled", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\GameDVR", "AllowGameDVR", "1", RegistryValueKind.DWord);
        }

        public void Enable()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AppCaptureEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AudioCaptureEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "CursorCaptureEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "UseNexusForGameBarEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "ShowStartupPanel", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\System\GameConfigStore", "GameDVR_Enabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\GameDVR", "AllowGameDVR", "0", RegistryValueKind.DWord);
        }

        public bool GetIsChanged()
        {
            int appCaptureValue = (int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AppCaptureEnabled", 1);
            int audioCaptureValue = (int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AudioCaptureEnabled", 1);
            int cursorCaptureValue = (int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "CursorCaptureEnabled", 1);
            int useNexusValue = (int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "UseNexusForGameBarEnabled", 1);
            int showStartupValue = (int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "ShowStartupPanel", 1);
            int gameDVRValue = (int)Registry.GetValue(@"HKEY_CURRENT_USER\System\GameConfigStore", "GameDVR_Enabled", 1);
            int allowGameDVRValue = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\GameDVR", "AllowGameDVR", 1);

            return appCaptureValue != 0 || audioCaptureValue != 0 || cursorCaptureValue != 0 || useNexusValue != 0 || showStartupValue != 0 || gameDVRValue != 0 || allowGameDVRValue != 0;
        }

        public bool ExplorerRebootRequires { get; }
    }
}
