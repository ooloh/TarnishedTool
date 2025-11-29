using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SilkyRing.Utilities;
using SilkyRing.ViewModels;
using Xceed.Wpf.Toolkit;

namespace SilkyRing.Views
{
    public partial class TargetTab
    {
        private readonly TargetViewModel _targetViewModel;

        public TargetTab(TargetViewModel targetViewModel)
        {
            InitializeComponent();
            _targetViewModel = targetViewModel;
            DataContext = _targetViewModel;
            
            InitializeUpDownHelpers();
        }
        
        private void InitializeUpDownHelpers()
        {
            _ = new UpDownHelper<double>(
                SpeedUpDown,
                _targetViewModel.SetSpeed
            );
        }
    }
}