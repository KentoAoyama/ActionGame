using UnityEngine;

[System.Serializable]
public class PlayerAnimation
{
    [Tooltip("PlayerのAnimator")]
    [SerializeField]
    private Animator _animator;

    //[Header("IKの設定")]

    //[Tooltip("手の Position に対するウェイト")]
    //[SerializeField, Range(0f, 1f)]
    //private float _handPositionWeight = 0;

    //[Tooltip("手の Rotation に対するウェイト")]
    //[SerializeField, Range(0f, 1f)]
    //private float _handRotationWeight = 0;

    /// <summary>
    /// PlayerController内のUpdateで行う処理
    /// </summary>
    public void Update(float speed)
    {
        WalkParameterSet(speed);
    }

    /// <summary>
    /// 移動時のアニメーションを受け取る
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
