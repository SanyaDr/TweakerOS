using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.AccessControl;
using System.ServiceProcess;
using System.Windows;


namespace TweakerWin.TweakHelper
{
    internal class Utilities
    {
        internal static bool ServiceExists(string serviceName)
        {
            return Array.Exists(ServiceController.GetServices(), (serviceController => serviceController.ServiceName.Equals(serviceName)));
        }

        internal static void StopService(string serviceName)
        {
            if (ServiceExists(serviceName))
            {
                ServiceController sc = new ServiceController(serviceName);
                if (sc.CanStop)
                {
                    sc.Stop();
                }
            }
        }

        internal static void StartService(string serviceName)
        {
            if (ServiceExists(serviceName))
            {
                ServiceController sc = new ServiceController(serviceName);

                try
                {
                    sc.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message.ToString()}", "Eror Service.start", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        internal static void RunCommand(string command)
        {
            if (string.IsNullOrEmpty(command)) return;

            using (Process p = new Process())
            {
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "/C " + command;
                p.StartInfo.CreateNoWindow = true;

                try
                {
                    p.Start();
                    p.WaitForExit();
                    p.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message.ToString()}", "Utilities.RunCommand", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        internal static void TryDeleteRegistryValue(bool localMachine, string path, string valueName)
        {
            try
            {
                if (localMachine) Registry.LocalMachine.OpenSubKey(path, true).DeleteValue(valueName, false);
                if (!localMachine) Registry.CurrentUser.OpenSubKey(path, true).DeleteValue(valueName, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message.ToString()}", "Utilities.TryDeleteRegistryValue", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


    }
}
