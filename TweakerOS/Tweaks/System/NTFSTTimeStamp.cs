using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.System;

// TODO проверь правильность написания названий класса
public class NTFSTTimeStamp : ITweak
{
    public string Name => "Отключить временный штампы NTFS";
    public string Description => ""; // TODO Добавь понятное описание этого твика

    public bool GetIsChanged()
    {
        throw new NotImplementedException();
    }

    public bool ExplorerRebootRequires { get; }

    public void Enable()
    {
        Utilities.RunCommand("fsutil behavior set disablelastaccess 2");
    }

    public void Disable()
    {
        Utilities.RunCommand("fsutil behavior set disablelastaccess 1");
    }
}