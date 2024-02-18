using Domain;
using System.Collections.Generic;

namespace Application
{
    /// <summary>
    /// 敵オブジェクトへの参照を保持するクラス
    /// </summary>
    public class EnemysHolder
    {
        private List<IEnemyComponent> _enemyComponents = new();

        public EnemysHolder(List<IEnemyComponent> enemyComponents)
        {
            // IEnmyComponentを継承したクラスを全て取得
            _enemyComponents = enemyComponents;
        }

        public void AddEnemy(IEnemyComponent enemyComponent)
        {
            _enemyComponents.Add(enemyComponent);
        }

        public void RemoveEnemy(IEnemyComponent enemyComponent)
        {
            _enemyComponents.Remove(enemyComponent);
        }
    }
}
