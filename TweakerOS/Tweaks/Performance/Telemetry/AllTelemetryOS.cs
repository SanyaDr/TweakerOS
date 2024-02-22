using System.Net.Http.Headers;
using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.Performance.Telemetry
{
    class AllTelemetryOs : ITweak
    {
        public string Name => "Отключить не нужную телеметрию windows";

        public string Description => "Отключает телеметрию разных служб Find my device, Wi-Fi, Windows Hello и тд.";

        public void RestoreToFactory()
        {
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "RotatingLockScreenOverlayEnabled");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "RotatingLockScreenEnabled");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "DisableWindowsSpotlightFeatures");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "DisableTailoredExperiencesWithDiagnosticData");

            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\CloudContent", "DisableCloudOptimizedContent");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\PolicyManager\default\NewsAndInterests\AllowNewsAndInterests", "value");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth", "AllowAdvertising");

            // Account -> Sign-in options -> Privacy
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", "DisableAutomaticRestartSignOn");

            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\AdvertisingInfo", "DisabledByGroupPolicy");
            //Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "Start_TrackProgs");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\TabletPC", "PreventHandwritingDataSharing");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\TextInput", "AllowLinguisticDataCollection");

            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\InputPersonalization", "AllowInputPersonalization");

            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\System", "UploadUserActivities");

            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\System", "AllowCrossDeviceClipboard");
            Utilities.TryDeleteRegistryValue(true, @"Software\Policies\Microsoft\Windows\Messaging", "AllowMessageSync");
            Utilities.TryDeleteRegistryValue(true, @"Software\Policies\Microsoft\Windows\SettingSync", "DisableCredentialsSettingSync");
            Utilities.TryDeleteRegistryValue(true, @"Software\Policies\Microsoft\Windows\SettingSync", "DisableCredentialsSettingSyncUserOverride");
            Utilities.TryDeleteRegistryValue(true, @"Software\Policies\Microsoft\Windows\SettingSync", "DisableApplicationSettingSync");
            Utilities.TryDeleteRegistryValue(true, @"Software\Policies\Microsoft\Windows\SettingSync", "DisableApplicationSettingSyncUserOverride");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\AppPrivacy", "LetAppsActivateWithVoice");
            Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Microsoft\Windows\CurrentVersion\SearchSettings", "SafeSearchMode");

            // Find my device
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\FindMyDevice", "AllowFindMyDevice");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\Settings\FindMyDevice", "LocationSyncEnabled");

            // Timeline
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\System", "EnableActivityFeed");

            // Shared Experiences
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\System", "EnableCdp");

            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\Privacy", "TailoredExperiencesWithDiagnosticDataEnabled");
            Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Diagnostics\DiagTrack", "ShowedToastAtLevel");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Speech_OneCore\Settings\OnlineSpeechPrivacy", "HasAccepted");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\location", "Value");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\Settings\FindMyDevice", "LocationSyncEnabled");

            // Enable location tracking
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableLocation");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableLocationScripting");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableWindowsLocationProvider");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Sensor\Overrides\{BFA794E4-F964-4FDB-90F6-51056BFE4B44}", "SensorPermissionState");
            Utilities.TryDeleteRegistryValue(true, @"System\CurrentControlSet\Services\lfsvc\Service\Configuration", "Status");

            // Enable biometrics (Windows Hello)
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Biometrics", "Enabled");

            // Turn off KMS Client Online AVS Validation
            Utilities.TryDeleteRegistryValue(true, @"Software\Policies\Microsoft\Windows NT\CurrentVersion\Software Protection Platform", "NoGenTicket");

            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\Feeds", "ShellFeedsTaskbarOpenOnHover");

            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\CDP", "CdpSessionUserAuthzPolicy");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\CDP", "NearShareChannelUserAuthzPolicy");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\CDP", "RomeSdkChannelUserAuthzPolicy");

            // General
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled");
            Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost", "EnableWebContentEvaluation");
            Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost\EnableWebContentEvaluation", "Enabled");
            Utilities.TryDeleteRegistryValue(false, @"Control Panel\International\User Profile", "HttpAcceptLanguageOptOut");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\Windows\CurrentVersion\SmartGlass", "UserAuthPolicy");

            // Speech, inking & typing
            Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Microsoft\Personalization\Settings", "AcceptedPrivacyPolicy");
            Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language", "Enabled");
            Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Microsoft\InputPersonalization", "RestrictImplicitTextCollection");
            Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Microsoft\InputPersonalization", "RestrictImplicitInkCollection");
            Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Microsoft\InputPersonalization\TrainedDataStore", "HarvestContacts");
            Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Microsoft\Input\TIPC", "Enabled");

            // Other devices
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\AppPrivacy", "LetAppsSyncWithDevices");
            Utilities.TryDeleteRegistryValue(false, @"SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\LooselyCoupled", "Value");

            // Feedback & diagnostics
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Siuf\Rules", "PeriodInNanoSeconds");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Siuf\Rules", "NumberOfSIUFInPeriod");

            // Feedback & diagnostics
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection", "MaxTelemetryAllowed");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\System", "UploadUserActivities");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Siuf\Rules", "PeriodInNanoSeconds");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Siuf\Rules", "NumberOfSIUFInPeriod");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection", "AllowTelemetry");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\DataCollection", "AllowTelemetry");
            Utilities.TryDeleteRegistryValue(true, @"SYSTEM\ControlSet001\Control\WMI\AutoLogger\AutoLogger-Diagtrack-Listener", "Start");
            Utilities.TryDeleteRegistryValue(true, @"SYSTEM\CurrentControlSet\Control\WMI\AutoLogger\AutoLogger-Diagtrack-Listener", "Start");

            // Wi-Fi Sense
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\WcmSvc\wifinetworkmanager\config", "AutoConnectAllowedOEM");

            // Hotspot 2.0
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\WcmSvc\Tethering", "Hotspot2SignUp");
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\WlanSvc\AnqpCache", "OsuRegistrationStatus");

            // Mobile Hotspot remote startup
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Microsoft\WcmSvc\Tethering", "RemoteStartupDisabled");

            // Projecting to this PC
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows\Connect", "AllowProjectionToPC");

            // Phone Link
            Utilities.TryDeleteRegistryValue(true, @"SOFTWARE\Policies\Microsoft\Windows", "EnableMmx");
            Utilities.TryDeleteRegistryValue(true, @"Software\Policies\Microsoft\Windows\System", "EnableMmx");
            Utilities.TryDeleteRegistryValue(true, @"Software\Policies\Microsoft\Windows\System", "RSoPLogging");
        }

        public void ApplyTweak()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "RotatingLockScreenOverlayEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "RotatingLockScreenEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "DisableWindowsSpotlightFeatures", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", "DisableTailoredExperiencesWithDiagnosticData", "1", RegistryValueKind.DWord);

            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\CloudContent", "DisableCloudOptimizedContent", 1);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", 0);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\current\device\Bluetooth", "AllowAdvertising", 0);

            // Account -> Sign-in options -> Privacy
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", "DisableAutomaticRestartSignOn", 1);

            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\AdvertisingInfo", "DisabledByGroupPolicy", 1);
            //Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "Start_TrackProgs", 0);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\TabletPC", "PreventHandwritingDataSharing", 1);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\TextInput", "AllowLinguisticDataCollection", 0);

            // Privacy -> Speech
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\InputPersonalization", "AllowInputPersonalization", 0);

            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\SearchSettings", "SafeSearchMode", 0, RegistryValueKind.DWord);

            // Privacy -> Activity history -> Send my activity history to Microsoft
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "UploadUserActivities", 0);

            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "AllowCrossDeviceClipboard", 0);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Messaging", "AllowMessageSync", 0);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\SettingSync", "DisableCredentialsSettingSync", 2);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\SettingSync", "DisableCredentialsSettingSyncUserOverride", 1);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\SettingSync", "DisableApplicationSettingSync", 2);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\SettingSync", "DisableApplicationSettingSyncUserOverride", 1);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\AppPrivacy", "LetAppsActivateWithVoice", 2);

            // Find my device
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\FindMyDevice", "AllowFindMyDevice", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Settings\FindMyDevice", "LocationSyncEnabled", "0", RegistryValueKind.DWord);

            // Timeline
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "EnableActivityFeed", "0", RegistryValueKind.DWord);

            // Shared Experiences
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "EnableCdp", "0", RegistryValueKind.DWord);

            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Privacy", "TailoredExperiencesWithDiagnosticDataEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\Privacy", "TailoredExperiencesWithDiagnosticDataEnabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Diagnostics\DiagTrack", "ShowedToastAtLevel", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_USERS\.DEFAULT\SOFTWARE\Microsoft\Windows\CurrentVersion\Diagnostics\DiagTrack", "ShowedToastAtLevel", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Speech_OneCore\Settings\OnlineSpeechPrivacy", "HasAccepted", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\location", "Value", "Deny", RegistryValueKind.String);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Settings\FindMyDevice", "LocationSyncEnabled", "0", RegistryValueKind.DWord);

            // Disable location tracking
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableLocation", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableLocationScripting", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableWindowsLocationProvider", 1, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Sensor\Overrides\{BFA794E4-F964-4FDB-90F6-51056BFE4B44}", "SensorPermissionState", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\lfsvc\Service\Configuration", "Status", "0", RegistryValueKind.DWord);

            // Disable biometrics (Windows Hello)
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Biometrics", "Enabled", "0", RegistryValueKind.DWord);

            // News feeding
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Feeds", "ShellFeedsTaskbarOpenOnHover", "0", RegistryValueKind.DWord);

            // Turn off share apps across devices
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\CDP", "CdpSessionUserAuthzPolicy", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\CDP", "NearShareChannelUserAuthzPolicy", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\CDP", "RomeSdkChannelUserAuthzPolicy", "0", RegistryValueKind.DWord);

            // Turn off KMS Client Online AVS Validation
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows NT\CurrentVersion\Software Protection Platform", "NoGenTicket", "1", RegistryValueKind.DWord);

            // General
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost", "EnableWebContentEvaluation", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost\EnableWebContentEvaluation", "Enabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\International\User Profile", "HttpAcceptLanguageOptOut", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SmartGlass", "UserAuthPolicy", "0", RegistryValueKind.DWord);

            // Speech, inking & typing
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Personalization\Settings", "AcceptedPrivacyPolicy", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language", "Enabled", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\InputPersonalization", "RestrictImplicitTextCollection", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\InputPersonalization", "RestrictImplicitInkCollection", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\InputPersonalization\TrainedDataStore", "HarvestContacts", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Input\TIPC", "Enabled", "0", RegistryValueKind.DWord);

            // Other devices
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\AppPrivacy", "LetAppsSyncWithDevices", "2", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\LooselyCoupled", "Value", "Deny", RegistryValueKind.String);

            // Feedback & diagnostics
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection", "MaxTelemetryAllowed", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "UploadUserActivities", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Siuf\Rules", "PeriodInNanoSeconds", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Siuf\Rules", "NumberOfSIUFInPeriod", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\DataCollection", "AllowTelemetry", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\DataCollection", "AllowTelemetry", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\WMI\AutoLogger\AutoLogger-Diagtrack-Listener", "Start", "0", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\WMI\AutoLogger\AutoLogger-Diagtrack-Listener", "Start", "0", RegistryValueKind.DWord);

            // Wi-Fi Sense
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WcmSvc\wifinetworkmanager\config", "AutoConnectAllowedOEM", "0", RegistryValueKind.DWord);

            // Hotspot 2.0
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WcmSvc\Tethering", "Hotspot2SignUp", "1", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WlanSvc\AnqpCache", "OsuRegistrationStatus", "0", RegistryValueKind.DWord);

            // Mobile Hotspot remote startup
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WcmSvc\Tethering", "RemoteStartupDisabled", "1", RegistryValueKind.DWord);

            // Projecting to this PC
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Connect", "AllowProjectionToPC", "0", RegistryValueKind.DWord);

            // Phone Link
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "EnableMmx", 0, RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "RSoPLogging", 0, RegistryValueKind.DWord);
        }
        
        public bool GetTweakIsApplied()
        {
            throw new NotImplementedException();
        }

        public bool RebootRequires => true;
    }
}
