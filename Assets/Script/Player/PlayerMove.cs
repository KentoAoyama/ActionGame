using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [Tooltip("�ړ�����X�s�[�h")]
    [SerializeField]
    private float _speed = 7f;

    [Tooltip("�ړ����Ɍ�����ύX����X�s�[�h")]
    [SerializeField]
    private float _moveRotationSpeed = 400f;

    [Tooltip("��~���Ɍ�����ύX����X�s�[�h")]
    [SerializeField]
    private float _stopRotationSpeed = 100f;

    [Tooltip("��~���̌����ύX���~����ۂ̃A�j���[�V�����̃u�����h���x")]
    [SerializeField]
    private float _turnAnimationBlendSpeed = 0.8f;

    [Tooltip("�ړ��̉����x")]
    [SerializeField]
    private float _moveAcceleration = 1f;

    [Tooltip("�ړ��̌����x")]
    [SerializeField]
    private float _stopDeceleration = 1f;

    private Rigidbody _rb;
    private Transform _transform;

    // ===�ړ����x�֌W�̕ϐ��Q==
    private float _currentMoveSpeed = 0f;
    public float CurrentMoveSpeed => _currentMoveSpeed;

    private Vector3 _localMoveDir;
    public Vector3 LocalMoveDir => _localMoveDir;

    private Vector3 _currentVeclocity;

    // ===��]���x�֌W�̕ϐ��Q===
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
    /// Player�̈ړ��Ɋւ��鏈�����`���郁�\�b�h
    /// </summary>
    public void Move(Vector2 inputDir)
    {
        var deltaTime = Time.deltaTime;

        //���͂����邩�ǂ����m�F
        if (inputDir != Vector2.zero)
        {
            //�ړ�������������J�����̌������Q�Ƃ������̂ɂ���
            var velocity = Vector3.right * inputDir.x + Vector3.forward * inputDir.y;
            velocity = Camera.main.transform.TransformDirection(velocity);
            velocity.y = 0f;
            velocity = _speed * velocity.normalized;
            velocity.y = _rb.velocity.y;

            //�ړ��̑��x�����ʐ��`��Ԃ���
            _currentMoveSpeed += deltaTime / _moveAcceleration;
            _currentMoveSpeed = Mathf.Clamp01(_currentMoveSpeed); //0����1�͈̔͂ɃN�����v
            Vector3 targetVelo = new(0f, _rb.velocity.y, 0f);
            velocity = Vector3.Slerp(targetVelo, velocity, _currentMoveSpeed);

            //�ړ����s������
            _rb.velocity = velocity;

            _currentVeclocity = velocity;
        }
        else
        {
            _currentMoveSpeed -= deltaTime / _stopDeceleration;
            _currentMoveSpeed = Mathf.Clamp01(_currentMoveSpeed); //0����1�͈̔͂ɃN�����v
            _rb.velocity = Vector3.Slerp(Vector3.zero, _currentVeclocity, _currentMoveSpeed);
        }

        _localMoveDir = _transform.InverseTransformDirection(_rb.velocity) / _speed;
    }

    /// <summary>
    /// ���s���ɃJ�����̌����Ɍ�����ς��鏈��
    /// </summary>
    public void LookRotationCameraDirMoveState()
    {
        LookRotationCameraDir(_moveRotationSpeed, out bool _, out float _);
    }

    /// <summary>
    /// ��~�����ɃJ�����̌����Ɍ�����ς��鏈��
    /// </summary>
    public void LookRotationCameraDirIdleState()
    {
        var deltaTime = Time.deltaTime;

        LookRotationCameraDir(_stopRotationSpeed, out bool isRotation, out float angle);

        if (isRotation)
        {
            // ���X�ɉ���������
            _currentAngularVelocity += deltaTime / _turnAnimationBlendSpeed;
            _currentAngularVelocity = Mathf.Clamp01(_currentAngularVelocity);
            angle = Mathf.Clamp(angle, -1f, 1f);
            _turnSpeed = Mathf.Lerp(_prevTurnSpeed2, angle, _currentAngularVelocity);

            _prevTurnSpeed1 = _turnSpeed;
        }
        else
        {
            // ���X�Ɍ���������
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
    /// <returns>bool=��]�����Ă��邩  float=��]�p�x</returns>
    private void LookRotationCameraDir(float rotationSpeed, out bool isRotation, out float angle)
    {
        var deltaTime = Time.deltaTime;

        // �v���C���[�̌�����ύX����
        Vector3 lookDir = Camera.main.transform.forward;
        lookDir.y = 0f;
        //���������X�ɕύX����
        Quaternion changeRotation = Quaternion.LookRotation(lookDir, Vector3.up);
        //��1������Quaternion���Q������Quaternion�܂ő�R�����̑��x�ŕω�������
        _transform.rotation =
            Quaternion.RotateTowards(
                _transform.rotation,
                changeRotation,
                rotationSpeed * deltaTime);

        // �v���C���[�̉�]���x���v�Z����
        var currentRotation = _transform.rotation;

        // �p�x�̕ω���x�x�ȏ�Ȃ��]���Ă���Ɣ��肷��
        angle = currentRotation.eulerAngles.y - _prevRotation.eulerAngles.y;
        isRotation = Mathf.Abs(angle) > 0.1f;

        // �O�t���[���̎p�����X�V
        _prevRotation = currentRotation;
    }
}