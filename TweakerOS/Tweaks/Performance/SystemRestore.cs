using Microsoft.Win32;
using System.Diagnostics;
using System.Windows;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.SystemServices
{
    internal class SystemRestore : ITweak
    {
        public string Name => "Отключить системное восстановление";

        public string Description => "Отключает системное восстановление в Windows.";

        public void RestoreToFactory()
        {
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore", "DisableSR");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore", "DisableConfig");

            Utilities.StartService("VSS");
        }

        public void ApplyTweak()
        {
            Utilities.StopService("VSS");

            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore", "DisableSR", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore", "DisableConfig", "1", RegistryValueKind.DWord);
        }

        public bool GetTweakIsApplied()
        {
            try
            {
                int disableSRValue = (int)(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore", "DisableSR", -1) ?? -1);
                int disableConfigValue = (int)(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore", "DisableConfig", -1) ?? -1);

                return disableSRValue == 1 && disableConfigValue == 1;
            }catch
            {
                throw new NotImplementedException();
            }
        }


        public bool RebootRequires => false;

    }
}
