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
    public bool GetIsChanged()
    {
        bool v1 = (bool)Registry.ClassesRoot.GetValue(@"AllFilesystemObjects\\shellex\\ContextMenuHandlers\\Copy To", false);
        bool v2 = (bool)Registry.ClassesRoot.GetValue(@"AllFilesystemObjects\\shellex\\ContextMenuHandlers\\Move To", false);
        return v1 == v2 && v2 == false;
    }

    public bool ExplorerRebootRequires { get; }


    public void Enable()
    {
        Registry.SetValue("HKEY_CLASSES_ROOT\\AllFilesystemObjects\\shellex\\ContextMenuHandlers\\Copy To", "", "{C2FBB630-2971-11D1-A18C-00C04FD75D13}");
        Registry.SetValue("HKEY_CLASSES_ROOT\\AllFilesystemObjects\\shellex\\ContextMenuHandlers\\Move To", "", "{C2FBB631-2971-11D1-A18C-00C04FD75D13}");
    }

    public void Disable()
    {
        Registry.ClassesRoot.DeleteSubKeyTree(@"AllFilesystemObjects\\shellex\\ContextMenuHandlers\\Copy To", false);
        Registry.ClassesRoot.DeleteSubKeyTree(@"AllFilesystemObjects\\shellex\\ContextMenuHandlers\\Move To", false);
    }
}