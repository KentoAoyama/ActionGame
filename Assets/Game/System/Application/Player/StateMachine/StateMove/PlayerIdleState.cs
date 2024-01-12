using Domain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Application
{
    public class PlayerIdleState : IPlayerState
    {
        private readonly IPlayerComponent _player;

        [Inject]
        public PlayerIdleState(IPlayerComponent player)
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
                _player.TransitionState(new PlayerWalkState(_player));
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
}
