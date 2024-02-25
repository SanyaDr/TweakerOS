using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.System;


public class NTFSTTimeStamp : ITweak
{
    public string Name => "Отключить временный штампы NTFS";
    public string Description => "NTFS является стандартной файловой системой для операционных систем Windows. " +
        "Современная версия NTFS поддерживает сжатие, используемый для ограничения доступа к файлам механизм разрешений, " +
        "альтернативные потоки, а также присваивание временных меток — метаданных, доступных в свойствах объекта. " +
        "Эти метки позволяют определять последнее время создания, модификации и доступа к файлам и каталогам.\r\n\r\n" +
        "В старых версиях Windows можно было отключать функцию присвоения временных меток с целью повышения общей " +
        "производительности и скорости доступа к файлам и каталогам. Отключение этой функции на современных компьютерах " +
        "с SSD-дисками вряд ли позволит существенно ускорить доступ, тем не менее, Microsoft не только не удалила эту опцию, " +
        "но и расширила ее."; // TODO Добавь понятное описание этого твика

    public bool GetTweakIsApplied()
    {
        return Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FileSystem", "NtfsDisableLastAccessUpdate", -1) is int NtfsDisableLastAccessUpdate && NtfsDisableLastAccessUpdate == 1;
    }

    public bool RebootRequires { get; }

    public void ApplyTweak()
    {
        Utilities.RunCommand("fsutil behavior set disablelastaccess 1");
    }

    public void RestoreToFactory()
    {
        Utilities.RunCommand("fsutil behavior set disablelastaccess 2");
    }
}