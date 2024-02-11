using System.Collections.ObjectModel;
using TweakerOS.Interfaces;
using TweakerOS.Tweaks;

namespace TweakerOS.Controllers;

public class ViewTweaksCategory : ICategory
{
    public string Name => "Вид";
    public string Description => "Настройте вид вашей системы по вашему желанию";

    public ReadOnlyCollection<ITweak> Tweaks
    {
        get
        {
            Collection<ITweak> tweaks = new();
            tweaks.Add(new TestTweakView());
            tweaks.Add(new TestTweakView());
            return tweaks.AsReadOnly();
        }
    }
}