using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.SystemServices
{
    internal class PrintService : ITweak
    {
        public string Name => "Отключить службу печати";

        public string Description => "Отключает службу печати в Windows.";

        public void Disable()
        {
            Utilities.StopService("Spooler");
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Spooler", "Start", "3", RegistryValueKind.DWord);
        }

        public void Enable()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Spooler", "Start", "2", RegistryValueKind.DWord);
            Utilities.StartService("Spooler");
        }

        public bool GetIsChanged()
        {
            int startValue = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Spooler", "Start", -1);
            return startValue != 2;
        }
    }
}
