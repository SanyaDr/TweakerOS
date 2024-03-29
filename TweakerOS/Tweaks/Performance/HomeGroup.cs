﻿using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.Performance
{
    internal class HomeGroup : ITweak
    {
        public string Name => "Отключить домашнюю группу";

        public string Description => "Отключает домашнюю группу в Windows.";

        public void ApplyTweak()
        {
            Utilities.StopService("HomeGroupListener");
            Utilities.StopService("HomeGroupProvider");

            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\HomeGroup", "DisableHomeGroup", "1", RegistryValueKind.DWord);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\HomeGroupListener", "Start", "4", RegistryValueKind.DWord);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\HomeGroupProvider", "Start", "4", RegistryValueKind.DWord);
        }

        public void RestoreToFactory()
        {
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\HomeGroupListener", "Start", "2", RegistryValueKind.DWord);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\HomeGroupProvider", "Start", "2", RegistryValueKind.DWord);
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\HomeGroup", "DisableHomeGroup");

            Utilities.StartService("HomeGroupListener");
            Utilities.StartService("HomeGroupProvider");
        }

        public bool GetTweakIsApplied()
        {
            int listenerStartValue = (int)(Registry.GetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\HomeGroupListener", "Start", -1) ?? -1);
            int providerStartValue = (int)(Registry.GetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\HomeGroupProvider", "Start", -1) ?? -1);
            int homegroupmain = (int)(Registry.GetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\HomeGroupProvider", "Start", -1) ?? -1);
            return listenerStartValue == 4 || providerStartValue == 4 || homegroupmain == 1;
        }

        public bool RebootRequires => false;
    }
}
