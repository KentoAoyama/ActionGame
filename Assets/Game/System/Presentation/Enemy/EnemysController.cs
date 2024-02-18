using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Domain;
using Application;

namespace Presentation
{
    public class EnemysController : MonoBehaviour, IEnemysCompenent
    {
        [SerializeField]
        private EnemyGenerator enemyGenerator;

        private EnemysHolder enemysHolder;

        public void Initialized()
        {
            // “G‚ğŒŸõ‚µA‘S‚Äæ“¾‚·‚é
            var enemys = FindObjectsByType<ShootTestObj>(FindObjectsSortMode.None);

            List<IEnemyComponent> enemyComponents = new();
            foreach (var enemy in enemys)
            {
                enemyComponents.Add(enemy);
            }

            enemysHolder = new EnemysHolder(enemyComponents);
        }

        public void GenerateEnemy(Transform generatePos)
        {
            enemyGenerator.GenerateEnemy(generatePos);
            enemysHolder.AddEnemy(enemyGenerator.GenerateEnemy(generatePos));
        }

        public void GenerateTestEnemy(Transform generatePos)
        {
            enemyGenerator.GenerateTestEnemy(generatePos);
            enemysHolder.AddEnemy(enemyGenerator.GenerateTestEnemy(generatePos));
        }

        public void RemoveEnemy(IEnemyComponent enemyComponent)
        {
            enemyComponent.Disabled();
            enemysHolder.RemoveEnemy(enemyComponent);
        }
    }
}
