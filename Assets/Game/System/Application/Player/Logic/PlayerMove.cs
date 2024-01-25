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

    [Tooltip("��~���̌����ύX���s���ۂ̃A�j���[�V�����̃u�����h���x")]
    [SerializeField]
    private float _turnAcceleration = 0.8f;

    [Tooltip("��~���̌����ύX���~����ۂ̃A�j���[�V�����̃u�����h���x")]
    [SerializeField]
    private float _turnDeceleration = 0.8f;

    [Tooltip("�ړ��̉����x")]
    [SerializeField]
    private float _moveAcceleration = 1f;

    [Tooltip("�ړ��̌����x")]
    [SerializeField]
    private float _moveDeceleration = 1f;

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
            _currentMoveSpeed += _moveAcceleration * deltaTime;
            _currentMoveSpeed = Mathf.Clamp01(_currentMoveSpeed); //0����1�͈̔͂ɃN�����v
            Vector3 targetVelo = new(0f, _rb.velocity.y, 0f);
            velocity = Vector3.Slerp(targetVelo, velocity, _currentMoveSpeed);

            //�ړ����s������
            _rb.velocity = velocity;

            _currentVeclocity = velocity;
        }
        else
        {
            _currentMoveSpeed -= _moveDeceleration * deltaTime;
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
        //LookRotationCameraDir(_moveRotationSpeed, out bool _, out float _);
    }

    /// <summary>
    /// ��~�����ɃJ�����̌����Ɍ�����ς��鏈��
    /// </summary>
    public void LookRotationCameraDirIdleState()
    {
        var deltaTime = Time.deltaTime;

        LookRotationCameraDir(_stopRotationSpeed, out bool isRotation, out float rotationDir);

        if (isRotation)
        {
            // ���X�ɉ���������
            _currentAngularVelocity += _turnAcceleration * deltaTime;
            _currentAngularVelocity = Mathf.Clamp(_currentAngularVelocity, 0.1f, 1f);
            _turnSpeed = Mathf.Lerp(_prevTurnSpeed2, rotationDir, _currentAngularVelocity);

            _prevTurnSpeed1 = _turnSpeed;
        }
        else
        {
            // ���X�Ɍ���������
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

        // �v���C���[�̌�����ύX����
        Vector3 lookDir = Camera.main.transform.forward;
        lookDir.y = 0f;
        //���������X�ɕύX����
        Quaternion cameraRotation = Quaternion.LookRotation(lookDir, Vector3.up);
        //��1������Quaternion���Q������Quaternion�܂ő�R�����̑��x�ŕω�������
        _transform.rotation =
            Quaternion.RotateTowards(
                _transform.rotation,
                cameraRotation,
                rotationSpeed * _currentAngularVelocity * deltaTime);

        // �v���C���[�̉�]���x���v�Z����
        var currentRotation = _transform.rotation;

        // �p�x�̕ω���x�x�ȏ�Ȃ��]���Ă���Ɣ��肷��
        // 0~360�͈̔͂ɕϊ�
        float beforeAngle = _prevRotation.eulerAngles.y;
        float currentAngle = _transform.rotation.eulerAngles.y;
        beforeAngle = (beforeAngle + 360f) % 360f;
        currentAngle = (currentAngle + 360f) % 360f;

        // ��]���s���Ă��邩����
        isRotation = Mathf.Abs(currentAngle - beforeAngle) > 0.1f;
        rotationDir = 0f;

        // �E�ɉ�]���Ă��邩�����ɉ�]���Ă��邩�𔻒肷�� 0~360�͈̔͂Ŋp�x���n����邱�Ƃ��l������
        // ���݂̊p�x�ƑO�̃t���[���̊p�x�̍������v�Z
        if (currentAngle - beforeAngle < 0)
        {
            // ���݂̊p�x���O�̃t���[���̊p�x���傫���ꍇ
            // �^�[�Q�b�g�̊p�x���猻�݂̊p�x���������l��-180�x��菬�����ꍇ����]
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
            // ���݂̊p�x���^�[�Q�b�g�̊p�x���������ꍇ
            // ���݂̊p�x����^�[�Q�b�g�̊p�x���������l��180�x���傫���ꍇ�E��]
            if (beforeAngle - currentAngle > 180f)
            {
                rotationDir = -1;
            }
            else
            {
                rotationDir = 1;
            }
        }

        // �O�t���[���̎p�����X�V
        _prevRotation = currentRotation;
    }
}