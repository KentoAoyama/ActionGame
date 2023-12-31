using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : IPlayerState
{
    private readonly PlayerController _player;

    public PlayerWalkState(PlayerController player)
    {
        _player = player;
    }

    public void Enter()
    {

    }

    public void Update()
    {
        //入力が無ければIdle状態へ遷移
        if (_player.Input.GetMoveDir() == Vector2.zero)
        {
            _player.StateMachine.TransitionState(new PlayerIdleState(_player));
            return;
        }

        _player.Attack();
        _player.Move();
    }

    public void Exit()
    {

    }
}
