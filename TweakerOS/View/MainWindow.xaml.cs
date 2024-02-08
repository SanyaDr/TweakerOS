using System.Windows;
using System.Windows.Controls;
using TweakerOS.Controllers;
using TweakerOS.Interfaces;
using TweakerOS.Model;

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

    // добавление категорий
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
// TODO а нужен ли homepage в списке категорий?
        _categories.Add(new HomePageCategory());
        _categories.Add(new ViewTweaksCategory());
        LoadButtons(_categories);
    }

    // отрисовка кнопок категорий на экран
    // TODO переименовать методы в более понятные
    private void LoadButtons(List<ICategory> categories)
    {
        CategoriesStackPanel.Children.Clear();
        for (int i = 1; i < categories.Count; i++)
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

    // отрисовка твиков выбранной категории 
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

    
    // обработка нажатия на кнопку домашней страницы
    // TODO объединить с методом CategoryButton_OnClick
    private void HomePageButton_OnClick(object sender, RoutedEventArgs e)
    {
        // TweaksStackPanel
        ShowTweaks(new HomePageCategory());
    }

    // обработка нажатия выбора категории
    private void CategoryButton_OnClick(object sender, RoutedEventArgs e)
    {
        // MessageBox.Show("Нажата кнопка категории!", ((Button)sender).Content.ToString());
        int ind = int.Parse(((Button)sender).Name.Split("Category")[1]);
        ShowTweaks(_categories[ind]);
    }
    
}