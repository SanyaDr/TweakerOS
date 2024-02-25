using Microsoft.Win32;
using TweakerOS.Interfaces;
using TweakerWin.TweakHelper;

namespace TweakerOS.Tweaks.Performance
{
    /// <summary>
    /// Включает автозавершение в диалоговом окне "Выполнить".
    /// </summary>
    internal class EnableAutoCompleteRunDialog : ITweak
    {
        public string Name => "Включить автозаполнение в диалоговом окне \"Выполнить\"";

        public string Description => "Автозаполнение команд при запуске диалогового окна WIN + R";

        public void RestoreToFactory()
        {
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\Explorer\AutoComplete", "Append Completion");
            Utilities.TryDeleteRegistryValue(false, @"Software\Microsoft\Windows\CurrentVersion\Explorer\AutoComplete", "AutoSuggest");
        }

        public void ApplyTweak()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\AutoComplete", "Append Completion", "yes", RegistryValueKind.String);
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\AutoComplete", "AutoSuggest", "yes", RegistryValueKind.String);
        }

        public bool GetTweakIsApplied()
        {
            // Получаем текущие значения из реестра
            string currentAppendCompletion = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\AutoComplete", "Append Completion", null) as string ?? string.Empty;
            string currentAutoSuggest = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\AutoComplete", "AutoSuggest", null) as string ?? string.Empty;

            // Проверяем, отличаются ли текущие значения от ожидаемых
            return (currentAppendCompletion == "yes" || currentAutoSuggest == "yes");
        }

        public bool RebootRequires => false;
    }

}
