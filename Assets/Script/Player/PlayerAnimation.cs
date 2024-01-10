using UnityEngine;

[System.Serializable]
public class PlayerAnimation
{
    [Tooltip("Player��Animator")]
    [SerializeField]
    private Animator _animator;

    //[Header("IK�̐ݒ�")]

    //[Tooltip("��� Position �ɑ΂���E�F�C�g")]
    //[SerializeField, Range(0f, 1f)]
    //private float _handPositionWeight = 0;

    //[Tooltip("��� Rotation �ɑ΂���E�F�C�g")]
    //[SerializeField, Range(0f, 1f)]
    //private float _handRotationWeight = 0;

    /// <summary>
    /// PlayerController����Update�ōs������
    /// </summary>
    public void Update(Vector3 velocity, float moveSpeed)
    {
        WalkParameterSet(
            new Vector2(velocity.x, velocity.z),
            moveSpeed);
    }

    /// <summary>
    /// �ړ����̃A�j���[�V�������󂯎��
    /// </summary>
    private void WalkParameterSet(Vector2 velocity, float moveSpeed)
    {
        velocity = velocity.normalized;

        _animator.SetFloat(
            "SpeedX",
            velocity.x);

        _animator.SetFloat(
            "SpeedZ",
            velocity.y);

        _animator.SetFloat(
            "MoveSpeed",
            moveSpeed);
    }

    public void SetIK(Transform shootPos)
    {
        //_animator.SetIKPosition(
        //    AvatarIKGoal.RightHand,
        //    shootPos.transform.position);

        //_animator.SetIKRotation(
        //    AvatarIKGoal.RightHand,
        //    shootPos.transform.rotation);

        //_animator.SetIKPositionWeight(
        //    AvatarIKGoal.RightHand,
        //    _handPositionWeight);

        //_animator.SetIKRotationWeight(
        //    AvatarIKGoal.RightHand,
        //    -_handRotationWeight);
    }

    public void ChangeIKWeight(bool isAim)
    {
        //float weight = 0f;

        //if (isAim)
        //{
        //    weight = 1f;
        //}

        //_handPositionWeight = weight;
        //_handRotationWeight = weight;
    }
}
