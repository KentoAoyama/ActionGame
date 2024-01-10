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
        //“ü—Í‚ª–³‚¯‚ê‚ÎIdleó‘Ô‚Ö‘JˆÚ
        if (_player.Input.GetMoveDir() == Vector2.zero)
        {
            _player.StateMachine.TransitionState(new PlayerIdleState(_player));
            return;
        }

        _player.Attack();
        _player.Move();
        _player.LookRotationCameraDirMoveState();
    }

    public void Exit()
    {

    }
}
