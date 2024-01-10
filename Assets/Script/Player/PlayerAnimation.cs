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
    public void Update(Vector3 moveDir, float moveSpeed, float turnSpeed)
    {
        moveDir = moveDir.normalized;

        _animator.SetFloat(
            "SpeedX",
            moveDir.x);

        _animator.SetFloat(
            "SpeedZ",
            moveDir.y);

        _animator.SetFloat(
            "MoveSpeed",
            moveSpeed);

        _animator.SetFloat(
            "TurnSpeed",
            turnSpeed);
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
