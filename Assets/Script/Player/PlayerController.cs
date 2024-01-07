using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Tooltip("PlayerのRigidbody")]
    [SerializeField]
    private Rigidbody _rb;

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
    /// PlayerのStateを管理するクラス
    /// </summary>
    public PlayerStateMachine StateMachine => _stateMachine;

    /// <summary>
    /// 入力を受け取るインターフェース
    /// </summary>
    [Inject]
    private readonly IInputProvider _input;

    public IInputProvider Input => _input;

    /// <summary>
    /// クラスの初期化処理を行う
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
    /// 通常の移動時に行う処理
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
