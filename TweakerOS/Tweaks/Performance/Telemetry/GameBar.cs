using Microsoft.Win32;
using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks.Performance
{
    class GameBar : ITweak
    {
        public string Name => "Отключить Game Bar";

        public string Description => "Включает или отключает Game Bar в Windows.";

        public void RestoreToFactory()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AppCaptureEnabled", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AudioCaptureEnabled", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "CursorCaptureEnabled", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "UseNexusForGameBarEnabled", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "ShowStartupPanel", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\System\GameConfigStore", "GameDVR_Enabled", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\GameDVR", "AllowGameDVR", "1", RegistryValueKind.DWord);
        }

        public void ApplyTweak()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AppCaptureEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AudioCaptureEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "CursorCaptureEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "UseNexusForGameBarEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "ShowStartupPanel", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\System\GameConfigStore", "GameDVR_Enabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\GameDVR", "AllowGameDVR", "0", RegistryValueKind.DWord);
        }

        public bool GetTweakIsApplied()
        {
            // Проверяем существование реестра
            var gameDVRKeyExists = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\GameDVR") != null;
            var gameBarKeyExists = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\GameBar") != null;
            var gameConfigStoreKeyExists = Registry.CurrentUser.OpenSubKey(@"System\GameConfigStore") != null;
            var gameDVRPolicyKeyExists = Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows\GameDVR") != null;

            // Если реестр существует, получаем значения ключей
            if (gameDVRKeyExists && gameBarKeyExists && gameConfigStoreKeyExists && gameDVRPolicyKeyExists)
            {
                var appCaptureValue = (int)(Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AppCaptureEnabled", 1) ?? 1);
                var audioCaptureValue = (int)(Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "AudioCaptureEnabled", 1) ?? 1);
                var cursorCaptureValue = (int)(Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\GameDVR", "CursorCaptureEnabled", 1) ?? 1);
                var useNexusValue = (int)(Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "UseNexusForGameBarEnabled", 1) ?? 1);
                var showStartupValue = (int)(Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\GameBar", "ShowStartupPanel", 1) ?? 1);
                var gameDVRValue = (int)(Registry.GetValue(@"HKEY_CURRENT_USER\System\GameConfigStore", "GameDVR_Enabled", 1) ?? 1);
                var allowGameDVRValue = (int)(Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\GameDVR", "AllowGameDVR", 1) ?? 1);

                // Возвращаем результат проверки
                return appCaptureValue != 0 || audioCaptureValue != 0 || cursorCaptureValue != 0 || useNexusValue != 0 || showStartupValue != 0 || gameDVRValue != 0 || allowGameDVRValue != 0;
            }
            else
            {
                // Если реестр не существует, считаем, что твик не применен
                return false;
            }
        }


        public bool RebootRequires { get; }
    }
}
