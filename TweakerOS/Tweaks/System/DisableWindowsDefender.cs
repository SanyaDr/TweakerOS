using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.System;

/// <summary>
/// Отключить Windows Defender
/// </summary>
public class DisableWindowsDefender:ITweak
{
    public string Name => "Отключить защитник Windows";
    public string Description => "Отключает антивирус Windows Defender \nНЕ РЕКОМЕНДУЕТСЯ!";
    public bool GetIsChanged()
    {
        throw new NotImplementedException();
    }

    public bool ExplorerRebootRequires { get; }

    public void Enable()
    {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiVirus", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender", "DisableSpecialRunningModes", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender", "DisableRoutinelyTakingAction", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender", "ServiceKeepAlive", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableRealtimeMonitoring", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Signature Updates", "ForceUpdateFromMU", 0);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Spynet", "DisableBlockAtFirstSeen", 1);

            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\MpEngine", "MpEnablePus", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender", "PUAProtection", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Policy Manager", "DisableScanningNetworkFiles", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender", "DisableRealtimeMonitoring", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows Defender\Spynet", "SpyNetReporting", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows Defender\Spynet", "SubmitSamplesConsent", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\MRT", "DontReportInfectionInformation", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\MRT", "DontOfferThroughWUAU", "1", RegistryValueKind.DWord);
            Registry.ClassesRoot.DeleteSubKeyTree(@"\CLSID\{09A47860-11B0-4DA5-AFA5-26D86198A780}", false);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableBehaviorMonitoring", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableOnAccessProtection", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableScanOnRealtimeEnable", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableIOAVProtection", "1", RegistryValueKind.DWord);

            RegistryKey k = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);

            using (RegistryKey tmp = k.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                tmp.DeleteValue("WindowsDefender", false);
                tmp.DeleteValue("SecurityHealth", false);
            }
    }

    public void Disable()
    {
        //TODO check null ref..
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\MpEngine", "MpEnablePus", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender", "PUAProtection", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Policy Manager", "DisableScanningNetworkFiles", "0", RegistryValueKind.DWord);
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender", true).DeleteValue("DisableRealtimeMonitoring", false);
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender", true).DeleteValue("DisableAntiSpyware", false);
            Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows Defender\Spynet", true).DeleteValue("SpyNetReporting", false);
            Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows Defender\Spynet", true).DeleteValue("SubmitSamplesConsent", false);
            Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\MRT", true).DeleteValue("DontReportInfectionInformation", false);
            Registry.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\MRT", true).DeleteValue("DontOfferThroughWUAU", false);
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", true).DeleteValue("DisableBehaviorMonitoring", false);
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", true).DeleteValue("DisableOnAccessProtection", false);
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", true).DeleteValue("DisableScanOnRealtimeEnable", false);
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", true).DeleteValue("DisableIOAVProtection", false);

            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiVirus");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows Defender", "DisableSpecialRunningModes");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows Defender", "DisableRoutinelyTakingAction");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows Defender", "ServiceKeepAlive");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableRealtimeMonitoring");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows Defender\Signature Updates", "ForceUpdateFromMU");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows Defender\Spynet", "DisableBlockAtFirstSeen");
    }
}