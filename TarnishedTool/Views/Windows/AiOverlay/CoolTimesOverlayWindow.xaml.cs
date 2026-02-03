// 

using System;
using TarnishedTool.ViewModels;

namespace TarnishedTool.Views.Windows.AiOverlay;

public partial class CoolTimesOverlayWindow : AiOverlayWindowBase
{
    protected override string SettingsPrefix => "AiOverlayCoolTimes";
    
    public CoolTimesOverlayWindow(AiWindowViewModel viewModel, Action onClosed = null) 
        : base(onClosed)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}