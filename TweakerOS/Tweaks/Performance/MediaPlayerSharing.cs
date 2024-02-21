using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.Performance
{
    internal class MediaPlayerSharing : ITweak
    {
        public string Name => "Отключить совместное использование медиаплеера";

        public string Description => "Отключает совместное использование медиаплеера Windows.";

        public void Enable()
        {
            Utilities.StopService("WMPNetworkSvc");
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WMPNetworkSvc", "Start", "4", RegistryValueKind.DWord);
        }

        public void Disable()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WMPNetworkSvc", "Start", "2", RegistryValueKind.DWord);
            Utilities.StartService("WMPNetworkSvc");
        }

        public bool GetIsChanged()
        {
            int startValue = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WMPNetworkSvc", "Start", -1);
            return startValue != 2;
        }

        public bool ExplorerRebootRequires => false;
    }
}
