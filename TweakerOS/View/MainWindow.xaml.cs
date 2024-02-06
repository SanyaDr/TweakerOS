using System.Windows;
using System.Windows.Controls;
using TweakerOS.Controllers;
using TweakerOS.Interfaces;
using TweakerOS.Model;

namespace TweakerOS.View;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        ReadCategoties();
    }

    private void ReadCategoties()
    {
        List<ICategory> categories = new();
        categories.Add(new HomePageCategory());
    }

    private void ShowTweaks(ICategory category)
    {
        TweaksStackPanel.Children.Clear();
        foreach (var tweak in category.Tweaks)
        {
            Border main = new Border();
            main.Style = (Style)FindResource("TweakBorderStyle");

            CheckBox chkbox = new();
            chkbox.Style = (Style)FindResource("TweakCheckBoxStyle");

            Button butt = new();
            butt.Style = (Style)FindResource("TweakInfoButtonStyle");

            Label label = new();
            label.Style = (Style)FindResource("TweakNameLabelStyle");
            label.Content = tweak.Name;
            
            DockPanel dpanel = new();
            dpanel.Style = (Style)FindResource("TweakDockPanelStyle");
            DockPanel.SetDock(chkbox, Dock.Left);
            DockPanel.SetDock(butt, Dock.Right);
            DockPanel.SetDock(label, Dock.Left);
            dpanel.Children.Add(chkbox);
            dpanel.Children.Add(butt);
            dpanel.Children.Add(label);
            main.Child = dpanel;
            TweaksStackPanel.Children.Add(main);
        }
    }

    private void HomePageButton_OnClick(object sender, RoutedEventArgs e)
    {
        // TweaksStackPanel
        ShowTweaks(new HomePageCategory());
    }
}