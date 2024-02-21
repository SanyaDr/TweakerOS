using Microsoft.Win32;
using TweakerOS.Interfaces;
namespace TweakerOS.Tweaks.Visual;
struct Themes
{
    public string HilightRgb;
    public string HotTrackingColorRgb;
    public string ActiveBorderRgb;
    public string HilightTextRgb;
}

/// <summary>
/// Изменить цвет выделения Windows
/// </summary>
public class ChangeHighlightColor : ITweak
{
    public string Name => "Изменить цвет выделения";
    public string Description => "Изменяет цвет выделения Windows на ваш";
    
    /// <summary>
    /// Стандартная тема Windows (голубая)
    /// </summary>
    private Themes StandartWindowsTheme = new Themes
    {
        HilightRgb = "0 120 215",
        HotTrackingColorRgb = "0 102 204",
        ActiveBorderRgb = "180 180 180",
        HilightTextRgb = "255 255 255"
    };
    /// <summary>
    /// Серый цвет выделения
    /// </summary>
    private Themes GrayWindowsTheme = new Themes
    {
        HilightRgb = "80 80 80",
        HotTrackingColorRgb = "60 60 60",
        ActiveBorderRgb = "60 60 60",
        HilightTextRgb = "80 80 80"
    };

    /// <summary>
    /// Отличается ли этот параметр от стандартного?
    /// </summary>
    /// <returns>false - стандартная тема windows, true - цвет выделения изменен </returns>
    public bool GetIsChanged()
    {
        string[] values = new string [4];
        values[0] = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", StandartWindowsTheme.HilightRgb);
        values[1] = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", StandartWindowsTheme.HotTrackingColorRgb);
        values[2] = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", StandartWindowsTheme.ActiveBorderRgb);
        values[3] = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", StandartWindowsTheme.HilightTextRgb);

        // bool b1 = values[0] == StandartWindowsTheme.HilightRgb;
        // bool b2 = values[1] == StandartWindowsTheme.HotTrackingColorRgb;
        // bool b3 = values[2] == StandartWindowsTheme.ActiveBorderRgb;
        // bool b4 = values[3] == StandartWindowsTheme.HilightTextRgb;
        
        return !(values[0] == StandartWindowsTheme.HilightRgb &&
                 values[1] == StandartWindowsTheme.HotTrackingColorRgb &&
                 values[2] == StandartWindowsTheme.ActiveBorderRgb && 
                 values[3] == StandartWindowsTheme.HilightTextRgb);
    }

    public bool ExplorerRebootRequires => true;
    
    public void Enable()
    {

        Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", GrayWindowsTheme.HilightRgb, RegistryValueKind.String);
        Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", GrayWindowsTheme.HotTrackingColorRgb,
            RegistryValueKind.String);
        Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", GrayWindowsTheme.ActiveBorderRgb,
            RegistryValueKind.String);
        Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", GrayWindowsTheme.HilightTextRgb, RegistryValueKind.String);
    }

    public void Disable()
    {
        Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", StandartWindowsTheme.HilightRgb, RegistryValueKind.String);
        Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", StandartWindowsTheme.HotTrackingColorRgb,
            RegistryValueKind.String);
        Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", StandartWindowsTheme.ActiveBorderRgb,
            RegistryValueKind.String);
        Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", StandartWindowsTheme.HilightTextRgb, RegistryValueKind.String);
    }
}