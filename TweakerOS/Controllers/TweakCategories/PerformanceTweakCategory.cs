using System.Collections.ObjectModel;
using System.Windows.Automation;
using TweakerOS.Interfaces;
using TweakerOS.Tweaks;
using TweakerOS.Tweaks.Performance;
using TweakerOS.Tweaks.SystemServices;

namespace TweakerOS.Controllers.TweakCategories;

public class PerformanceTweakCategory:ICategory
{
    public string Name => "Производительность";
    public string Description => "Повысьте производительность вашей системы!";
    public string SystemCodeName => "Performance";

    public ReadOnlyCollection<ITweak> Tweaks
    {
        get
        {
            Collection<ITweak> tweaks = new();
            
            tweaks.Add(new CompatibilityAssistant());
            tweaks.Add( new DesktopAndExplorerSettings());
            tweaks.Add( new  DisableRemoteAssistance());
            tweaks.Add( new  DisableShakingToMinimize());
            tweaks.Add( new  EnableAutoCompleteRunDialog());
            tweaks.Add( new  HomeGroup());
            tweaks.Add( new  MediaPlayerSharing());
            tweaks.Add( new  MouseShowDelayZero());
            tweaks.Add( new  MultimediaSystemProfileSettings());
            tweaks.Add( new  NetworkThrottling());
            tweaks.Add( new  NoDelayMenuShowingUp());
            tweaks.Add( new  PrintService());
            tweaks.Add( new  ReduceDumpFileSize());
            tweaks.Add( new  ServiceTweaksStoped());
            tweaks.Add( new  Superfetch());
            tweaks.Add( new  SystemRestore());
            tweaks.Add(new AllTelemetryOS());
            tweaks.Add(new ChromeTelemetry());
            tweaks.Add(new EdgeTelemetry());
            tweaks.Add(new FirefoxTelemetry());
            tweaks.Add(new GameBar());
            tweaks.Add(new NvidiaTelemetry());
            tweaks.Add(new VisualStudioTelemetry());
            return tweaks.AsReadOnly();
        }
    }}