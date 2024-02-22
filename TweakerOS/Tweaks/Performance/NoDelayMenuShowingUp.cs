using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.Performance
{
    /// <summary>
    /// // Убирает задержку при отображении меню.
    /// </summary>
    internal class NoDelayMenuShowingUp : 
        ITweak
    {
        public string Name => "Отобразить все значки в панели задач";

        public string Description => "Отображает скрытые значки в панели задач в правом нижнем углу.";

        public void RestoreToFactory()
        {
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\Explorer", "EnableAutoTray");
        }

        public void ApplyTweak()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "EnableAutoTray", 0, RegistryValueKind.DWord);
        }

        public bool GetTweakIsApplied()
        {
            object registryValue = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "EnableAutoTray", null);
            if (registryValue != null && (int)registryValue == 0)
            {
                // Значение в реестре соответствует ожидаемому значению.
                return false;
            }
            else
            {
                // Значение в реестре отличается от ожидаемого.
                return true;
            }
        }

        public bool RebootRequires { get; }
    }
}
