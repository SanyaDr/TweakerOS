using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks;

public class TestTweak : ITweak
{

    public string Name => "Тест твик 1!";

    public string Description => "Тестовый твик для проверки 1";
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
    
}