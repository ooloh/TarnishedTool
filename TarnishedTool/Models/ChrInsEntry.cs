// 

using System;
using System.Windows.Input;
using TarnishedTool.Core;
using TarnishedTool.ViewModels;

namespace TarnishedTool.Models;

public class ChrInsEntry(nint chrIns) : BaseViewModel
{
    public nint ChrIns { get; set; } = chrIns;
    public int ChrId { get; set; }
    public uint NpcParamId { get; set; }
    public int NpcThinkParamId { get; set; }
    public long Handle { get; set; }
    public string Name { get; set; }
    public Action<ChrInsEntry, string, bool> OnOptionChanged { get; set; }
    public Action<ChrInsEntry, string> OnCommandExecuted { get; set; }
    public Action<ChrInsEntry> OnExpanded { get; set; }

    public string Display =>
        $@"{Name}   ChrId: {ChrId} NpcParamId: {NpcParamId} NpcThinkParamId: {NpcThinkParamId}";
    
    private bool _isExpanded;
    public bool IsExpanded
    {
        get => _isExpanded;
        set
        {
            if (SetProperty(ref _isExpanded, value) && value)
            {
                OnExpanded?.Invoke(this);
            }
        }
    }


    private bool _isAiDisabled;
    public bool IsAiDisabled
    {
        get => _isAiDisabled;
        set
        {
            if (SetProperty(ref _isAiDisabled, value))
            {
               OnOptionChanged.Invoke(this, nameof(IsAiDisabled), value);
            }
        }
    }
    
    private bool _isTargetViewEnabled;
    public bool IsTargetViewEnabled
    {
        get => _isTargetViewEnabled;
        set
        {
            if (SetProperty(ref _isTargetViewEnabled, value))
            {
                OnOptionChanged.Invoke(this, nameof(IsTargetViewEnabled), value);
            }
        }
    }
    
    private ICommand _warpCommand;
    public ICommand WarpCommand => _warpCommand ??= new DelegateCommand(() =>
    {
        OnCommandExecuted?.Invoke(this, nameof(WarpCommand));
    });
    
    private ICommand _openGoalWindowCommand;
    public ICommand OpenAiWindowCommand => _openGoalWindowCommand ??= new DelegateCommand(() =>
    {
        OnCommandExecuted?.Invoke(this, nameof(OpenAiWindowCommand));
    });

}