using SilkyRing.ViewModels;

namespace SilkyRing.Views.Tabs;

public partial class EnemyTab
{
    public EnemyTab(EnemyViewModel enemyViewModel)
    {
        InitializeComponent();
        DataContext = enemyViewModel;
    }
}