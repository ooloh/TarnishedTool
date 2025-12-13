using System.Windows.Controls;
using SilkyRing.ViewModels;

namespace SilkyRing.Views.Tabs
{
    public partial class TravelTab : UserControl
    {
        public TravelTab(TravelViewModel travelViewModel)
        {
            InitializeComponent();
            DataContext = travelViewModel;
        }
    }
}