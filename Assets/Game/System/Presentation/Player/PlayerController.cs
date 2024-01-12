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
        [Tooltip("Player��Rigidbody")]
        [SerializeField]
        private Rigidbody _rb;

        [Tooltip("Player��Transform")]
        [SerializeField]
        private Transform _transform;

        [Tooltip("Player�̈ړ��Ɋւ��鏈�����`����N���X")]
        [SerializeField]
        private PlayerMove _move;

        [Tooltip("Player�̍U���Ɋւ��鏈�����`����N���X")]
        [SerializeField]
        private PlayerAttack _attack;

        [Tooltip("Player��Animation�Ɋւ��鏈�����`����N���X")]
        [SerializeField]
        private PlayerAnimation _animation;
        /// <summary>
        /// Player��Animation���Ǘ�����N���X�̃v���p�e�B�BStateMachine�Ŏg���p
        /// </summary>
        public PlayerAnimation Animation => _animation;

        [SerializeField]
        private PlayerStateMachine _stateMachine = new();

        /// <summary>
        /// ���͂��󂯎��C���^�[�t�F�[�X
        /// </summary>
        [Inject]
        private readonly IBattleInput _input;

        public IBattleInput Input => _input;

        /// <summary>
        /// �N���X�̏������������s��
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
        /// �ʏ�̈ړ����ɍs������
        /// </summary>
        public void Move()
        {
            _move.Move(_input.GetMoveDir());
        }

        /// <summary>
        /// ���s���ɃJ�����̌����Ɍ�����ς��鏈��
        /// </summary>
        public void LookRotationCameraDirMoveState()
        {
            _move.LookRotationCameraDirMoveState();
        }

        /// <summary>
        /// ��~�����ɃJ�����̌����Ɍ�����ς��鏈��
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
