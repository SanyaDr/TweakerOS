using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks.Performance
{
    internal class DisableRemoteAssistance : ITweak
    {
        public string Name => "Отключить удаленную помощь";

        public string Description => "Отключает удаленную помощь.";

        public void Disable()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Remote Assistance", "fAllowToGetHelp", "1", RegistryValueKind.DWord);
        }

        public void Enable()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Remote Assistance", "fAllowToGetHelp", "0", RegistryValueKind.DWord);
        }

        public bool GetIsChanged()
        {
            // Проверяем, отключена ли уже удаленная помощь
            int fAllowToGetHelp = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Remote Assistance", "fAllowToGetHelp", -1);
            return (fAllowToGetHelp == 0);
        }
    }
}

