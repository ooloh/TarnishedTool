// 

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TarnishedTool.Enums;
using TarnishedTool.Interfaces;
using TarnishedTool.Models;

namespace TarnishedTool.ViewModels;

internal class AiWindowViewModel : BaseViewModel
{
    private readonly IAiService _aiService;
    private readonly IStateService _stateService;
    private readonly IGameTickService _gameTickService;

    private readonly Dictionary<long, ChrInsEntry> _entriesByHandle = new();

    public AiWindowViewModel(IAiService aiService, IStateService stateService, IGameTickService gameTickService)
    {
        _aiService = aiService;
        _stateService = stateService;
        _gameTickService = gameTickService;
    }

    #region Commands

    #endregion

    #region Properties

    private ObservableCollection<ChrInsEntry> _chrInsEntries = new();

    public ObservableCollection<ChrInsEntry> ChrInsEntries

    {
        get => _chrInsEntries;
        set => SetProperty(ref _chrInsEntries, value);
    }

    #endregion

    #region Private Methods

    private void OnGameLoaded()
    {
        _gameTickService.Subscribe(ChrInsEntriesTick);
    }

    private void OnGameNotLoaded()
    {
        _gameTickService.Unsubscribe(ChrInsEntriesTick);
    }

    private void ChrInsEntriesTick()
    {
        var entries = _aiService.GetNearbyChrInsEntries();
        var seenHandles = new HashSet<long>();
        
        foreach (var entry in entries)
        {
            long handle = _aiService.GetHandleByChrIns(entry.ChrIns);

            seenHandles.Add(handle);
            if (_entriesByHandle.TryGetValue(handle, out _))
            {
                continue;
            }
            
            entry.NpcThinkParamId = _aiService.GetNpcThinkParamIdByChrIns(entry.ChrIns);

            if (entry.NpcThinkParamId == 0) continue;

            entry.Handle = handle;
            entry.ChrId = _aiService.GetChrIdByChrIns(entry.ChrIns);
            entry.NpcParamId = _aiService.GetNpcParamIdByChrIns(entry.ChrIns);
            
            _entriesByHandle[handle] = entry;
            ChrInsEntries.Add(entry);
        }
        

        var toRemove = _entriesByHandle.Keys.Where(h => !seenHandles.Contains(h)).ToList();
        foreach (var handle in toRemove)
        {
            var entry = _entriesByHandle[handle];
            _entriesByHandle.Remove(handle);
            ChrInsEntries.Remove(entry);
        }
    }

    #endregion

    #region Public Methods

    public void NotifyWindowOpen()
    {
        _stateService.Subscribe(State.Loaded, OnGameLoaded);
        _gameTickService.Subscribe(ChrInsEntriesTick);
    }

    public void NotifyWindowClosed()
    {
        _stateService.Subscribe(State.NotLoaded, OnGameNotLoaded);
        _gameTickService.Unsubscribe(ChrInsEntriesTick);
    }

    #endregion
}