using TweakerOS.Interfaces;

namespace TweakerOS.Controllers.TweakCategories;

public static class AllCategories
{
    public static List<ICategory> GetCategories()
    {
        List<ICategory> categories =
        [
            new HomePageCategory(),
            new ViewTweakCategory(),
            new PerformanceTweakCategory(),
            new SystemTweakCategory()
        ];

        return categories;
    }
}