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
    public void Update(float speed)
    {
        WalkParameterSet(speed);
    }

    /// <summary>
    /// �ړ����̃A�j���[�V�������󂯎��
    /// </summary>
    private void WalkParameterSet(float speed)
    {
        _animator.SetFloat(
            "Speed",
            speed);
    }

    public void JumpParameterSet(bool isJump)
    {
        _animator.SetBool(
            "IsJump",
            isJump);
    }

    public void IsGroundParameterSet(bool isGround)
    {
        _animator.SetBool(
            "IsGround",
            isGround);
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
        //    Debug.Log("OK");
        //}

        //_handPositionWeight = weight;
        //_handRotationWeight = weight;
    }
}
