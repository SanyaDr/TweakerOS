using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks.Performance
{
    /// <summary>
    /// Включает автозавершение в диалоговом окне "Выполнить".
    /// </summary>
    internal class EnableAutoCompleteRunDialog : ITweak
    {
        public string Name => "Включить автозаполнение в диалоговом окне \"Выполнить\"";

        public string Description => "Автозаполнение команд при запуске диалогового окна WIN + R";

        public void Disable()
        {
            Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\AutoComplete", true).DeleteValue("Append Completion", false);
            Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\AutoComplete", true).DeleteValue("AutoSuggest", false);
        }

        public void Enable()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\AutoComplete", "Append Completion", "yes", RegistryValueKind.String);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\AutoComplete", "AutoSuggest", "yes", RegistryValueKind.String);
        }

        public bool GetIsChanged()
        {
            // Получаем текущие значения из реестра
            string currentAppendCompletion = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\AutoComplete", "Append Completion", null) as string;
            string currentAutoSuggest = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\AutoComplete", "AutoSuggest", null) as string;

            // Проверяем, отличаются ли текущие значения от ожидаемых
            return (currentAppendCompletion != "yes" || currentAutoSuggest != "yes");
        }
    }
    
}
