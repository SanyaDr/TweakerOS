using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks.Performance
{
    /// <summary>
    /// // Устанавливает задержку отображения меню и времени задержки при наведении мыши в ноль.
    /// </summary>
    internal class MouseShowDelayZero : ITweak
    {
        public string Name => "Убрать задержку отображения меню";

        public string Description =>
            "// Устанавливает задержку отображения меню и времени задержки при наведении мыши в ноль.";

        public void RestoreToFactory()
        {
            Registry.SetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "MenuShowDelay", "400");
            Registry.SetValue("HKEY_CURRENT_USER\\Control Panel\\Mouse", "MouseHoverTime", "400");
        }

        public void ApplyTweak()
        {
            Registry.SetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "MenuShowDelay", 0);
            Registry.SetValue("HKEY_CURRENT_USER\\Control Panel\\Mouse", "MouseHoverTime", 0);
        }

        public bool GetTweakIsApplied()
        {
            int.TryParse(Registry.GetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "MenuShowDelay", -1).ToString(),
                out int currentMenuShowDelay);
            int.TryParse(Registry.GetValue("HKEY_CURRENT_USER\\Control Panel\\Mouse", "MouseHoverTime", -1).ToString(),
                out int currentMouseHoverTime);

            // Если значения отличаются от ожидаемого (0), то возвращаем true, иначе false
            return (currentMenuShowDelay != 0 || currentMouseHoverTime != 0);
        }

        public bool RebootRequires { get; }
    }
}