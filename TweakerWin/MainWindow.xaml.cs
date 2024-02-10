using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TweakerWin.Tweaks;

namespace TweakerWin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PerformanceTweaks Tweak1 = new PerformanceTweaks();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Enable(object sender, RoutedEventArgs e)
        {
            Tweak1.Enable();
        }
        private void Button_Click_Disable(object sender, RoutedEventArgs e)
        {
            Tweak1.Disable();
        }
    }
}