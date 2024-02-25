using System.Collections.Generic;
using UnityEngine;
using Domain;
using Application;

namespace Presentation
{
    public class EnemysController : MonoBehaviour, IEnemysCompenent
    {
        [SerializeField]
        private EnemyGenerator _enemyGenerator;

        private EnemysHolder _enemysHolder;

        public void Initialized()
        {
            // “G‚ğŒŸõ‚µA‘S‚Äæ“¾‚·‚é
            var enemys = FindObjectsByType<ShootTestObj>(FindObjectsSortMode.None);

            List<IEnemyComponent> enemyComponents = new();
            foreach (var enemy in enemys)
            {
                enemyComponents.Add(enemy);
            }

            _enemysHolder = new EnemysHolder(enemyComponents);
        }

        public void GenerateEnemy(Transform generatePos)
        {
            IEnemyComponent enemyComponent =@_enemyGenerator.GenerateEnemy(generatePos);
            _enemysHolder.AddEnemy(enemyComponent);
            
        }

        public void GenerateTestEnemy(Transform generatePos)
        {
            IEnemyComponent enemyComponent = _enemyGenerator.GenerateTestEnemy(generatePos);
            _enemysHolder.AddEnemy(enemyComponent);
        }

        public void RemoveEnemy(IEnemyComponent enemyComponent)
        {
            enemyComponent.Disabled();
            _enemysHolder.RemoveEnemy(enemyComponent);
        }

        public void RemoveAllEnemy()
        {
            foreach (var enemy in _enemysHolder.GetEnemyComponents())
            {
                enemy.Disabled();
                _enemysHolder.RemoveEnemy(enemy);
            }
        }
    }
}
