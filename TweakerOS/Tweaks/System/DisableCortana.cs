using Microsoft.Win32;
using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks.System;

public class DisableCortana : ITweak
{
    public string Name => "Отключить ассистента Cortana";
    public string Description => "Отключение Кортаны";

    public bool GetTweakIsApplied()
    {
        bool isTweakApplied = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\SearchSettings", "IsDeviceSearchHistoryEnabled", -1) is int IsDeviceSearchHistoryEnabled && IsDeviceSearchHistoryEnabled == 0 &&
                              Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\Windows Search", "AllowCortana", -1) is int AllowCortana && AllowCortana == 0 &&
                              Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\Windows Search", "DisableWebSearch", -1) is int DisableWebSearch && DisableWebSearch == 1 &&
                              Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\Windows Search", "ConnectedSearchUseWeb", -1) is int ConnectedSearchUseWeb && ConnectedSearchUseWeb == 0 &&
                              Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\Windows Search", "ConnectedSearchUseWebOverMeteredConnections", -1) is int OverMeteredConnections && OverMeteredConnections == 0 &&
                              Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\Windows Search", "AllowCloudSearch", -1) is int AllowCloudSearch && AllowCloudSearch == 0 &&
                              Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "HistoryViewEnabled", -1) is int HistoryViewEnabled && HistoryViewEnabled == 0 &&
                              Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "DeviceHistoryEnabled", -1) is int DeviceHistoryEnabled && DeviceHistoryEnabled == 0 &&
                              Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "AllowSearchToUseLocation", -1) is int AllowSearchToUseLocation && AllowSearchToUseLocation == 0 &&
                              Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "BingSearchEnabled", -1) is int BingSearchEnabled && BingSearchEnabled == 0 &&
                              Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "CortanaConsent", -1) is int CortanaConsent && CortanaConsent == 0;
        return isTweakApplied;
    }

    public bool RebootRequires => false;

    public void ApplyTweak()
    {
        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\SearchSettings",
            "IsDeviceSearchHistoryEnabled", "0", RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "AllowCortana", "0", RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search",
            "DisableWebSearch", "1", RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search",
            "ConnectedSearchUseWeb", "0", RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search",
            "ConnectedSearchUseWebOverMeteredConnections", "0", RegistryValueKind.DWord);

        Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Search",
            "HistoryViewEnabled", "0", RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Search",
            "DeviceHistoryEnabled", "0", RegistryValueKind.DWord);

        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search",
            "AllowSearchToUseLocation", "0", RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search",
            "BingSearchEnabled", "0", RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "CortanaConsent",
            "0", RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search",
            "AllowCloudSearch", "0", RegistryValueKind.DWord);
    }

    public void RestoreToFactory()
    {
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "AllowCortana",
            "1", RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search",
            "DisableWebSearch", "0", RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search",
            "ConnectedSearchUseWeb", "1", RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search",
            "ConnectedSearchUseWebOverMeteredConnections", "1", RegistryValueKind.DWord);

        Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Search",
            "HistoryViewEnabled", "1", RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Search",
            "DeviceHistoryEnabled", "1", RegistryValueKind.DWord);

        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search",
            "AllowSearchToUseLocation", "1", RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search",
            "BingSearchEnabled", "1", RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "CortanaConsent",
            "1", RegistryValueKind.DWord);
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search",
            "AllowCloudSearch", "1", RegistryValueKind.DWord);
    }
}