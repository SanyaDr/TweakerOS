using Microsoft.Win32;
using TweakerOS.Interfaces;

namespace TweakerOS.Tweaks.Visual;

/// <summary>
/// Изменить цвет выделения Windows
/// </summary>
public class ChangeHighlightColor : ITweak
{
    public string Name => "Изменить цвет выделения";
    public string Description => "Изменяет цвет выделения Windows на ваш";

    public bool GetIsChanged()
    {
        throw new NotImplementedException();
    }

    public void Enable()
    {
        int red = 222;
        int green = 222;
        int blue = 55;

        string rgbValue = $"{red} {green} {blue}";

        Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", rgbValue, RegistryValueKind.String);
        Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", rgbValue,
            RegistryValueKind.String);
    }

    public void Disable()
    {
        int red = 0;
        int green = 0;
        int blue = 255;

        string rgbValue = $"{red} {green} {blue}";

        Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", rgbValue, RegistryValueKind.String);
        Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", rgbValue,
            RegistryValueKind.String);
        Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", rgbValue,
            RegistryValueKind.String);
        Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", rgbValue, RegistryValueKind.String);
    }
}