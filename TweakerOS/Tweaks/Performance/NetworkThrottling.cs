using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks.Performance
{
    internal class NetworkThrottling : ITweak
    {
        public string Name => "Отключить ограничение сети";

        public string Description => "Отключает ограничение сети в Windows.";

        public void Enable()
        {
            Int32 tempInt = Convert.ToInt32("ffffffff", 16);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile", "NetworkThrottlingIndex", tempInt, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Psched", "NonBestEffortLimit", 0, RegistryValueKind.DWord);
        }

        public void Disable()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Psched", "NonBestEffortLimit", 80, RegistryValueKind.DWord);
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile", true).DeleteValue("NetworkThrottlingIndex", false);
            Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Dnscache\Parameters", true).DeleteValue("MaxCacheTtl", false);
            Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Dnscache\Parameters", true).DeleteValue("MaxNegativeCacheTtl", false);
        }

        public bool GetIsChanged()
        {
            int networkThrottlingIndexValue = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "NetworkThrottlingIndex", -1);
            int nonBestEffortLimitValue = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Psched", "NonBestEffortLimit", -1);
            return networkThrottlingIndexValue == 80 || nonBestEffortLimitValue == 0;
        }

        public bool ExplorerRebootRequires { get; }
    }
}
