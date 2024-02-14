using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.Performance
{
    class VisualStudioTelemetry : ITweak
    {
        public string Name => "Отключить телеметрию Visual Studio";

        public string Description => "Включает или отключает сбор телеметрии в Visual Studio.";

        public void Enable()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\VisualStudio\Telemetry", "TurnOffSwitch", 1);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\VisualStudio\Feedback", "DisableFeedbackDialog", 1);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\VisualStudio\Feedback", "DisableEmailInput", 1);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\VisualStudio\Feedback", "DisableScreenshotCapture", 1);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\VisualStudio\SQM", "OptIn", 0);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\VisualStudio\Setup", "ConcurrentDownloads", 2, RegistryValueKind.DWord);

            if (Environment.Is64BitOperatingSystem)
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VSCommon\14.0\SQM", "OptIn", 0);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VSCommon\15.0\SQM", "OptIn", 0);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VSCommon\16.0\SQM", "OptIn", 0);
            }
            else
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\VSCommon\14.0\SQM", "OptIn", 0);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\VSCommon\15.0\SQM", "OptIn", 0);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\VSCommon\16.0\SQM", "OptIn", 0);
            }
        }

        public void Disable()
        {
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\VisualStudio\Telemetry", "TurnOffSwitch");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\VisualStudio\Feedback", "DisableFeedbackDialog");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\VisualStudio\Feedback", "DisableEmailInput");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\VisualStudio\Feedback", "DisableScreenshotCapture");
            Utilities.TryDeleteRegistryValue(true, @"Software\Policies\Microsoft\VisualStudio\SQM", "OptIn");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\VisualStudio\Setup", "ConcurrentDownloads");

            if (Environment.Is64BitOperatingSystem)
            {
                Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Microsoft\VSCommon\14.0\SQM", "OptIn");
                Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Microsoft\VSCommon\15.0\SQM", "OptIn");
                Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Microsoft\VSCommon\16.0\SQM", "OptIn");
            }
            else
            {
                Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Wow6432Node\Microsoft\VSCommon\14.0\SQM", "OptIn");
                Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Wow6432Node\Microsoft\VSCommon\15.0\SQM", "OptIn");
                Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Wow6432Node\Microsoft\VSCommon\16.0\SQM", "OptIn");
            }
        }

        public bool GetIsChanged()
        {

            bool isChanged = false;

            int turnOffSwitchValue = (int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\VisualStudio\Telemetry", "TurnOffSwitch", -1);
            int disableFeedbackDialogValue = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\VisualStudio\Feedback", "DisableFeedbackDialog", -1);
            int disableEmailInputValue = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\VisualStudio\Feedback", "DisableEmailInput", -1);
            int disableScreenshotCaptureValue = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\VisualStudio\Feedback", "DisableScreenshotCapture", -1);
            int optInValue = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\VisualStudio\SQM", "OptIn", -1);
            int concurrentDownloadsValue = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\VisualStudio\Setup", "ConcurrentDownloads", -1);

            if (turnOffSwitchValue != 1 ||
                disableFeedbackDialogValue != 1 ||
                disableEmailInputValue != 1 ||
                disableScreenshotCaptureValue != 1 ||
                optInValue != 0 ||
                concurrentDownloadsValue != 2)
            {
                isChanged = true;
            }

            if (Environment.Is64BitOperatingSystem)
            {
                int sqm14Value = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VSCommon\14.0\SQM", "OptIn", -1);
                int sqm15Value = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VSCommon\15.0\SQM", "OptIn", -1);
                int sqm16Value = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VSCommon\16.0\SQM", "OptIn", -1);

                if (sqm14Value != 0 || sqm15Value != 0 || sqm16Value != 0)
                {
                    isChanged = true;
                }
            }
            else
            {
                int sqm14Value = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\VSCommon\14.0\SQM", "OptIn", -1);
                int sqm15Value = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\VSCommon\15.0\SQM", "OptIn", -1);
                int sqm16Value = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\VSCommon\16.0\SQM", "OptIn", -1);

                if (sqm14Value != 0 || sqm15Value != 0 || sqm16Value != 0)
                {
                    isChanged = true;
                }
            }

            return isChanged;
        }
    }
}
