using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [Tooltip("移動するスピード")]
    [SerializeField]
    private float _speed = 7f;

    [Tooltip("移動時に向きを変更するスピード")]
    [SerializeField]
    private float _moveRotationSpeed = 400f;

    [Tooltip("停止時に向きを変更するスピード")]
    [SerializeField]
    private float _stopRotationSpeed = 100f;

    [Tooltip("停止時の向き変更を停止する際のアニメーションのブレンド速度")]
    [SerializeField]
    private float _turnAnimationBlendSpeed = 0.8f;

    [Tooltip("移動の加速度")]
    [SerializeField]
    private float _moveAcceleration = 1f;

    [Tooltip("移動の減速度")]
    [SerializeField]
    private float _stopDeceleration = 1f;

    private Rigidbody _rb;
    private Transform _transform;

    // ===移動速度関係の変数群==
    private float _currentMoveSpeed = 0f;
    public float CurrentMoveSpeed => _currentMoveSpeed;

    private Vector3 _localMoveDir;
    public Vector3 LocalMoveDir => _localMoveDir;

    private Vector3 _currentVeclocity;

    // ===回転速度関係の変数群===
    private Quaternion _prevRotation;
    private float _prevTurnSpeed1;
    private float _prevTurnSpeed2;

    private float _turnSpeed;
    public float TurnSpeed => _turnSpeed;

    private float _currentAngularVelocity;


    public void Initialized(Rigidbody rb, Transform transform)
    {
        _rb = rb;
        _transform = transform;

        _currentMoveSpeed = 0;
        _prevRotation = _rb.rotation;
    }

    /// <summary>
    /// Playerの移動に関する処理を定義するメソッド
    /// </summary>
    public void Move(Vector2 inputDir)
    {
        var deltaTime = Time.deltaTime;

        //入力があるかどうか確認
        if (inputDir != Vector2.zero)
        {
            //移動をする方向をカメラの向きを参照したものにする
            var velocity = Vector3.right * inputDir.x + Vector3.forward * inputDir.y;
            velocity = Camera.main.transform.TransformDirection(velocity);
            velocity.y = 0f;
            velocity = _speed * velocity.normalized;
            velocity.y = _rb.velocity.y;

            //移動の速度を球面線形補間する
            _currentMoveSpeed += deltaTime / _moveAcceleration;
            _currentMoveSpeed = Mathf.Clamp01(_currentMoveSpeed); //0から1の範囲にクランプ
            Vector3 targetVelo = new(0f, _rb.velocity.y, 0f);
            velocity = Vector3.Slerp(targetVelo, velocity, _currentMoveSpeed);

            //移動を行う処理
            _rb.velocity = velocity;

            _currentVeclocity = velocity;
        }
        else
        {
            _currentMoveSpeed -= deltaTime / _stopDeceleration;
            _currentMoveSpeed = Mathf.Clamp01(_currentMoveSpeed); //0から1の範囲にクランプ
            _rb.velocity = Vector3.Slerp(Vector3.zero, _currentVeclocity, _currentMoveSpeed);
        }

        _localMoveDir = _transform.InverseTransformDirection(_rb.velocity) / _speed;
    }

    /// <summary>
    /// 歩行時にカメラの向きに向きを変える処理
    /// </summary>
    public void LookRotationCameraDirMoveState()
    {
        LookRotationCameraDir(_moveRotationSpeed, out bool _, out float _);
    }

    /// <summary>
    /// 停止時時にカメラの向きに向きを変える処理
    /// </summary>
    public void LookRotationCameraDirIdleState()
    {
        var deltaTime = Time.deltaTime;

        LookRotationCameraDir(_stopRotationSpeed, out bool isRotation, out float angle);

        if (isRotation)
        {
            // 徐々に加速させる
            _currentAngularVelocity += deltaTime / _turnAnimationBlendSpeed;
            _currentAngularVelocity = Mathf.Clamp01(_currentAngularVelocity);
            angle = Mathf.Clamp(angle, -1f, 1f);
            _turnSpeed = Mathf.Lerp(_prevTurnSpeed2, angle, _currentAngularVelocity);

            _prevTurnSpeed1 = _turnSpeed;
        }
        else
        {
            // 徐々に減速させる
            _currentAngularVelocity -= deltaTime / _turnAnimationBlendSpeed;
            _currentAngularVelocity = Mathf.Clamp01(_currentAngularVelocity);
            _turnSpeed = Mathf.Lerp(0f, _prevTurnSpeed1, _currentAngularVelocity);

            _prevTurnSpeed2 = _turnSpeed;
        }     
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rotationSpeed"></param>
    /// <returns>bool=回転をしているか  float=回転角度</returns>
    private void LookRotationCameraDir(float rotationSpeed, out bool isRotation, out float angle)
    {
        var deltaTime = Time.deltaTime;

        // プレイヤーの向きを変更する
        Vector3 lookDir = Camera.main.transform.forward;
        lookDir.y = 0f;
        //向きを徐々に変更する
        Quaternion changeRotation = Quaternion.LookRotation(lookDir, Vector3.up);
        //第1引数のQuaternionを第２引数のQuaternionまで第３引数の速度で変化させる
        _transform.rotation =
            Quaternion.RotateTowards(
                _transform.rotation,
                changeRotation,
                rotationSpeed * deltaTime);

        // プレイヤーの回転速度を計算する
        var currentRotation = _transform.rotation;

        // 角度の変化がx度以上なら回転していると判定する
        angle = currentRotation.eulerAngles.y - _prevRotation.eulerAngles.y;
        isRotation = Mathf.Abs(angle) > 0.1f;

        // 前フレームの姿勢を更新
        _prevRotation = currentRotation;
    }
}