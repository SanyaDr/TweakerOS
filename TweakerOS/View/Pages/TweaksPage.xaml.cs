using System.Diagnostics.Tracing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using TweakerOS.Controllers;
using TweakerOS.Interfaces;
using TweakerOS.View.Windows;

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
        int i = 0;
        foreach (var tweak in _tweaks)
        {
            Border main = new();
            main.Style = (Style)FindResource("TweakBorderStyle");
            
            Grid grid = new();
            
            grid.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(0.1, GridUnitType.Star)});
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(0.08, GridUnitType.Star)});
            CheckBox cbox = new();
            cbox.Tag = tweak;
            cbox.Style = (Style)FindResource("CheckBoxTweakStyle");
            cbox.IsChecked = tweak.GetIsChanged();
            Grid.SetColumn(cbox, 0);
            cbox.Click += TweakWasToggled;
            
            Label label = new();
            label.Style = (Style)FindResource("NameTweakLabelStyle");
            label.Content = tweak.Name;
            Grid.SetColumn(label, 1);

            Button button = new();
            button.Name = "AboutTweakButton" + i.ToString();
            button.Style = (Style)FindResource("InfoButtonStyle");
            button.Click += AboutTweakButtonClick;
            Grid.SetColumn(button, 2);

            Image infoLogo = new();
            infoLogo.Source = (ImageSource)FindResource("InformationIcon");
            button.Content = infoLogo;

            grid.Children.Add(cbox);
            grid.Children.Add(label);
            grid.Children.Add(button);

            main.Child = grid;
            
            TweaksStackPanel.Children.Add(main);
            i++;
        }
    }

    private void TweakWasToggled(object sender, RoutedEventArgs e)
    {
        CheckBox checkBox = (CheckBox)sender;
        ITweak tweak = (ITweak)checkBox.Tag;
       
        if (tweak.GetIsChanged())
        {
            tweak.Disable();
        }
        else
        {
            tweak.Enable();
        }
        
        if (tweak.ExplorerRebootRequires)
        {
            MessageBoxCustomWindow ms = new();
            if (ms.ShowRebootExplorerRequired() == true)
            {
                Rebooter.RebootExplorer();
            }
        }
    }

    private void AboutTweakButtonClick(object sender, RoutedEventArgs e)
    {
        var butSender = (Button)sender;
        int ind = int.Parse(butSender.Name.Split("AboutTweakButton")[1]);
        ITweak tweakSender = _tweaks[ind];
        MessageBoxCustomWindow ms = new(tweakSender.Description, "О твике: " + tweakSender.Name);
        ms.ShowDialog();
    }
}