using System.Windows;
using System.Windows.Controls;
using TweakerOS.Controllers;
using TweakerOS.Interfaces;

namespace TweakerOS.View;

public partial class MainWindow : Window
{
    // Список категорий для отображения на экране 
    private List<ICategory> _categories = new();

    public MainWindow()
    {
        InitializeComponent();
        //после запуска добавляем категории в список
        Loaded += OnLoaded;
    }

    /// <summary>
    /// Операции после запуска приложения. Выполняет загрузку категорий твиков и вызов домашней страницы
    /// </summary>
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _categories.Add(new ViewTweaksCategory());
        ShowAllCategoryButtons(_categories);
        HomePageButton_OnClick(sender, e);
    }

    /// <summary>
    /// Отрисовка всех категорий на экран
    /// </summary>
    /// <param name="categories">Список категорий</param>
    private void ShowAllCategoryButtons(List<ICategory> categories)
    {
        CategoriesStackPanel.Children.Clear();
        for (int i = 0; i < categories.Count; i++)
        {
            Button btn = new();
            btn.Style = (Style)FindResource("CategoryButtonStyle");
            Label label = new();
            label.Content = categories[i].Name;
            label.Style = (Style)FindResource("CategoryButtonLabelsStyle");
            btn.Name = $"Category{i}";
            btn.Content = label;
            btn.Click += CategoryButton_OnClick;
            CategoriesStackPanel.Children.Add(btn);
        }
    }

    /// <summary>
    /// Отрисовка всех твиков выбранной категории
    /// </summary>
    /// <param name="category">Выбранная категория</param>
    private void ShowTweaksBySelectedCategory(ICategory category)
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


    // обработка нажатия на кнопку домашней страницы
    /// <summary>
    /// Обработка нажатия на кнопку домашней страницы
    /// </summary>
    private void HomePageButton_OnClick(object sender, RoutedEventArgs e)
    {
        ShowTweaksBySelectedCategory(new HomePageCategory());
    }

    /// <summary>
    /// Обработка нажатия на кнопку категории твиков
    /// </summary>
    private void CategoryButton_OnClick(object sender, RoutedEventArgs e)
    {
        int index = int.Parse(((Button)sender).Name.Split("Category")[1]);
        ShowTweaksBySelectedCategory(_categories[index]);
    }
}