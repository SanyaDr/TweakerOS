using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.Visual
{
    internal class DisablingSuffixArrowShortcut : ITweak
    {
        public string Name => "Отключение суффикса ярлык и стрелки ярлыка";

        public string Description => "Отключает стрелки на ярлыках и название ярлык для вновь созданных ярлыков";

        public bool RebootRequires => true;

        public void ApplyTweak()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Icons",
                "29", "%Windir%\\System32\\Shell32.dll,-50", RegistryValueKind.String);
        }

        public void RestoreToFactory()
        {
            Registry.CurrentUser.DeleteSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Icons", false);
        }

        public bool GetTweakIsApplied()
        {
            var value = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Icons", "29", null);
            return value != null && value.ToString() == "%Windir%\\System32\\Shell32.dll,-50";
        }
    }
}
