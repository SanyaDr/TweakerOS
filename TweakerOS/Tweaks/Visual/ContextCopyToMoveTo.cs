using TweakerOS.Interfaces;
using Microsoft.Win32;
namespace TweakerOS.Tweaks.Visual;

/// <summary>
/// Устанавливает обработчики контекстного меню "Копировать в" и "Переместить в".
/// </summary>
public class ContextCopyToMoveTo :ITweak
{
    public string Name => "Добавить пункты \"Копировать в...\" в контекстное меню";

    public string Description => "Добавляет в контекстное меню пункты" +
                                 "\"Копровать в..\" и \"Вставить в..\"";
    public bool GetTweakIsApplied()
    {
        return Registry.GetValue(@"HKEY_CLASSES_ROOT\AllFilesystemObjects\shellex\ContextMenuHandlers\Copy To", "", null) is string sa && sa == "{C2FBB630-2971-11D1-A18C-00C04FD75D13}";
    }

    public bool RebootRequires { get; }


    public void ApplyTweak()
    {
        Registry.SetValue("HKEY_CLASSES_ROOT\\AllFilesystemObjects\\shellex\\ContextMenuHandlers\\Copy To", "", "{C2FBB630-2971-11D1-A18C-00C04FD75D13}");
        Registry.SetValue("HKEY_CLASSES_ROOT\\AllFilesystemObjects\\shellex\\ContextMenuHandlers\\Move To", "", "{C2FBB631-2971-11D1-A18C-00C04FD75D13}");
    }

    public void RestoreToFactory()
    {
        Registry.ClassesRoot.DeleteSubKeyTree(@"AllFilesystemObjects\\shellex\\ContextMenuHandlers\\Copy To", false);
        Registry.ClassesRoot.DeleteSubKeyTree(@"AllFilesystemObjects\\shellex\\ContextMenuHandlers\\Move To", false);
    }
}