using System.Windows.Controls;
using TweakerOS.Interfaces;

namespace TweakerOS.View.Pages;

public partial class TweaksPage : Page
{
    public TweaksPage(object extraData)
    {
        InitializeComponent();

        textLabel.Content = ((ICategory)extraData).Name;
    }
}