using System.Collections.Generic;
using UnityEngine;

namespace Domain
{
    public interface IEnemysCompenent
    {
        void GenerateEnemy(Transform generatePos);

        void GenerateTestEnemy(Transform generatePos);

        void RemoveEnemy(IEnemyComponent enemyComponent);

        void RemoveAllEnemy();
    }
}
