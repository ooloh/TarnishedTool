// 

using System.Windows;
using TarnishedTool.Utilities;
using TarnishedTool.ViewModels;

namespace TarnishedTool.Views.Windows;

public partial class AiWindow : TopmostWindow
{
    public AiWindow()
    {
        InitializeComponent();
        
        if (Application.Current.MainWindow != null)
        {
            Application.Current.MainWindow.Closing += (sender, args) => { Close(); };
        }
        
        Loaded += (s, e) =>
        {
            // if (SettingsManager.Default.AttackInfoWindowLeft > 0)
            //     Left = SettingsManager.Default.AttackInfoWindowLeft;
            //
            // if (SettingsManager.Default.AttackInfoWindowTop > 0)
            //     Top = SettingsManager.Default.AttackInfoWindowTop;
            //
            // AlwaysOnTopCheckBox.IsChecked = SettingsManager.Default.AtkInfoAlwaysOnTop;
        };
        
    }
    
    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
        base.OnClosing(e);
        var vm = (AiWindowViewModel)DataContext;
        vm.ClearSelected();

        // SettingsManager.Default.AttackInfoWindowLeft = Left;
        // SettingsManager.Default.AttackInfoWindowTop = Top;
        // SettingsManager.Default.AtkInfoAlwaysOnTop = AlwaysOnTopCheckBox.IsChecked ?? false;
        // SettingsManager.Default.Save();
    }
}