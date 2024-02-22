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

            try
            {
                CheckBox cbox = new();
                cbox.Tag = tweak;
                cbox.Style = (Style)FindResource("CheckBoxTweakStyle");
                cbox.IsChecked = tweak.GetTweakIsApplied();
                cbox.Click += TweakWasToggled;
                grid.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(0.1, GridUnitType.Star)});
                Grid.SetColumn(cbox, 0);
            }
            catch (NotImplementedException e)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(0.2, GridUnitType.Star)});
                StackPanel panel = new();
                Grid.SetColumn(panel, 0);
                panel.Orientation = Orientation.Horizontal;
                
                Button bOn = new();
                bOn.Style = (Style)FindResource("TweakDoubleButtonsStyle");
                bOn.Content = "Вкл";
                bOn.Margin = new Thickness(8, 5, 4, 5);

                Button bOff = new();
                bOff.Style = (Style)FindResource("TweakDoubleButtonsStyle");
                bOff.Content = "Выкл";
                bOff.Margin = new Thickness(4, 5, 8, 5);

                panel.Children.Add(bOn);
                panel.Children.Add(bOff);

                grid.Children.Add(panel);
            }
            finally
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(0.08, GridUnitType.Star)});
            }
            
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

            // grid.Children.Add(cbox);
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
       
        if (tweak.GetTweakIsApplied())
        {
            tweak.RestoreToFactory();
        }
        else
        {
            tweak.ApplyTweak();
        }
        
        if (tweak.RebootRequires)
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