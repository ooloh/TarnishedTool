using SilkyRing.ViewModels;

namespace SilkyRing.Views.Tabs
{
    public partial class EventTab
    {
        
        public EventTab(EventViewModel eventViewModel)
        {
            InitializeComponent();
            DataContext = eventViewModel;
        }
    }
}