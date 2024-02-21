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
    class NvidiaTelemetry : ITweak
    {
        public string Name => "Nvidia Telemetry";

        public string Description => ".";

        public void Enable()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\NvTelemetryContainer", "Start", 4);

            Utilities.RunCommand("schtasks.exe /change /tn NvTmRepOnLogon_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8} /disable");
            Utilities.RunCommand("schtasks.exe /change /tn NvTmRep_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8} /disable");
            Utilities.RunCommand("schtasks.exe /change /tn NvTmMon_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8} /disable");
            Utilities.RunCommand("net.exe stop NvTelemetryContainer");
            Utilities.RunCommand("sc.exe config NvTelemetryContainer start= disabled");
            Utilities.RunCommand("sc.exe stop NvTelemetryContainer");
        }

        public void Disable()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\NvTelemetryContainer", "Start", 2);

            Utilities.RunCommand("schtasks.exe /change /tn NvTmRepOnLogon_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8} /enable");
            Utilities.RunCommand("schtasks.exe /change /tn NvTmRep_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8} /enable");
            Utilities.RunCommand("schtasks.exe /change /tn NvTmMon_{B2FE1952-0186-46C3-BAEC-A80AA35AC5B8} /enable");
            Utilities.RunCommand("net.exe start NvTelemetryContainer");
            Utilities.RunCommand("sc.exe config NvTelemetryContainer start= enabled");
            Utilities.RunCommand("sc.exe start NvTelemetryContainer");
        }

        public bool GetIsChanged()
        {
            int startValue = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\NvTelemetryContainer", "Start", -1);
            return startValue != 2; 
        }

        public bool ExplorerRebootRequires { get; }
    }
}
