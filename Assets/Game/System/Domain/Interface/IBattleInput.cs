using UnityEngine;

namespace Domain
{
    public interface IBattleInput
    {
        /// <summary>
        /// 移動方向の入力処理
        /// </summary>
        /// <returns>移動の方向</returns>
        Vector2 GetMoveDir();

        /// <summary>
        /// 攻撃の入力処理
        /// </summary>
        /// <returns>攻撃の入力判定</returns>
        bool GetFire();

        /// <summary>
        /// エイムの入力判定
        /// </summary>
        /// <returns>エイムの入力判定</returns>
        bool GetAim();
    }
}
