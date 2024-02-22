using System.Diagnostics;

namespace TweakerOS.Controllers;

public static class Rebooter
{
    public static void RebootExplorer()
    {
        foreach (Process p in Process.GetProcesses())
        {
            // In case we get Access Denied
            try
            {
                if (p.MainModule.FileName.ToLower().EndsWith(":\\windows\\explorer.exe"))
                {
                    p.Kill();
                    break;
                }
            }
            catch
            {
                // ignored
            }
        }

        Process.Start("explorer.exe");
    }
}