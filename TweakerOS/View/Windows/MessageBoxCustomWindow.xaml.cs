using System.Windows;

namespace TweakerOS.View.Windows;

public partial class MessageBoxCustomWindow : Window
{
    private bool isCancelButtonEnabled = false;
    public MessageBoxCustomWindow(string text = "", string topic = "")
    {
        InitializeComponent();
        textLabel.Content = text;
        Title = topic;
    }

    public bool? ShowRebootExplorerRequired()
    {
        Title = "Требуется перезапуск проводника!";
        textLabel.Content = "Чтобы твик вступил в силу, необходимо перезагрузить проводник!";
        isCancelButtonEnabled = true;
        return this.ShowDialog();
    }
    
}