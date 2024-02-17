using System.Collections.ObjectModel;
using TweakerOS.Interfaces;
using TweakerOS.Tweaks.System;

namespace TweakerOS.Controllers.TweakCategories;

public class SystemTweakCategory : ICategory
{
    public string Name => "Системные";
    public string Description => "Твики, которые повысят удобство работы с системой";
    public string SystemCodeName => "System";

    public ReadOnlyCollection<ITweak> Tweaks
    {
        get
        {
            Collection<ITweak> tweaks = new();
            tweaks.Add(new DisableCortana());
            tweaks.Add(new DisableSmb());
            tweaks.Add(new DisableWindowsDefender());
            tweaks.Add(new DisableWSearch());
            tweaks.Add(new GamingMode());
            tweaks.Add(new NTFSTTimeStamp());
            tweaks.Add(new SmartScreen());
            tweaks.Add(new UninstallOneDrive());
            tweaks.Add(new WindowsAutoUpdate());
            tweaks.Add(new WindowsReporting());
            tweaks.Add(new WindowsStoreUpdates());
            tweaks.Add(new XboxLive());
            return tweaks.AsReadOnly();
        }
    }
}