using System.Collections.ObjectModel;
using TweakerOS.Interfaces;
using TweakerOS.Tweaks;
using TweakerOS.Tweaks.Visual;

namespace TweakerOS.Controllers.TweakCategories;

public class ViewTweakCategory : ICategory
{
    public string Name => "Вид";
    public string Description => "Настройте вид вашей системы по вашему желанию";
    public string SystemCodeName => "View";

    public ReadOnlyCollection<ITweak> Tweaks
    {
        get
        {
            Collection<ITweak> tweaks = new();
            tweaks.Add(new ContextCopyToMoveTo());
            tweaks.Add(new ClassicPhotoViewer());
            tweaks.Add(new ChangeHighlightColor());
            tweaks.Add(new DeleteFoldersFromComputer());
            tweaks.Add(new DisablingSuffixArrowShortcut());
            tweaks.Add(new TransparencyStartMenu());
            return tweaks.AsReadOnly();
        }
    }
}