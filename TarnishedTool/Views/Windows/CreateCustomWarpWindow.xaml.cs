// 

using System.Windows;
using TarnishedTool.Utilities;

namespace TarnishedTool.Views.Windows;

public partial class CreateCustomWarpWindow : TopmostWindow
{
    public CreateCustomWarpWindow()
    {
        InitializeComponent();
        
        
        if (Application.Current.MainWindow != null)
        {
            Application.Current.MainWindow.Closing += (_, _) => Close();
        }
        
        Loaded += (s, e) =>
        {
            if (SettingsManager.Default.CreateCustomWarpWindowLeft > 0)
                Left = SettingsManager.Default.CreateCustomWarpWindowLeft;
        
            if (SettingsManager.Default.CreateCustomWarpWindowTop > 0)
                Top = SettingsManager.Default.CreateCustomWarpWindowTop;
            
            AlwaysOnTopCheckBox.IsChecked = SettingsManager.Default.CreateCustomWarpWindowAlwaysOnTop;
        };
    }
}