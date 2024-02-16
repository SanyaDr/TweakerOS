using System.Windows;
using System.Windows.Controls;
using TweakerOS.Controllers.TweakCategories;
using TweakerOS.Interfaces;

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
        int i = 1;
        foreach (var category in _categories)
        {
            RadioButton rb = new();
            rb.Content = category.Name;
            rb.Name = category.SystemCodeName + "RadioButton" + i.ToString();
            // rb.Tag = FindResource(category.SystemCodeName);
            rb.Style = (Style)FindResource("MenuRadioButtonStyle");
            rb.Click += CategoryClick;
            MenuButtons.Children.Add(rb);
            i++;
        }
    }

    private void CategoryClick(object sender, RoutedEventArgs e)
    {
        var button = (RadioButton)sender;

        //...
    }
}