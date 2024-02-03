using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweakerWin.Interface;

namespace TweakerWin.Tweaks
{
    internal class Tweak1 : Itweak
    {
        public string Name;

        public void Action()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "EnableAutoTray", "0", RegistryValueKind.DWord);
        }

    }
}
