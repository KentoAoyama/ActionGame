using Domain;
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
            //“ü—Í‚ª–³‚¯‚ê‚ÎIdleó‘Ô‚Ö‘JˆÚ
            if (_player.Input.GetMoveDir() == Vector2.zero)
            {
                _player.TransitionState(new PlayerIdleState(_player));
                return;
            }

            _player.Attack();
        }

        public void FixedUpdate()
        {
            _player.Move();
            _player.LookRotationCameraDirMoveState();
        }

        public void Exit()
        {

        }
    }
}
