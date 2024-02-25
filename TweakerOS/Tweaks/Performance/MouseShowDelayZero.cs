using Microsoft.Win32;
using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks.Performance
{
    internal class MouseShowDelayZero : ITweak
    {
        public string Name => "Убрать задержку отображения меню";

        public string Description => "Устанавливает задержку отображения меню и времени задержки при наведении мыши в ноль.";

        public void RestoreToFactory()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "MenuShowDelay", "400", RegistryValueKind.String);
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "MouseHoverTime", "400", RegistryValueKind.String);
        }

        public void ApplyTweak()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "MenuShowDelay", "0", RegistryValueKind.String);
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "MouseHoverTime", "0", RegistryValueKind.String);
        }

        public bool GetTweakIsApplied()
        {
            string currentMenuShowDelay = Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop", "MenuShowDelay", null) as string?? string.Empty;
            string currentMouseHoverTime = Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "MouseHoverTime", null) as string ?? string.Empty;
            return (currentMenuShowDelay == "0" && currentMouseHoverTime == "0");
        }

        public bool RebootRequires => false;
    }
}
