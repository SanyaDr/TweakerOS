using System.Windows;
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
    private void HomePageButton_OnClick(object sender, RoutedEventArgs e)
    {
        
    }
}