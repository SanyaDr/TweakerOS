using System.Diagnostics;

namespace TweakerWin.TweakHelper
{
    static internal class DeleteCMDHelper
    {
        static public void RunCmdCommand(string command)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            process.StartInfo = startInfo;
            process.Start();

            process.StandardInput.WriteLine(command);
            process.StandardInput.Flush();
            process.StandardInput.Close();

            process.WaitForExit();
            process.Close();
        }
    }
}

