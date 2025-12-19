using SilkyRing.ViewModels;

namespace SilkyRing.Views.Tabs
{
    public partial class UtilityTab
    {
        public UtilityTab(UtilityViewModel utilityViewModel)
        {
            InitializeComponent();
            DataContext = utilityViewModel;
        }
    }
}