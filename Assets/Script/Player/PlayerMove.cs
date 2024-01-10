using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [Tooltip("�ړ�����X�s�[�h")]
    [SerializeField]
    private float _speed = 500f;

    [Tooltip("������ύX����X�s�[�h")]
    [SerializeField]
    private float _rotationSpeed = 200f;

    [Tooltip("�ړ��̉����x")]
    [SerializeField]
    private float _moveAcceleration = 1f;

    [Tooltip("�ړ��̌����x")]
    [SerializeField]
    private float _stopAcceleration = 1f;

    private Rigidbody _rb;

    private float _currentMoveSpeed = 0f;
    public float CurrentMoveSpeed => _currentMoveSpeed;

    private Vector3 _currentVeclocity;

    private Vector3 _localVelocity;
    public Vector3 LocalVeclocity => _localVelocity;

    public void Initialized(Rigidbody rb)
    {
        _rb = rb;

        _currentMoveSpeed = 0;
    }

    /// <summary>
    /// Player�̈ړ��Ɋւ��鏈�����`���郁�\�b�h
    /// </summary>
    public void Move(Transform moveTransform, Vector2 inputDir)
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

            //���������X�ɕύX����
            Quaternion changeRotation = default;
            if (velocity.sqrMagnitude > 0.5f) //���x�����ȏ�Ȃ�A������ύX����
            {
                velocity.y = 0f;
                changeRotation = Quaternion.LookRotation(velocity, Vector3.up);
            }
            //��1������Quaternion���Q������Quaternion�܂ő�R�����̑��x�ŕω�������
            moveTransform.rotation =
                Quaternion.RotateTowards(
                    moveTransform.rotation,
                    changeRotation,
                    _rotationSpeed * deltaTime);

            _currentVeclocity = velocity;
        }
        else
        {
            _currentMoveSpeed -= deltaTime / _stopAcceleration;
            _currentMoveSpeed = Mathf.Clamp01(_currentMoveSpeed); //0����1�͈̔͂ɃN�����v
            _rb.velocity = Vector3.Slerp(Vector3.zero, _currentVeclocity, _currentMoveSpeed);
        }

            _localVelocity = moveTransform.InverseTransformDirection(_rb.velocity);
    }
}
