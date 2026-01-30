// 

using System.Collections.ObjectModel;
using System.Linq;

namespace TarnishedTool.ViewModels;

public class GoalViewModel : BaseViewModel
{
    private string _name;
    private float _life;
    private float _turnTime;
    private ObservableCollection<GoalParamViewModel> _params;
    private ObservableCollection<GoalViewModel> _children;
    private int _indentLevel;

    #region Properties

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public float Life
    {
        get => _life;
        set => SetProperty(ref _life, value);
    }

    public float TurnTime
    {
        get => _turnTime;
        set => SetProperty(ref _turnTime, value);
    }

    public bool HasTurnTime => TurnTime > 0;

    public ObservableCollection<GoalParamViewModel> Params
    {
        get => _params;
        set => SetProperty(ref _params, value);
    }

    public bool HasParams => Params?.Count > 0;

    public ObservableCollection<GoalViewModel> Children
    {
        get => _children;
        set => SetProperty(ref _children, value);
    }

    public int IndentLevel
    {
        get => _indentLevel;
        set => SetProperty(ref _indentLevel, value);
    }
    
    public string FormattedParams => Params != null 
        ? string.Join(", ", Params.Select(p => $"{p.Label}:{p.Value:F1}")) 
        : string.Empty;

    #endregion
}