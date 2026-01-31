// 

using System;
using System.Windows;

namespace TarnishedTool.Views.Windows;

public partial class AiWindow : Window
{
    public AiWindow()
    {
        InitializeComponent();
    }
    
    
    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        (DataContext as IDisposable)?.Dispose();
    }
}