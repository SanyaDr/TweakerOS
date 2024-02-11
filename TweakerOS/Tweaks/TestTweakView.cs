using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks;

public class TestTweakView : ITweak
{

    public string Name => "Визуальный твик !";

    public string Description => "Тестовый твик для проверки 2";
    public bool GetIsChanged()
    {
        throw new NotImplementedException();
    }

    public void Enable()
    {
        throw new NotImplementedException();
    }

    public void Disable()
    {
        throw new NotImplementedException();
    }


    public void Action()
    {
        //...
    }
}