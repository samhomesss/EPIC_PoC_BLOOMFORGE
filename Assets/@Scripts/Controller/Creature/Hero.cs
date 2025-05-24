using UnityEngine;
using static Define;
public class Hero : Creature
{
    Vector2 _moveDir = Vector2.zero;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        CreatureType = ECreatureType.Hero;
        CreatureState = ECreatureState.Idle;
        Speed = 5.0f;

        Managers.Object.Heros.Add(this); // 현재는 생성 하는게 아니라서 이거 추가 해야됨 

        #region Actions
        Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;
        Managers.Game.OnInputSystemStateChanged -= HandleOnInputSystemStateChanged;
        Managers.Game.OnInputSystemStateChanged += HandleOnInputSystemStateChanged;
        #endregion

        return true;
    }

    private void Update()
    {
        transform.TranslateEx(_moveDir * Time.deltaTime * Speed);
    }

    private void HandleOnMoveDirChanged(Vector2 dir)
    {
        _moveDir = dir;
    }

    private void HandleOnInputSystemStateChanged(EInputSystemState inputSystemState)
    {
        switch (inputSystemState)
        {
            case EInputSystemState.Idle:
                CreatureState = ECreatureState.Idle;
                break;
            case EInputSystemState.Move:
                CreatureState = ECreatureState.Move;
                break;
            case EInputSystemState.Attack:
                CreatureState = ECreatureState.Attack;
                break;
        }
    }
}
