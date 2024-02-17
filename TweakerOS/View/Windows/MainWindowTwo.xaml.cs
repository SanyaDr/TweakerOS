using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TweakerOS.Controllers.TweakCategories;
using TweakerOS.Interfaces;
using TweakerOS.Tweaks.System;

namespace TweakerOS.View;

public partial class MainWindowTwo : Window
{
    /// <summary>
    /// Список всех добавленных категорий
    /// </summary>
    private List<ICategory> _categories;

    public MainWindowTwo()
    {
        InitializeComponent();

        Loaded += Init;
    }

    /// <summary>
    /// Выполняется после загрузки окна, идет получение всех категорий 
    /// </summary>
    private void Init(object sender, RoutedEventArgs e)
    {
        _categories = AllCategories.GetCategories();
        AddMenuButtons();
    }

    /// <summary>
    /// Добавление кнопок категорий на экран 
    /// </summary>
    private void AddMenuButtons()
    {
        // int i = 0;
        foreach (var category in _categories)
        {
            RadioButton rb = new();
            rb.Content = category.Name;
            rb.Name = category.SystemCodeName + "RadioButton"; // + i.ToString();
            // PerformanceMenuRb
            // rb.Tag = FindResource(category.SystemCodeName + "MenuRb");
            // rb.Tag = FindResource("PerformanceMenuRb");
            //
            // Image iconImage = new Image();
            //
            //
            // SvgImageSource svgImageSource = new SvgImageSource(new Uri("путь_к_вашему_файлу.svg", UriKind.RelativeOrAbsolute));
            //
            // iconImage.Source = svgImageSource;
            // radioButton.Content = iconImage;
            //
            // DrawingImage img = (DrawingImage)FindResource("PerformanceMenuRb");
            // BitmapImage img = (BitmapImage)FindResource(category.SystemCodeName + "MenuRb");
            // rb.Tag = img;
            rb.Style = (Style)FindResource("MenuRadioButtonStyle");
            rb.Click += CategoryClick;
            MenuButtons.Children.Add(rb);
            // i++;
        }
    }

    private void CategoryClick(object sender, RoutedEventArgs e)
    {
        var button = (RadioButton)sender;
        // int ind = int.Parse(button.Name.Split("RadioButton")[1]);
        string categoryName = button.Name.Split("RadioButton")[0];
        ICategory? selectedCategory = null;
        foreach (var cat in _categories)
        {
            if (cat.SystemCodeName == categoryName)
            {
                selectedCategory = cat;
                break;
            }
        }

        // PagesNavigation.Navigate(new Uri("/View/Pages/"+ _categories[ind].SystemCodeName + "Page.xaml",
        //     UriKind.RelativeOrAbsolute));
        if (selectedCategory != null)
        {
            var pageName = "/View/Pages/" + selectedCategory.SystemCodeName + "Page.xaml";
            PagesNavigation.Navigate(new Uri(pageName,
                UriKind.RelativeOrAbsolute));
        }
    }

    private void CloseButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void ResizeButtonClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
    }

    private void MinimizeButtonClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}