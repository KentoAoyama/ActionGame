using Application;
using Domain;
using UnityEngine;
using Zenject;

public class PlayerAimState : MonoBehaviour
{
    private readonly IPlayerComponent _player;

    [Inject]
    public PlayerAimState(IPlayerComponent player)
    {
        _player = player;
    }

    public void Enter()
    {
        
    }

    public void Update()
    {
        if (!_player.Input.GetAim())
        {
            // 移動の入力があればMoveStateに遷移
            if (_player.Input.GetMoveDir() != Vector2.zero)
            {
                _player.TransitionState(new PlayerWalkState(_player));
                return;
            }
            else
            {
                _player.TransitionState(new PlayerIdleState(_player));
                return;
            }
        }

        _player.Attack();
    }

    public void FixedUpdate()
    {
        _player.Move();
        _player.LookRotationCameraDirIdleState();
    }

    public void Exit() 
    {
        
    }
}
