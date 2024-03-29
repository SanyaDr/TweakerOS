﻿using Microsoft.Win32;
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

        public void RestoreToFactory()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Remote Assistance", "fAllowToGetHelp", "1", RegistryValueKind.DWord);
        }

        public void ApplyTweak()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Remote Assistance", "fAllowToGetHelp", "0", RegistryValueKind.DWord);
        }

        public bool GetTweakIsApplied()
        {
            // Проверяем, отключена ли уже удаленная помощь
            return Registry.GetValue(@"HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Remote Assistance", "fAllowToGetHelp", -1) is int fAllowToGetHelp && fAllowToGetHelp == 0;
        }

        public bool RebootRequires => false;
    }
}

