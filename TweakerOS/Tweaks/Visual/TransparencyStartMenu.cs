using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.Visual
{
    internal class TransparencyStartMenu : ITweak
    {
        public string Name => "Добавить прозрачности меню пуск";

        public string Description => "Добавляет прозрачность в меню пуск";

        public bool RebootRequires => true;

        public void ApplyTweak()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize",
                 "EnableTransparency", "2", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
                "UseOLEDTaskbarTransparency", "1", RegistryValueKind.DWord);
        }

        public void RestoreToFactory()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize",
                "EnableTransparency", "0", RegistryValueKind.DWord);
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
                "UseOLEDTaskbarTransparency");
        }

        public bool GetTweakIsApplied() =>
           (Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", null) as int? == 2)
           || (Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "UseOLEDTaskbarTransparency", null) as int? == 1);

    }
}
