// 

using System.Windows.Input;
using TarnishedTool.ViewModels;

namespace TarnishedTool.Views.Tabs;

public partial class ItemTab
{
    public ItemTab(ItemViewModel itemViewModel)
    {
        InitializeComponent();
        DataContext = itemViewModel;
    }

    private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is ItemViewModel vm && vm.SpawnItemCommand.CanExecute(null))
        {
            vm.SpawnItemCommand.Execute(null);
        }
    }
}