﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.Performance
{
    internal class ServiceTweaksStoped : ITweak
    {
        public string Name => "Отключить не нужные системные службы";

        public string Description => "DiagTrack: Эта служба собирает данные диагностики и использования приложений для отправки их в Microsoft. " +
            "Она помогает Microsoft улучшать качество своего программного обеспечения и предоставлять дополнительные функции.\r\n\r\n" +
            "diagnosticshub.standardcollector.service: Эта служба также связана с сбором и отправкой диагностических данных в Microsoft. " +
            "Она может использоваться для сбора информации о работе системы и приложений для анализа и улучшения производительности " +
            "и стабильности.\r\n\r\ndmwappushservice: Эта служба отвечает за получение и установку универсальных приложений из Windows Store. " +
            "Она также может выполнять другие задачи, связанные с обновлением и управлением приложениями из магазина Windows.";

        public void Disable()
        {
            // Включаем службы
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DiagTrack", "Start", "2", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service", "Start", "2", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\dmwappushservice", "Start", "2", RegistryValueKind.DWord);

            // Включение службы удаленного реестра
            Utilities.RunCommand("sc config \"RemoteRegistry\" start= auto");

            Utilities.StartService("DiagTrack");
            Utilities.StartService("diagnosticshub.standardcollector.service");
            Utilities.StartService("dmwappushservice");
        }

        public void Enable()
        {
            // Останавливаем службы
            Utilities.StopService("DiagTrack");
            Utilities.StopService("diagnosticshub.standardcollector.service");
            Utilities.StopService("dmwappushservice");

            // Отключаем службу удаленного реестра
            Utilities.RunCommand("sc config \"RemoteRegistry\" start= disabled");

            // Устанавливаем тип запуска для служб
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DiagTrack", "Start", "4", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service", "Start", "4", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\dmwappushservice", "Start", "4", RegistryValueKind.DWord);
        }

        public bool GetIsChanged()
        {
            bool isChanged = false;
            if (Utilities.ServiceExists("DiagTrack") && Utilities.ServiceExists("diagnosticshub.standardcollector.service") && Utilities.ServiceExists("dmwappushservice"))
            {
                if (!Utilities.IsServiceRunning("DiagTrack") || !Utilities.IsServiceRunning("diagnosticshub.standardcollector.service") || !Utilities.IsServiceRunning("dmwappushservice"))
                {
                    isChanged = true;
                }
            }
            else
            {
                isChanged = true;
            }
            return isChanged;
        }

        public bool ExplorerRebootRequires { get; }
    }

}
