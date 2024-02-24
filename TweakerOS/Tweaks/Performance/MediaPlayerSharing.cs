using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.Performance
{
    internal class MediaPlayerSharing : ITweak
    {
        public string Name => "Отключить совместное использование медиаплеера";

        public string Description => "Отключает совместное использование медиаплеера Windows.";

        public void ApplyTweak()
        {
            Utilities.StopService("WMPNetworkSvc");
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WMPNetworkSvc", "Start", "4", RegistryValueKind.DWord);
        }

        public void RestoreToFactory()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WMPNetworkSvc", "Start", "2", RegistryValueKind.DWord);
            Utilities.StartService("WMPNetworkSvc");
        }

        public bool GetTweakIsApplied()
        {
            return Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WMPNetworkSvc", "Start", -1) is int WMPNetworkSvc && WMPNetworkSvc == 4;
        }

        public bool RebootRequires => false;
    }
}
