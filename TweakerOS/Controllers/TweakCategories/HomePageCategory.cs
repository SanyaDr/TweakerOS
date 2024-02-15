using System.Collections.ObjectModel;
using TweakerOS.Interfaces;
using TweakerOS.Tweaks;

namespace TweakerOS.Controllers.TweakCategories;

public class HomePageCategory: ICategory
{
    public string Name => "Главнаяя";
    public string Description => "На этой странице вы можете увидеть информацию о приложении";

    public ReadOnlyCollection<ITweak> Tweaks
    {
        get
        {
            Collection<ITweak> tweaks = new();

            return tweaks.AsReadOnly();
        }
    }
}