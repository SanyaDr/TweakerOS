namespace TweakerOS.Interfaces;

public interface ITweak
{
    public string Name { get; }
    public string Description { get; }
    public bool GetIsChanged();     // получить применен ли твик
    public bool ExplorerRebootRequires { get; }     // требуется ли перезагрузка после применения твика
    public void Enable();           // применить твик
    public void Disable();          // отменить твик
}