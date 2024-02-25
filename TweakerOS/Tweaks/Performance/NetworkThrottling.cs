using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.Performance
{
    internal class NetworkThrottling : ITweak
    {
        public string Name => "Отключить ограничение сети";

        public string Description => "Отключает ограничение сети в Windows.";

        public void ApplyTweak()
        {
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile",
                "NetworkThrottlingIndex", unchecked((int)0xFFFFFFFF), RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Psched",
                "NonBestEffortLimit", 0, RegistryValueKind.DWord);
        }

        public void RestoreToFactory()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Psched",
                "NonBestEffortLimit", 80, RegistryValueKind.DWord);
            Utilities.TryDeleteRegistryValue(true,
                @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "NetworkThrottlingIndex");
        }

        public bool GetTweakIsApplied()
        {
            int IndexValue = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile","NetworkThrottlingIndex", -1)!;
            int LimitValue = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Psched", "NonBestEffortLimit", -1)!;
            return IndexValue == unchecked((int)0xFFFFFFFF) && LimitValue == 0;
        }

        public bool RebootRequires => false;
    }
}
