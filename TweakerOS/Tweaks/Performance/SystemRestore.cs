using Microsoft.Win32;
using System.Diagnostics;
using System.Windows;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.SystemServices
{
    internal class SystemRestore : ITweak
    {
        public string Name => "Отключить системное восстановление";

        public string Description => "Отключает системное восстановление в Windows.";

        public void RestoreToFactory()
        {
            try
            {
                using (Process p = new Process())
                {
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.FileName = "vssadmin";
                    p.StartInfo.Arguments = "delete shadows /for=c: /all /quiet";
                    p.StartInfo.UseShellExecute = false;

                    p.Start();
                    p.WaitForExit();
                    p.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message.ToString()}", "PerformanceTweak.DisableSystemRestore", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            Utilities.StopService("VSS");

            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore", "DisableSR", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore", "DisableConfig", "1", RegistryValueKind.DWord);
        }

        public void ApplyTweak()
        {
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore", "DisableSR");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore", "DisableConfig");

            Utilities.StartService("VSS");
        }

        public bool GetTweakIsApplied()
        {
            int vssStartValue = (int)Registry.GetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\VSS", "Start", -1);
            int disableSRValue = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore", "DisableSR", -1);
            int disableConfigValue = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore", "DisableConfig", -1);

            return vssStartValue != 2 || disableSRValue != 1 || disableConfigValue != 1;
        }

        public bool RebootRequires => false;

    }
}
