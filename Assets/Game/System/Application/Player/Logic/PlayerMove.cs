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

    [Tooltip("停止時の向き変更を行う際のアニメーションのブレンド速度")]
    [SerializeField]
    private float _turnAcceleration = 0.8f;

    [Tooltip("停止時の向き変更を停止する際のアニメーションのブレンド速度")]
    [SerializeField]
    private float _turnDeceleration = 0.8f;

    [Tooltip("移動の加速度")]
    [SerializeField]
    private float _moveAcceleration = 1f;

    [Tooltip("移動の減速度")]
    [SerializeField]
    private float _moveDeceleration = 1f;

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
            _currentMoveSpeed += _moveAcceleration * deltaTime;
            _currentMoveSpeed = Mathf.Clamp01(_currentMoveSpeed); //0から1の範囲にクランプ
            Vector3 targetVelo = new(0f, _rb.velocity.y, 0f);
            velocity = Vector3.Slerp(targetVelo, velocity, _currentMoveSpeed);

            //移動を行う処理
            _rb.velocity = velocity;

            _currentVeclocity = velocity;
        }
        else
        {
            _currentMoveSpeed -= _moveDeceleration * deltaTime;
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
        //LookRotationCameraDir(_moveRotationSpeed, out bool _, out float _);
    }

    /// <summary>
    /// 停止時時にカメラの向きに向きを変える処理
    /// </summary>
    public void LookRotationCameraDirIdleState()
    {
        var deltaTime = Time.deltaTime;

        LookRotationCameraDir(_stopRotationSpeed, out bool isRotation, out float rotationDir);

        if (isRotation)
        {
            // 徐々に加速させる
            _currentAngularVelocity += _turnAcceleration * deltaTime;
            _currentAngularVelocity = Mathf.Clamp(_currentAngularVelocity, 0.1f, 1f);
            _turnSpeed = Mathf.Lerp(_prevTurnSpeed2, rotationDir, _currentAngularVelocity);

            _prevTurnSpeed1 = _turnSpeed;
        }
        else
        {
            // 徐々に減速させる
            _currentAngularVelocity -= _turnDeceleration * deltaTime;
            _currentAngularVelocity = Mathf.Clamp(_currentAngularVelocity, 0.1f, 1f);
            _turnSpeed = Mathf.Lerp(0f, _prevTurnSpeed1, _currentAngularVelocity);

            _prevTurnSpeed2 = _turnSpeed;

            if (_currentAngularVelocity == 0f)
            {
                _prevTurnSpeed1 = 0f;
            }
        }     
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rotationSpeed"></param>
    /// <param name="isRotation"></param>
    /// <param name="rotationDir"></param>
    private void LookRotationCameraDir(float rotationSpeed, out bool isRotation, out float rotationDir)
    {
        var deltaTime = Time.deltaTime;

        // プレイヤーの向きを変更する
        Vector3 lookDir = Camera.main.transform.forward;
        lookDir.y = 0f;
        //向きを徐々に変更する
        Quaternion cameraRotation = Quaternion.LookRotation(lookDir, Vector3.up);
        //第1引数のQuaternionを第２引数のQuaternionまで第３引数の速度で変化させる
        _transform.rotation =
            Quaternion.RotateTowards(
                _transform.rotation,
                cameraRotation,
                rotationSpeed * _currentAngularVelocity * deltaTime);

        // プレイヤーの回転速度を計算する
        var currentRotation = _transform.rotation;

        // 角度の変化がx度以上なら回転していると判定する
        // 0~360の範囲に変換
        float beforeAngle = _prevRotation.eulerAngles.y;
        float currentAngle = _transform.rotation.eulerAngles.y;
        beforeAngle = (beforeAngle + 360f) % 360f;
        currentAngle = (currentAngle + 360f) % 360f;

        // 回転が行われているか判定
        isRotation = Mathf.Abs(currentAngle - beforeAngle) > 0.1f;
        rotationDir = 0f;

        // 右に回転しているかか左に回転しているかを判定する 0~360の範囲で角度が渡されることを考慮する
        // 現在の角度と前のフレームの角度の差分を計算
        if (currentAngle - beforeAngle < 0)
        {
            // 現在の角度より前のフレームの角度が大きい場合
            // ターゲットの角度から現在の角度を引いた値が-180度より小さい場合左回転
            if (currentAngle - beforeAngle < -180f)
            {
                rotationDir = 1;
            }
            else
            {
                rotationDir = -1;
            }
        }
        else if (currentAngle - beforeAngle > 0)
        {
            // 現在の角度よりターゲットの角度が小さい場合
            // 現在の角度からターゲットの角度を引いた値が180度より大きい場合右回転
            if (beforeAngle - currentAngle > 180f)
            {
                rotationDir = -1;
            }
            else
            {
                rotationDir = 1;
            }
        }

        // 前フレームの姿勢を更新
        _prevRotation = currentRotation;
    }
}