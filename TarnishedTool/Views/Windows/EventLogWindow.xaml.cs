// 

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TarnishedTool.Models;
using TarnishedTool.ViewModels;

namespace TarnishedTool.Views.Windows;

public partial class EventLogWindow : Window
{
    private bool _autoScroll = true;
    
    public EventLogWindow()
    {
        InitializeComponent();
        
        CommandBindings.Add(new CommandBinding(ApplicationCommands.SelectAll, (s, e) =>
        {
            LogListBox.SelectAll();
        }));
        
        CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, (s, e) =>
        {
            var selectedItems = LogListBox.SelectedItems
                .Cast<EventLogEntry>()
                .Select(x => x.DisplayText);
        
            Clipboard.SetText(string.Join(Environment.NewLine, selectedItems));
        }));
    }
    
    private void LogListBox_ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (e.ExtentHeightChange == 0)
        {
            _autoScroll = e.VerticalOffset >= e.ExtentHeight - e.ViewportHeight - 1;
        }
        
        if (_autoScroll && e.ExtentHeightChange != 0)
        {
            var scrollViewer = (ScrollViewer)e.OriginalSource;
            scrollViewer.ScrollToEnd();
        }
    }

    private void ExcludedEvent_DoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is ListBoxItem item && item.DataContext is uint eventId)
        {
            ((EventLogViewModel)DataContext).RemoveFromExcluded(eventId);
        }
    }

    private void Event_DoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is ListBoxItem item && item.DataContext is EventLogEntry entry)
        {
            ((EventLogViewModel)DataContext).AddToExcluded(entry.EventId);
        }
    }
}