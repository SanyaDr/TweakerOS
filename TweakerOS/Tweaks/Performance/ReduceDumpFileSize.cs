using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks.Performance
{
    /// <summary>
    /// Уменьшает размер файлов дампа при крахе.
    /// </summary>
    internal class ReduceDumpFileSize : ITweak
    {
        public string Name => "Уменьшить размер дампа";

        public string Description => "Уменьшает размер файлов дампа при крахе.";

        public void RestoreToFactory()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\CrashControl", "CrashDumpEnabled", 7, RegistryValueKind.DWord);
        }

        public void ApplyTweak()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\CrashControl", "CrashDumpEnabled", 3, RegistryValueKind.DWord);
        }

        public bool GetTweakIsApplied()
        {
            int currentCrashDumpEnabled = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\CrashControl", "CrashDumpEnabled", -1);
            return (currentCrashDumpEnabled == 3);
        }

        public bool RebootRequires { get; }
    }
    }

