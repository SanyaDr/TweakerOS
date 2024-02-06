using TweakerOS.Interfaces;
using TweakerOS.Model;

namespace TweakerOS.Tweaks;

public class TestTweak : ITweak
{

    public string Name => "Тест твик 1!";

    public string Description => "Тестовый твик для проверки 1";

    public void Action()
    {
        //...
    }
}