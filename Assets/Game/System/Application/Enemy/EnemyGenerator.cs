using Domain;
using UnityEngine;

namespace Application
{
    /// <summary>
    /// 敵の生成を行うクラス
    /// 多分実際のシーンではシーンに直接配置するため使わない
    /// </summary>
    [System.Serializable]
    public class EnemyGenerator
    {
        [Header("敵のprefab")]
        [SerializeField]
        private GameObject _enemyPrefab;

        [Header("テスト用のprefab")]
        [SerializeField]
        private GameObject _testEnemyPrefab;

        public IEnemyComponent GenerateEnemy(Transform generatePos)
        {
            return InstantiateEnemy(_enemyPrefab, generatePos);
        } 

        public IEnemyComponent GenerateTestEnemy(Transform generatePos)
        {
            return InstantiateEnemy(_testEnemyPrefab, generatePos);
        }

        private IEnemyComponent InstantiateEnemy(
            GameObject enemyPrefab,
            Transform generatePos)
        {
            var obj = GameObject.Instantiate(
                enemyPrefab,
                generatePos.position,
                Quaternion.identity);
            IEnemyComponent enemyComponent = obj.GetComponent<IEnemyComponent>();
                

            obj.transform.SetParent(generatePos);
            enemyComponent.Initialized();

            return enemyComponent;
        }
    }
}
