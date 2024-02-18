using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TweakerOS.Interfaces;

namespace TweakerOS.View.Pages;

public partial class TweaksPage : Page
{
    private List<ITweak> _tweaks;
    public TweaksPage(ICategory selectedCategory)
    {
        InitializeComponent();
        _tweaks = selectedCategory.Tweaks.ToList();
        AddTweakButton();
    }

    private void AddTweakButton()
    {
        TweaksStackPanel.Children.Clear();
        foreach (var tweak in _tweaks)
        {
            Border main = new();
            main.Style = (Style)FindResource("TweakBorderStyle");
            
            Grid grid = new();
            
            grid.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(0.1, GridUnitType.Star)});
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(0.08, GridUnitType.Star)});
            CheckBox cbox = new();
            cbox.Style = (Style)FindResource("CheckBoxTweakStyle");
            Grid.SetColumn(cbox, 0);
            
            Label label = new();
            label.Style = (Style)FindResource("NameTweakLabelStyle");
            label.Content = tweak.Name;
            Grid.SetColumn(label, 1);

            Button button = new();
            button.Style = (Style)FindResource("InfoButtonStyle");
            Grid.SetColumn(button, 2);

            Image infoLogo = new();
            infoLogo.Source = (ImageSource)FindResource("InformationIcon");
            button.Content = infoLogo;

            grid.Children.Add(cbox);
            grid.Children.Add(label);
            grid.Children.Add(button);

            main.Child = grid;
            
            TweaksStackPanel.Children.Add(main);
        }
    }
}