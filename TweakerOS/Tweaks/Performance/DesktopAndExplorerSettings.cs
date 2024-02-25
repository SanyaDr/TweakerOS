using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.Performance
{
    internal class DesktopAndExplorerSettings : ITweak
    {
        public string Name => "Различные настройки рабочего стола и проводника";

        public string Description => "Устанавливают различные параметры настройки системы, " +
            "такие как время ожидания для завершения задач, время ожидания зависших приложений, " +
            "игнорирование проверок свободного места на диске и другие аспекты поведения операционной системы.";

        public void RestoreToFactory()
        {
            Utilities.TryDeleteRegistryValue(false, @"Control Panel\Desktop", "AutoEndTasks");
            Utilities.TryDeleteRegistryValue(false, @"Control Panel\Desktop", "HungAppTimeout");
            Utilities.TryDeleteRegistryValue(false, @"Control Panel\Desktop", "WaitToKillAppTimeout");
            Utilities.TryDeleteRegistryValue(false, @"Control Panel\Desktop", "LowLevelHooksTimeout");

            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoLowDiskSpaceChecks");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "LinkResolveIgnoreLinkInfo");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoResolveSearch");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoResolveTrack");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoInternetOpenWith");

            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control", "WaitToKillServiceTimeout", "5000", RegistryValueKind.DWord);
        }

        public void ApplyTweak()
        {
            Registry.SetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "AutoEndTasks", "1", RegistryValueKind.DWord);
            Registry.SetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "HungAppTimeout", "1000", RegistryValueKind.DWord);
            Registry.SetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "WaitToKillAppTimeout", "2000", RegistryValueKind.DWord);
            Registry.SetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "LowLevelHooksTimeout", "1000", RegistryValueKind.DWord);
            Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer", "NoLowDiskSpaceChecks", "00000001", RegistryValueKind.DWord);
            Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer", "LinkResolveIgnoreLinkInfo", "00000001", RegistryValueKind.DWord);
            Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer", "NoResolveSearch", "00000001", RegistryValueKind.DWord);
            Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer", "NoResolveTrack", "00000001", RegistryValueKind.DWord);
            Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer", "NoInternetOpenWith", "00000001", RegistryValueKind.DWord);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control", "WaitToKillServiceTimeout", "2000", RegistryValueKind.DWord);
        }

        public bool GetTweakIsApplied()
        {
            bool settingsChanged = false;

            // Проверяем настройки рабочего стола
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "AutoEndTasks", null) is int autoEndTasks && autoEndTasks == 1)
                settingsChanged = true;
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "HungAppTimeout", null) is int hungAppTimeout && hungAppTimeout == 1000)
                settingsChanged = true;
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "WaitToKillAppTimeout", null) is int waitToKillAppTimeout && waitToKillAppTimeout == 2000)
                settingsChanged = true;
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "LowLevelHooksTimeout", null) is int lowLevelHooksTimeout && lowLevelHooksTimeout == 1000)
                settingsChanged = true;

            // Проверяем настройки проводника
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoLowDiskSpaceChecks", null) is int noLowDiskSpaceChecks && noLowDiskSpaceChecks == 1)
                settingsChanged = true;
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "LinkResolveIgnoreLinkInfo", null) is int linkResolveIgnoreLinkInfo && linkResolveIgnoreLinkInfo == 1)
                settingsChanged = true;
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoResolveSearch", null) is int noResolveSearch && noResolveSearch == 1)
                settingsChanged = true;
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoResolveTrack", null) is int noResolveTrack && noResolveTrack == 1)
                settingsChanged = true;
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoInternetOpenWith", null) is int noInternetOpenWith && noInternetOpenWith == 1)
                settingsChanged = true;

            // Проверяем настройку таймаута завершения служб
            if (Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control", "WaitToKillServiceTimeout", -1) is int waitToKillServiceTimeout && waitToKillServiceTimeout == 2000)
                settingsChanged = true;

            return settingsChanged;
        }


        public bool RebootRequires => false;
    }
}
