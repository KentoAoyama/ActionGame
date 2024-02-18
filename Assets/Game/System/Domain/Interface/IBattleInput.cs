using UnityEngine;

namespace Domain
{
    public interface IBattleInput
    {
        /// <summary>
        /// �ړ������̓��͏���
        /// </summary>
        /// <returns>�ړ��̕���</returns>
        Vector2 GetMoveDir();

        /// <summary>
        /// �U���̓��͏���
        /// </summary>
        /// <returns>�U���̓��͔���</returns>
        bool GetFire();

        /// <summary>
        /// �G�C���̓��͔���
        /// </summary>
        /// <returns>�G�C���̓��͔���</returns>
        bool GetAim();
    }
}
