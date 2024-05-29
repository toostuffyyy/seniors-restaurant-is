using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Seniors.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}