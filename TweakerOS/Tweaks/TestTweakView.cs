using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks;

public class TestTweakView : ITweak
{

    public string Name => "Визуальный твик !";

    public string Description => "Тестовый твик для проверки 2";
    

    public void Action()
    {
        //...
    }
}