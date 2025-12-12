using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using SilkyRing.Core;
using SilkyRing.Enums;
using SilkyRing.Interfaces;
using SilkyRing.Models;
using SilkyRing.Utilities;

namespace SilkyRing.ViewModels;

public class EnemyViewModel : BaseViewModel
{
    private readonly IEnemyService _enemyService;

    private const int EbNpcThinkParamId = 22000000;
    
    public EnemyViewModel(IEnemyService enemyService, IStateService stateService)
    {
        _enemyService = enemyService;

        stateService.Subscribe(State.Loaded, OnGameLoaded);
        stateService.Subscribe(State.NotLoaded, OnGameNotLoaded);

        EbForceActSequenceCommand = new DelegateCommand(ForceEbActSequence);

        _acts = new ObservableCollection<Act>(DataLoader.GetEbActs());
        SelectedAct = Acts.FirstOrDefault();
    }

    #region Commands

    public ICommand EbForceActSequenceCommand { get; set; }

    #endregion

    #region Properties

    private bool _areOptionsEnabled = true;

    public bool AreOptionsEnabled
    {
        get => _areOptionsEnabled;
        set => SetProperty(ref _areOptionsEnabled, value);
    }
    
    private bool _isNoDeathEnabled;

    public bool IsNoDeathEnabled
    {
        get => _isNoDeathEnabled;
        set
        {
            SetProperty(ref _isNoDeathEnabled, value);
            _enemyService.ToggleNoDeath(_isNoDeathEnabled);
        }
    }

    private bool _isNoDamageEnabled;

    public bool IsNoDamageEnabled
    {
        get => _isNoDamageEnabled;
        set
        {
            SetProperty(ref _isNoDamageEnabled, value);
            _enemyService.ToggleNoDamage(_isNoDamageEnabled);
        }
    }

    private bool _isNoHitEnabled;

    public bool IsNoHitEnabled
    {
        get => _isNoHitEnabled;
        set
        {
            SetProperty(ref _isNoHitEnabled, value);
            _enemyService.ToggleNoHit(_isNoHitEnabled);
        }
    }

    private bool _isNoAttackEnabled;

    public bool IsNoAttackEnabled
    {
        get => _isNoAttackEnabled;
        set
        {
            SetProperty(ref _isNoAttackEnabled, value);
            _enemyService.ToggleNoAttack(_isNoAttackEnabled);
        }
    }

    private bool _isNoMoveEnabled;

    public bool IsNoMoveEnabled
    {
        get => _isNoMoveEnabled;
        set
        {
            SetProperty(ref _isNoMoveEnabled, value);
            _enemyService.ToggleNoMove(_isNoMoveEnabled);
        }
    }

    private bool _isDisableAiEnabled;

    public bool IsDisableAiEnabled
    {
        get => _isDisableAiEnabled;
        set
        {
            SetProperty(ref _isDisableAiEnabled, value);
            _enemyService.ToggleDisableAi(_isDisableAiEnabled);
        }
    }
    
    private ObservableCollection<Act> _acts;
    
    public ObservableCollection<Act> Acts
    {
        get => _acts;
        set => SetProperty(ref _acts, value);
    }
    
    private Act _selectedAct;

    public Act SelectedAct
    {
        get => _selectedAct;
        set => SetProperty(ref _selectedAct, value);
    }

    #endregion

    #region Private Methods

    private void OnGameLoaded()
    {
        AreOptionsEnabled = true;
    }

    private void OnGameNotLoaded()
    {
        AreOptionsEnabled = false;
    }

    private void ForceEbActSequence()
    {
        int[] acts = [22, SelectedAct.ActIdx];
        _enemyService.ForceActSequence(acts, EbNpcThinkParamId);
    }

    #endregion
}