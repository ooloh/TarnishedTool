// 

using System;
using System.Windows;

namespace TarnishedTool.Views.Windows;

public partial class GoalWindow : Window
{
    public GoalWindow()
    {
        InitializeComponent();
    }
    
    
    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        (DataContext as IDisposable)?.Dispose();
    }
}