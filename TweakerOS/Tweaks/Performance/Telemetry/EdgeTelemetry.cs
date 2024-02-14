using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.Performance
{
    class EdgeTelemetry : ITweak
    {
        public string Name => "Отключить телметрию Edge";

        public string Description => "Включает или отключает сбор телеметрии в браузере Microsoft Edge.";

        public void Enable()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Edge", "PersonalizationReportingEnabled", 0);
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Edge", "PersonalizationReportingEnabled", 0);
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Edge", "UserFeedbackAllowed", 0);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Edge", "UserFeedbackAllowed", 0);

            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Edge", "MetricsReportingEnabled", 0);
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Edge", "MetricsReportingEnabled", 0);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\MicrosoftEdge\BooksLibrary", "EnableExtendedBooksTelemetry", 0, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\MicrosoftEdge\BooksLibrary", "EnableExtendedBooksTelemetry", 0, RegistryValueKind.DWord);

            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Edge\SmartScreenEnabled", "", 0);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Edge\SmartScreenPuaEnabled", "", 0);

            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Edge", "SpotlightExperiencesAndRecommendationsEnabled", 0);
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Edge", "SpotlightExperiencesAndRecommendationsEnabled", 0);
        }

        public void Disable()
        {
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Edge\SmartScreenEnabled", "");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Edge\SmartScreenPuaEnabled", "");

            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Edge", "MetricsReportingEnabled");
            Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Policies\Microsoft\Edge", "MetricsReportingEnabled");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\MicrosoftEdge\BooksLibrary", "EnableExtendedBooksTelemetry");
            Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Policies\Microsoft\MicrosoftEdge\BooksLibrary", "EnableExtendedBooksTelemetry");

            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Edge", "PersonalizationReportingEnabled");
            Utilities.TryDeleteRegistryValue(true, @"Software\Policies\Microsoft\Edge", "UserFeedbackAllowed");
            Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Policies\Microsoft\Edge", "PersonalizationReportingEnabled");
            Utilities.TryDeleteRegistryValue(false, @"Software\Policies\Microsoft\Edge", "UserFeedbackAllowed");

            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Edge", "SpotlightExperiencesAndRecommendationsEnabled");
            Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Policies\Microsoft\Edge", "SpotlightExperiencesAndRecommendationsEnabled");
        }

        public bool GetIsChanged()
        {
            return false;
        }
    }
}
