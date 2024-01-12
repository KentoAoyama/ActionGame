using Domain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Application
{
    public class PlayerWalkState : IPlayerState
    {
        private readonly IPlayerComponent _player;

        [Inject]
        public PlayerWalkState(IPlayerComponent player)
        {
            _player = player;
        }

        public void Enter()
        {

        }

        public void Update()
        {
            //���͂��������Idle��Ԃ֑J��
            if (_player.Input.GetMoveDir() == Vector2.zero)
            {
                _player.TransitionState(new PlayerIdleState(_player));
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
}
