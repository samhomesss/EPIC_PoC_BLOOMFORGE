using UnityEngine;
using static Define;
public class InputSystem : MonoBehaviour
{
    float _inputX;
    float _inputY;
    Vector2 moveDir = Vector2.zero;

    private void Update()
    {
        Move();
        Attack();
    }
    void Move()
    {
        _inputX = Input.GetAxis("Horizontal");
        _inputY = Input.GetAxis("Vertical");
        moveDir = new Vector2(_inputX, _inputY);
        Managers.Game.MoveDir = moveDir;
        Managers.Game.InputSystemState = EInputSystemState.Move;

    }

    void Attack()
    {
        if ((Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0)))
        {
            Managers.Game.AttackAction = true;
            Managers.Game.InputSystemState = Define.EInputSystemState.Attack;
        }
    }

}
