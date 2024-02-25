using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.Performance
{
    class VisualStudioTelemetry : ITweak
    {
        public string Name => "Отключить телеметрию Visual Studio";

        public string Description => "Включает или отключает сбор телеметрии в Visual Studio.";

        public void ApplyTweak()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\VisualStudio\Telemetry", "TurnOffSwitch", 1);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\VisualStudio\Feedback", "DisableFeedbackDialog", 1);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\VisualStudio\Feedback", "DisableEmailInput", 1);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\VisualStudio\Feedback", "DisableScreenshotCapture", 1);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\VisualStudio\SQM", "OptIn", 0);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\VisualStudio\Setup", "ConcurrentDownloads", 2, RegistryValueKind.DWord);

        }

        public void RestoreToFactory()
        {
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\VisualStudio\Telemetry", "TurnOffSwitch");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\VisualStudio\Feedback", "DisableFeedbackDialog");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\VisualStudio\Feedback", "DisableEmailInput");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\VisualStudio\Feedback", "DisableScreenshotCapture");
            Utilities.TryDeleteRegistryValue(true, @"Software\Policies\Microsoft\VisualStudio\SQM", "OptIn");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\VisualStudio\Setup", "ConcurrentDownloads");

        }

        public bool GetTweakIsApplied()
        {
            return Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\VisualStudio\Setup", "ConcurrentDownloads", -1) is int ConcurrentDownloads && ConcurrentDownloads == 2;
        }


        public bool RebootRequires { get; }
    }
}
