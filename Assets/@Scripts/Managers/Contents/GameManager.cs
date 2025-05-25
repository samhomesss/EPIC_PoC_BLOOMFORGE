using System;
using UnityEngine;
using static Define;

/// <summary>
/// 게임 중앙 관리 
/// </summary>
public class GameManager
{
    #region Hero
    private Vector2 _moveDir;
    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set
        {
            _moveDir = value;
            OnMoveDirChanged?.Invoke(value);
        }
    }

    private bool _attackAction;
    public bool AttackAction
    {
        get => _attackAction;
        set
        {
            _attackAction = value;
            OnAttackActionEvent?.Invoke(value);
        }
    }

    private EInputSystemState _inputSystemState;
    public EInputSystemState InputSystemState
    {
        get => _inputSystemState;
        set

        {
            _inputSystemState = value;
            OnInputSystemStateChanged?.Invoke(_inputSystemState);
        }
    }
    #endregion



    #region Action
    public event Action<Vector2> OnMoveDirChanged;
    public event Action<bool> OnAttackActionEvent;
    public event Action<Define.EInputSystemState> OnInputSystemStateChanged;


    /// UI
    /// TODO : 나무를 관리하는것에 필요한 도구들을 정리 
    public event Action<int> OnGiveWaterEvent; // 나무에 물을 주었을때 발생할 이벤트 
    public event Action<int> OnGiveLightEvent; // 나무에 빛을 제공해주는 것 
    public event Action<bool> OnGiveBugDestroyToolEvent; // 해충 없애주는 이벤트 

    public void GiveWaterUI(int waterPoint)
    {
        OnGiveWaterEvent?.Invoke(waterPoint);
    }

    #endregion

    public void DisConnect()
    {
        OnMoveDirChanged = null;
        OnAttackActionEvent = null;
        OnInputSystemStateChanged = null;
    }
}
