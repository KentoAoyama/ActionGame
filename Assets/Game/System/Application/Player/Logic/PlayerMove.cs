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

    [Tooltip("移動の加速度")]
    [SerializeField]
    private float _moveAcceleration = 1f;

    [Tooltip("移動の減速度")]
    [SerializeField]
    private float _stopDeceleration = 1f;

    [Tooltip("停止時の向き変更を停止する際のアニメーションのブレンド速度")]
    [SerializeField]
    private float _turnAnimationBlendSpeed = 0.8f;

    [Tooltip("回転を実行する角度")]
    [SerializeField]
    private float _executeTurnAngle = 90f;

    [Tooltip("連続で回転を実行する角度")]
    [SerializeField]
    private float _executeContinueTurnAngle = 10f;

    private Rigidbody _rb;
    private Transform _transform;

    // ===移動速度関係の変数群==
    private float _currentMoveSpeed = 0f;
    public float CurrentMoveSpeed => _currentMoveSpeed;

    private Vector3 _localMoveDir;
    public Vector3 LocalMoveDir => _localMoveDir;

    private Vector3 _currentVeclocity;

    // ===回転速度関係の変数群===
    public Quaternion _targetRotation;
    public float _targetAngle;
    public float _currentAngle;
    public float _currentCameraAngle;

    public float _currentAngularVelocity;
    public bool _isRotation = false;
    public bool _isRotationFinish = false;
    public bool _isRightTurn = false;

    private float _turnSpeed;
    // 回転の速度 -1~1の範囲
    public float TurnSpeed => _turnSpeed;



    public void Initialized(Rigidbody rb, Transform transform)
    {
        _rb = rb;
        _transform = transform;

        _currentMoveSpeed = 0;

        _targetRotation = _transform.rotation;
        _targetAngle = _transform.rotation.eulerAngles.y;
        _currentAngle = _transform.rotation.eulerAngles.y;
        _currentAngularVelocity = 0f;
        _isRotation = false;
        _isRotationFinish = false;
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
        var deltaTime = Time.deltaTime;

        // プレイヤーの向きを変更
        Vector3 lookDir = Camera.main.transform.forward;
        lookDir.y = 0f;
        //向きを徐々に変更
        Quaternion changeRotation = Quaternion.LookRotation(lookDir, Vector3.up);
        //第1引数のQuaternionを第２引数のQuaternionまで第３引数の速度で変化
        _transform.rotation =
            Quaternion.RotateTowards(
                _transform.rotation,
                changeRotation,
                _moveRotationSpeed * deltaTime);
    }

    /// <summary>
    /// 停止時時にカメラの向きに向きを変える処理
    /// </summary>
    public void LookRotationCameraDirIdleState()
    {
        var deltaTime = Time.deltaTime;

        if (_isRotation)
        {
            ChangeValue(
                ref _currentAngle, 
                _targetAngle, 
                _stopRotationSpeed * deltaTime * _currentAngularVelocity);

            _transform.rotation = Quaternion.Euler(0f, _currentAngle, 0f);
            // ターゲットの角度との差分を計算
            float angle = Mathf.Abs(_currentAngle) - Mathf.Abs(_targetAngle);

            if (!_isRotationFinish)
            {
                _currentAngularVelocity += deltaTime / _turnAnimationBlendSpeed;
                if (angle < 10f)
                {
                    _isRotationFinish = true;
                }
            }
            else
            {
                _currentAngularVelocity -= deltaTime / _turnAnimationBlendSpeed;
                if (angle <= 0.1f && _currentAngularVelocity <= 0f)
                {
                    _isRotation = false;
                    _isRotationFinish = false;
                }
            }
            _currentAngularVelocity = Mathf.Clamp01(_currentAngularVelocity);

            if (_isRightTurn)
            {
                _turnSpeed = Mathf.Lerp(0f, 1f, _currentAngularVelocity);
            }
            else
            {
                _turnSpeed = Mathf.Lerp(0f, -1f, _currentAngularVelocity);
            }
        }

        // プレイヤーの向きとカメラの向きとの回転角度を計算
        Vector3 lookDir = Camera.main.transform.forward;
        lookDir.y = 0f;
        Quaternion cameraRotation = Quaternion.LookRotation(lookDir, Vector3.up);
        float cameraAngle = cameraRotation.eulerAngles.y;
        // cameraAngleが-180~180の範囲になるので0~360の範囲に変換
        if (cameraAngle < 0f)
        {
            cameraAngle += 360f;
        }
        _currentCameraAngle = (cameraAngle + _currentAngle) % 360;

        //// 現在の回転方向の反対だったらリターン
        if ()
        {
            return;
        }

        //// 角度が一定以上ならば目標角度を更新
        if ()
        {
            _targetAngle = _currentCameraAngle % 360;
            _isRotationFinish = false;

            // 回転中でなければ回転を開始
            if (!_isRotation)
            {
                _isRotation = true;

                if (_currentAngle)
            }
        }
    }

    private float ChangeValue(ref float angle, float targetAngle, float speed)
    {
        float resultAngle = angle;
        float angleAbs = Mathf.Abs(angle);
        float targetAngleAbs = Mathf.Abs(targetAngle);

        if (angleAbs - targetAngleAbs > 0.1f)
        {
            if (angleAbs > targetAngleAbs)
            {
                resultAngle -= speed;
            }
            else
            {
                resultAngle += speed;
            }
        }
        else
        {
            resultAngle = targetAngle;
        }

        return resultAngle % 360;
    }

    ///// <summary>
    ///// 停止時時にカメラの向きに向きを変える処理
    ///// </summary>
    //public void LookRotationCameraDirIdleState()
    //{
    //    var deltaTime = Time.deltaTime;

    //    if (_isRotation)
    //    {
    //        _currentAngle = ChangeValue(_currentAngle, _targetAngle, _turnSpeed * deltaTime * _currentAngularVelocity);
    //        _transform.rotation = Quaternion.Euler(0f, _currentAngle, 0f);
    //        // ターゲットの角度との差分を計算
    //        float angle = Mathf.Abs(_currentAngle) - Mathf.Abs(_targetAngle);

    //        if (!_isRotationFinish)
    //        {
    //            _currentAngularVelocity += deltaTime / _turnAnimationBlendSpeed;
    //            if (angle < 10f)
    //            {
    //                _isRotationFinish = true;
    //            }
    //        }
    //        else
    //        {
    //            _currentAngularVelocity -= deltaTime / _turnAnimationBlendSpeed;
    //            if (angle <= 0.1f && _currentAngularVelocity <= 0f)
    //            {
    //                _isRotation = false;
    //                _isRotationFinish = false;
    //            }
    //        }
    //        _currentAngularVelocity = Mathf.Clamp01(_currentAngularVelocity);

    //        if (_isRightTurn)
    //        {
    //            _turnSpeed = Mathf.Lerp(0f, 1f, _currentAngularVelocity);
    //        }
    //        else
    //        {
    //            _turnSpeed = Mathf.Lerp(0f, -1f, _currentAngularVelocity);
    //        }
    //    }

    //    // プレイヤーの向きとカメラの向きとの回転角度を計算
    //    Vector3 lookDir = Camera.main.transform.forward;
    //    lookDir.y = 0f;
    //    Quaternion cameraRotation = Quaternion.LookRotation(lookDir, Vector3.up);
    //    float cameraAngle = cameraRotation.eulerAngles.y;
    //    // cameraAngleが0~360の範囲になるので-180~180の範囲に変換
    //    if (cameraAngle > 180f)
    //    {
    //        cameraAngle -= 360f;
    //    }
    //    _currentCameraAngle = cameraAngle + _currentAngle;

    //    // 現在の回転方向の反対だったらリターン
    //    if (_isRotation && _isRightTurn && _currentCameraAngle - _targetAngle < 0f ||
    //        _isRotation && !_isRightTurn && _currentCameraAngle - _targetAngle > 0f)
    //    {
    //        return;
    //    }

    //    // 角度が一定以上ならば目標角度を更新
    //    if (Mathf.Abs(_currentAngle) > _executeTurnAngle ||
    //        Mathf.Abs(cameraAngle) > _executeContinueTurnAngle && _isRotation)
    //    {
    //        _targetAngle = cameraAngle;
    //        _isRotationFinish = false;

    //        // 回転中でなければ回転を開始
    //        if (!_isRotation)
    //        {
    //            _isRotation = true;
    //        }
    //    }
    //}
}