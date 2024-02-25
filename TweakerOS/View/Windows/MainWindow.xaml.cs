using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using TweakerOS.Controllers.TweakCategories;
using TweakerOS.Interfaces;
using TweakerOS.View.Pages;

namespace TweakerOS.View.Windows;

public partial class MainWindow : Window
{
    /// <summary>
    /// Список всех добавленных категорий
    /// </summary>
    private List<ICategory> _categories;

    public MainWindow()
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
        ShowMenuToggleButton.IsChecked = true;
    }

    /// <summary>
    /// Добавление кнопок категорий на экран 
    /// </summary>
    private void AddMenuButtons()
    {
        foreach (var category in _categories)
        {
            RadioButton rb = new();
            rb.Content = category.Name;
            rb.Name = category.SystemCodeName + "RadioButton";

            // WIP иконка категории
            // example: PerformanceMenuRb
            rb.Tag = (PathGeometry)FindResource("PerformanceMenuRb"); // - криво отображается
            
            // rb.Tag = (PathGeometry)FindResource("ViewMenuRb");
            rb.Style = (Style)FindResource("MenuRadioButtonStyle");
            rb.Click += CategoryClick;
            MenuButtons.Children.Add(rb);
        }
    }

    /// <summary>
    /// Обработка нажатия на кнопку категории
    /// </summary>
    private void CategoryClick(object sender, RoutedEventArgs e)
    {
        var button = (RadioButton)sender;
        string categoryName = button.Name.Split("RadioButton")[0]; // Получаем имя категории из названия кнопки
        ICategory? selectedCategory = null; // Объект категории если мы его нашли
        foreach (var cat in _categories) // Поиск категории..
        {
            if (cat.SystemCodeName == categoryName)
            {
                selectedCategory = cat;
                break;
            }
        }

        if (selectedCategory != null)
        {
            // Если категория найдена, то переходим на страницу этой категории
            if (!selectedCategory.SystemCodeName.StartsWith("Info"))
            {
                PagesNavigation.Navigate(new TweaksPage(selectedCategory));
            }
            else
            {
                string pageName = "/View/Pages/" + selectedCategory.SystemCodeName.Split("Info")[1] + "Page.xaml";
                PagesNavigation.Navigate(new Uri(pageName, UriKind.RelativeOrAbsolute));
            }
        }
    }

    /// <summary>
    /// Закрытие приложения
    /// </summary>
    private void CloseButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    /// <summary>
    /// Открытие окна приложения во весь экран
    /// </summary>
    private void ResizeButtonClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
    }

    /// <summary>
    /// Сворачивание окна приложения
    /// </summary>
    private void MinimizeButtonClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void HyperlinkInBrowser_Navigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
        e.Handled = true;
    }

    private void HyperlinkLocal_Navigate(object sender, RequestNavigateEventArgs e)
    {
        AboutWindow about = new();
        about.Show();
        e.Handled = true;
    }

    private void HomePageCategoryClick(object sender, RoutedEventArgs e)
    {
        PagesNavigation.Navigate(new HomePage());
    }
}