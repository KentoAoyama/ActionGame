using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Zenject;
using Application;
using Domain;

namespace Presentation
{

    public class PlayerController : MonoBehaviour, IPlayerComponent
    {
        [Tooltip("PlayerのRigidbody")]
        [SerializeField]
        private Rigidbody _rb;

        [Tooltip("PlayerのTransform")]
        [SerializeField]
        private Transform _transform;

        [Tooltip("Playerの移動に関する処理を定義するクラス")]
        [SerializeField]
        private PlayerMove _move;

        [Tooltip("Playerの攻撃に関する処理を定義するクラス")]
        [SerializeField]
        private PlayerAttack _attack;

        [Tooltip("PlayerのAnimationに関する処理を定義するクラス")]
        [SerializeField]
        private PlayerAnimation _animation;
        /// <summary>
        /// PlayerのAnimationを管理するクラスのプロパティ。StateMachineで使う用
        /// </summary>
        public PlayerAnimation Animation => _animation;

        [SerializeField]
        private PlayerStateMachine _stateMachine = new();

        /// <summary>
        /// 入力を受け取るインターフェース
        /// </summary>
        [Inject]
        private readonly IBattleInput _input;

        public IBattleInput Input => _input;

        /// <summary>
        /// クラスの初期化処理を行う
        /// </summary>
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            _move.Initialized(_rb, _transform);
            _stateMachine.Initialized(new PlayerIdleState(this));
        }

        private void Update()
        {
            _animation.Update(
               _move.LocalMoveDir,
               _move.CurrentMoveSpeed,
               _move.TurnSpeed);

            _stateMachine.Update();
        }

        /// <summary>
        /// 通常の移動時に行う処理
        /// </summary>
        public void Move()
        {
            _move.Move(_input.GetMoveDir());
        }

        /// <summary>
        /// 歩行時にカメラの向きに向きを変える処理
        /// </summary>
        public void LookRotationCameraDirMoveState()
        {
            _move.LookRotationCameraDirMoveState();
        }

        /// <summary>
        /// 停止時時にカメラの向きに向きを変える処理
        /// </summary>
        public void LookRotationCameraDirIdleState()
        {
            _move.LookRotationCameraDirIdleState();
        }

        public void TransitionState(IPlayerState state)
        {
            _stateMachine.TransitionState(state);
        }

        public void Attack()
        {
            //_attack.BulletShoot(
            //    _input.GetAim(),
            //    _input.GetShoot(),
            //    _transform);

            //_attack.ShootPositionSet();

            //_animation.ChangeIKWeight(_input.GetAim());
        }
    }
}
