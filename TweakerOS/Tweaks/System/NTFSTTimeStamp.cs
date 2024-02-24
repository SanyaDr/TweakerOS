using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.System;

// TODO проверь правильность написания названий класса
public class NTFSTTimeStamp : ITweak
{
    public string Name => "Отключить временный штампы NTFS";
    public string Description => ""; // TODO Добавь понятное описание этого твика

    public bool GetTweakIsApplied()
    {
        throw new NotImplementedException();
    }

    public bool RebootRequires { get; }

    public void ApplyTweak()
    {
        Utilities.RunCommand("fsutil behavior set disablelastaccess 2");
    }

    public void RestoreToFactory()
    {
        Utilities.RunCommand("fsutil behavior set disablelastaccess 1");
    }
}