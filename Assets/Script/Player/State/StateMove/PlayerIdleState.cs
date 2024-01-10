using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    private readonly PlayerController _player;

    public PlayerIdleState(PlayerController player)
    {
        _player = player;
    }

    public void Enter()
    {

    }

    public void Update()
    {
        // ˆÚ“®‚Ì“ü—Í‚ª‚ ‚ê‚ÎMoveState‚É‘JˆÚ
        if (_player.Input.GetMoveDir() != Vector2.zero)
        {
            _player.StateMachine.TransitionState(new PlayerWalkState(_player));
            return;
        }

        _player.Attack();
        _player.Move();
        _player.LookRotationCameraDirIdleState();
    }

    public void Exit()
    {

    }
}
