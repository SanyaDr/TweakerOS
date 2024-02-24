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
    class FirefoxTelemetry : ITweak
    {
        public string Name => "Firefox Telemetry";

        public string Description => ".";

        public void ApplyTweak()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Mozilla\Firefox", "DisableTelemetry", 1);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Mozilla\Firefox", "DisableDefaultBrowserAgent", 1);

            Utilities.RunCommand("schtasks.exe /change /disable /tn \"\\Mozilla\\Firefox Default Browser Agent 308046B0AF4A39CB\"");
            Utilities.RunCommand("schtasks.exe /change /disable /tn \"\\Mozilla\\Firefox Default Browser Agent D2CEEC440E2074BD\"");
        }

        public void RestoreToFactory()
        {
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Mozilla\Firefox", "DisableTelemetry");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Mozilla\Firefox", "DisableDefaultBrowserAgent");

            Utilities.RunCommand("schtasks.exe /change /enable /tn \"\\Mozilla\\Firefox Default Browser Agent 308046B0AF4A39CB\"");
            Utilities.RunCommand("schtasks.exe /change /enable /tn \"\\Mozilla\\Firefox Default Browser Agent D2CEEC440E2074BD\"");
        }

        public bool GetTweakIsApplied()
        {
            return Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Mozilla\Firefox", "DisableTelemetry", null) != null ||
                   Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Mozilla\Firefox", "DisableDefaultBrowserAgent", null) != null;
        }

        public bool RebootRequires { get; }
    }
}
