// 

using System;
using System.Collections.Generic;
using System.Windows.Input;
using TarnishedTool.Core;
using TarnishedTool.Enums;
using TarnishedTool.Interfaces;
using TarnishedTool.Models;
using TarnishedTool.Utilities;
using TarnishedTool.Views.Windows;

namespace TarnishedTool.ViewModels;

public class CreateCustomWarpViewModel : BaseViewModel
{
    private readonly IPlayerService _playerService;
    private readonly IGameTickService _gameTickService;

    private const byte DlcOverworld = 61;
    private const byte DlcDungeonStart = 20;
    private const byte DlcDungeonEnd = 28;
    private const byte DlcCatacombsStart = 40;
    private const byte DlcCatacombsEnd = 43;

    private Action<BlockWarp> _onWarpCreated;

    public CreateCustomWarpViewModel(
        Dictionary<string, List<BlockWarp>> customWarps,
        bool areOptionsEnabled,
        IStateService stateService,
        IPlayerService playerService,
        IGameTickService gameTickService, Action<BlockWarp> onWarpCreated)
    {
        _playerService = playerService;
        _gameTickService = gameTickService;
        _onWarpCreated = onWarpCreated;
        
        CustomWarps = new SearchableGroupedCollection<string, BlockWarp>(
            customWarps,
            (customWarp, search) => customWarp.Name.ToLower().Contains(search) ||
                                    customWarp.MainArea.ToLower().Contains(search));


        AreOptionsEnabled = areOptionsEnabled;
        if (AreOptionsEnabled) _gameTickService.Subscribe(LocationTick);

        stateService.Subscribe(State.Loaded, OnGameLoaded);
        stateService.Subscribe(State.NotLoaded, OnGameNotLoaded);

        SavePositionCommand = new DelegateCommand(SavePosition);
    }

    #region Commands

    public ICommand SavePositionCommand { get; }

    #endregion

    #region Properties

    private bool _areOptionsEnabled;

    public bool AreOptionsEnabled
    {
        get => _areOptionsEnabled;
        set => SetProperty(ref _areOptionsEnabled, value);
    }

    private MapLocation _mapLocation;

    public MapLocation MapLocation
    {
        get => _mapLocation;
        set => SetProperty(ref _mapLocation, value);
    }

    public SearchableGroupedCollection<string, BlockWarp> CustomWarps { get; }

    #endregion

    #region Private Methods

    private void OnGameLoaded()
    {
        AreOptionsEnabled = true;
        _gameTickService.Subscribe(LocationTick);
    }

    private void OnGameNotLoaded()
    {
        AreOptionsEnabled = false;
        _gameTickService.Unsubscribe(LocationTick);
    }

    private void LocationTick() => MapLocation = _playerService.GetMapLocation();

    private void SavePosition()
    {
        var results = MsgBox.ShowInputs(new[]
        {
            new InputField("category", "Category"),
            new InputField("name", "Warp Name"),
        }, "New Custom Warp");


        if (results == null) return; 

        if (string.IsNullOrWhiteSpace(results["category"]) || string.IsNullOrWhiteSpace(results["name"]))
        {
            MsgBox.Show("Category and name are required.", "Custom Warp");
            return;
        }

        CreateCustomWarp(results["category"], results["name"]);
    }

    private void CreateCustomWarp(string category, string name)
    {
        var warp = new BlockWarp
        {
            IsDlc = IsCurrentBlockDlc(),
            MainArea = category,
            Name = name,
            Position = new Position(
                MapLocation.BlockId,
                MapLocation.MapCoords,
                MapLocation.Angle
            )
        };
        
        CustomWarps.Add(category, warp);
        _onWarpCreated?.Invoke(warp);
    }

    private bool IsCurrentBlockDlc()
    {
        if (MapLocation.Area == DlcOverworld) return true;
        if (MapLocation.Area >=  DlcDungeonStart && MapLocation.Area <= DlcDungeonEnd ) return true;
        if (MapLocation.Area >= DlcCatacombsStart && MapLocation.Area <= DlcCatacombsEnd ) return true;
        return false;
    }

    #endregion
}