using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Player��Rigidbody")]
    [SerializeField]
    private Rigidbody _rb;

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
    /// Player��State���Ǘ�����N���X
    /// </summary>
    public PlayerStateMachine StateMachine => _stateMachine;

    /// <summary>
    /// ���͂��󂯎��C���^�[�t�F�[�X
    /// </summary>
    [Inject]
    private readonly IInputProvider _input;

    public IInputProvider Input => _input;

    /// <summary>
    /// �N���X�̏������������s��
    /// </summary>
    public void Initialized()
    {
        _move.Initialized();
        _stateMachine.Initialized(new PlayerIdleState(this));
    }

    void Update()
    {
        _animation.Update(_move.CurrentMoveSpeed);
        _stateMachine.Update();
    }

    /// <summary>
    /// �ʏ�̈ړ����ɍs������
    /// </summary>
    public void Move()
    {
        _move.Move(
            gameObject.transform,
            _rb,
            _input.GetMoveDir());
    }

    public void Attack()
    {
        //_attack.BulletShoot(
        //    _input.GetAim(),
        //    _input.GetShoot(),
        //    transform.position);

        //_attack.ShootPositionSet();

        //_animation.ChangeIKWeight(_input.GetAim());
    }
}
