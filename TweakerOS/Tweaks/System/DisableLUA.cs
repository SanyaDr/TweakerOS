using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks.System
{
    internal class DisableLUA : ITweak
    {
        public string Name => "Отключить LUA";

        public string Description => "Отключает надоедливое сообщение контроля учетных записей при открытии программ + приоритет запуска WIN + R от администратора";

        public bool RebootRequires => false;

        public void ApplyTweak()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System",
                 "EnableLUA", 0, RegistryValueKind.DWord);
        }

        public void RestoreToFactory()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System",
                "EnableLUA", 1, RegistryValueKind.DWord);
        }

        public bool GetTweakIsApplied() =>
            Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", "EnableLUA", null) as int? == 0;
    }
}
