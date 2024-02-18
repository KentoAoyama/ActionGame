using UnityEngine;

namespace Domain
{
    public interface IEnemysCompenent
    {
        void GenerateEnemy(Transform generatePos);

        void GenerateTestEnemy(Transform generatePos);

        void RemoveEnemy(IEnemyComponent enemyComponent);
    }
}
