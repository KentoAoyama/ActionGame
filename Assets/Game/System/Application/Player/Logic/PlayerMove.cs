using System.Collections;
using System.Threading;
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

    [Tooltip("�ړ��̉����x")]
    [SerializeField]
    private float _moveAcceleration = 1f;

    [Tooltip("�ړ��̌����x")]
    [SerializeField]
    private float _stopDeceleration = 1f;

    [Tooltip("��~���̌����ύX���~����ۂ̃A�j���[�V�����̃u�����h���x")]
    [SerializeField]
    private float _turnAnimationBlendSpeed = 0.8f;

    [Tooltip("��]�����s����p�x")]
    [SerializeField]
    private float _executeTurnAngle = 90f;

    [Tooltip("�A���ŉ�]�����s����p�x")]
    [SerializeField]
    private float _executeContinueTurnAngle = 10f;

    private Rigidbody _rb;
    private Transform _transform;

    // ===�ړ����x�֌W�̕ϐ��Q==
    private float _currentMoveSpeed = 0f;
    public float CurrentMoveSpeed => _currentMoveSpeed;

    private Vector3 _localMoveDir;
    public Vector3 LocalMoveDir => _localMoveDir;

    private Vector3 _currentVeclocity;

    // ===��]���x�֌W�̕ϐ��Q===
    public Quaternion _targetRotation;
    public float _targetAngle;
    public float _currentCameraAngle;

    public float _currentAngularVelocity;
    public bool _isRotation = false;
    public bool _isRotationFinish = false;
    public bool _isRightTurn = false;

    private float _turnSpeed;
    // ��]�̑��x -1~1�͈̔�
    public float TurnSpeed => _turnSpeed;



    public void Initialized(Rigidbody rb, Transform transform)
    {
        _rb = rb;
        _transform = transform;

        _currentMoveSpeed = 0;

        _targetRotation = _transform.rotation;
        _targetAngle = _transform.rotation.eulerAngles.y;
        _currentAngularVelocity = 0f;
        _isRotation = false;
        _isRotationFinish = false;
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
        var deltaTime = Time.deltaTime;

        // �v���C���[�̌�����ύX
        Vector3 lookDir = Camera.main.transform.forward;
        lookDir.y = 0f;
        //���������X�ɕύX
        Quaternion changeRotation = Quaternion.LookRotation(lookDir, Vector3.up);
        //��1������Quaternion���Q������Quaternion�܂ő�R�����̑��x�ŕω�
        _transform.rotation =
            Quaternion.RotateTowards(
                _transform.rotation,
                changeRotation,
                _moveRotationSpeed * deltaTime);
    }

    /// <summary>
    /// ��~�����ɃJ�����̌����Ɍ�����ς��鏈��
    /// </summary>
    public void LookRotationCameraDirIdleState()
    {
        var deltaTime = Time.deltaTime;

        // �v���C���[�̌����ƃJ�����̌����Ƃ̉�]�p�x���v�Z
        Vector3 lookDir = Camera.main.transform.forward;
        lookDir.y = 0f;
        Quaternion cameraRotation = Quaternion.LookRotation(lookDir, Vector3.up);
        float targetAngle = cameraRotation.eulerAngles.y - _transform.rotation.eulerAngles.y;
        _currentCameraAngle = targetAngle;

        if (_isRotation)
        {
            //��1������Quaternion���Q������Quaternion�܂ő�R�����̑��x�ŕω�
            _transform.rotation =
                Quaternion.RotateTowards(
                    _transform.rotation,
                    _targetRotation,
                    _stopRotationSpeed * deltaTime * _currentAngularVelocity);

            float currentAngle = _targetRotation.eulerAngles.y - _transform.rotation.eulerAngles.y;

            if (!_isRotationFinish)
            {
                _currentAngularVelocity += deltaTime / _turnAnimationBlendSpeed;
                if (Mathf.Abs(currentAngle) <= 5f)
                {
                    _isRotationFinish = true;
                }
            }
            else
            {
                _currentAngularVelocity -= deltaTime / _turnAnimationBlendSpeed;
                if (Mathf.Abs(currentAngle) <= 0.1f && _currentAngularVelocity <= 0f)
                {
                    _isRotation = false;
                    _isRotationFinish = false;
                }
            }
            _currentAngularVelocity = Mathf.Clamp01(_currentAngularVelocity);

            // TODO:��]�̕������ǂ��ɂ�����

            if (_isRightTurn)
            {
                _turnSpeed = Mathf.Lerp(0f, 1f, _currentAngularVelocity);
            }
            else
            {
                _turnSpeed = Mathf.Lerp(0f, -1f, _currentAngularVelocity);
            }          
        }

        // ���݂̉�]�����̔��΂������烊�^�[��
        if (_isRotation && _isRightTurn && targetAngle - _targetAngle < 0f ||
            _isRotation && !_isRightTurn && targetAngle - _targetAngle > 0f)
        {
            return;
        }

        // �p�x�����ȏ�Ȃ�ΖڕW�p�x���X�V
        if (Mathf.Abs(targetAngle) > _executeTurnAngle ||
            Mathf.Abs(targetAngle) > _executeContinueTurnAngle && _isRotation)
        {
            _targetRotation = cameraRotation;
            _targetAngle = targetAngle;
            _isRotationFinish = false;

            // ��]���łȂ���Ή�]���J�n
            if (!_isRotation)
            {
                _isRotation = true;
            }
        }
    }
}