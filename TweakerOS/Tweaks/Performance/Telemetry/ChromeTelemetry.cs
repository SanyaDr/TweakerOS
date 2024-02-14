using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.Performance
{
    class ChromeTelemetry : ITweak
    {
        public string Name => "Chrome Telemetry";

        public string Description => ".";

        public void Enable()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Google\Chrome", "MetricsReportingEnabled", 0);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Google\Chrome", "ChromeCleanupReportingEnabled", 0);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Google\Chrome", "ChromeCleanupEnabled", 0);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Google\Chrome", "UserFeedbackAllowed", 0);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Google\Chrome", "DeviceMetricsReportingEnabled", 0);
        }

        public void Disable()
        {
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Google\Chrome", "MetricsReportingEnabled");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Google\Chrome", "ChromeCleanupReportingEnabled");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Google\Chrome", "ChromeCleanupEnabled");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Google\Chrome", "UserFeedbackAllowed");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Google\Chrome", "DeviceMetricsReportingEnabled");
        }

        public bool GetIsChanged()
        {
            return Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Google\Chrome", "MetricsReportingEnabled", null) != null ||
                   Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Google\Chrome", "ChromeCleanupReportingEnabled", null) != null ||
                   Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Google\Chrome", "ChromeCleanupEnabled", null) != null ||
                   Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Google\Chrome", "UserFeedbackAllowed", null) != null ||
                   Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Google\Chrome", "DeviceMetricsReportingEnabled", null) != null;
        }
    }
}
