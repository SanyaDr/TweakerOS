using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks.Performance
{
    internal class DesktopAndExplorerSettings : ITweak
    {
        public string Name => "Различные настройки рабочего стола и проводника";

        public string Description => "Устанавливают различные параметры настройки системы, " +
            "такие как время ожидания для завершения задач, время ожидания зависших приложений, " +
            "игнорирование проверок свободного места на диске и другие аспекты поведения операционной системы.";

        public void Disable()
        {
            Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true).DeleteValue("AutoEndTasks", false);
            Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true).DeleteValue("HungAppTimeout", false);
            Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true).DeleteValue("WaitToKillAppTimeout", false);
            Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true).DeleteValue("LowLevelHooksTimeout", false);
            Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true).DeleteValue("NoLowDiskSpaceChecks", false);
            Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true).DeleteValue("LinkResolveIgnoreLinkInfo", false);
            Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true).DeleteValue("NoResolveSearch", false);
            Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true).DeleteValue("NoResolveTrack", false);
            Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true).DeleteValue("NoInternetOpenWith", false);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control", "WaitToKillServiceTimeout", "5000", RegistryValueKind.DWord);
        }

        public void Enable()
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

        public bool GetIsChanged()
        {
            // Проверяем, внесены ли изменения
            bool settingsChanged = false;

            // Проверяем настройки рабочего стола
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "AutoEndTasks", null) as string != "1")
                settingsChanged = true;
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "HungAppTimeout", null) as string != "1000")
                settingsChanged = true;
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "WaitToKillAppTimeout", null) as string != "2000")
                settingsChanged = true;
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "LowLevelHooksTimeout", null) as string != "1000")
                settingsChanged = true;

            // Проверяем настройки проводника
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoLowDiskSpaceChecks", null) as string != "00000001")
                settingsChanged = true;
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "LinkResolveIgnoreLinkInfo", null) as string != "00000001")
                settingsChanged = true;
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoResolveSearch", null) as string != "00000001")
                settingsChanged = true;
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoResolveTrack", null) as string != "00000001")
                settingsChanged = true;
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoInternetOpenWith", null) as string != "00000001")
                settingsChanged = true;

            // Проверяем настройку таймаута завершения служб
            if ((int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control", "WaitToKillServiceTimeout", -1) != 5000)
                settingsChanged = true;

            return settingsChanged;
        }

        public bool ExplorerRebootRequires => false;
    }
}
