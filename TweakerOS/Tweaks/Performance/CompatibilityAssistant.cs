using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.Performance
{
    internal class CompatibilityAssistant : ITweak
    {
        public string Name => "Включить службу совместимости";

        public string Description => "Включает службу совместимости в Windows.";

        public void Disable()
        {
            Utilities.StopService("PcaSvc");
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PcaSvc", "Start", "4", RegistryValueKind.DWord);
        }

        public void Enable()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PcaSvc", "Start", "2", RegistryValueKind.DWord);
            Utilities.StartService("PcaSvc");
        }

        public bool GetIsChanged()
        {
            int startValue = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PcaSvc", "Start", -1)!;
            return startValue != 2;
        }

        public bool ExplorerRebootRequires => false;
    }
}
