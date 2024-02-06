namespace TweakerOS.Interfaces;

public interface ITweak
{
    public string Name { get; }
    public string Description { get; }
    public void Action();
}