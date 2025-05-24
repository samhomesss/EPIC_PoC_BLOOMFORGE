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
    #endregion

    public void DisConnect()
    {
        OnMoveDirChanged = null;
        OnAttackActionEvent = null;
        OnInputSystemStateChanged = null;
    }
}
