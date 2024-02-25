using Domain;
using Presentation;
using System.Collections;
using UnityEngine;
using Zenject;


public class ShootTestObjGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform[] _generatePositions;

    [SerializeField]
    private float _generateInterval = 1f;

    private IEnemysCompenent _enemysController;

    // コルーチンを保持しておくための変数
    private Coroutine _generateCoroutine;

    private void Start()
    {
        _enemysController = FindObjectOfType<EnemysController>();
    }

    public void GenerateAll()
    {
        // 生成位置の数だけ生成する
        for (int i = 0; i < _generatePositions.Length; i++)
        {
            if (_generatePositions[i] == null)
            {
                continue;
            }

            if (_generatePositions[i].childCount > 0)
            {
                continue;
            }

            _enemysController.GenerateTestEnemy(_generatePositions[i]);
        }
    }

    public void StartRandomGenerate()
    {
        // 後から終了できるようにコルーチンを保持しておく
        _generateCoroutine = StartCoroutine(GenerateCoroutine());
    }

    public void StopRandomGenerate()
    {
        StopCoroutine(_generateCoroutine);
    }

    // 全てのShootTestObjを削除する
    public void DestroyAll()
    {
        for (int i = 0; i < _generatePositions.Length; i++)
        {
            if (_generatePositions[i] == null)
            {
                continue;
            }

            if (_generatePositions[i].childCount == 0)
            {
                continue;
            }

            DestroyEnemy(_generatePositions[i].GetChild(0).gameObject);
        }
    }

    private void DestroyEnemy(GameObject enemyObj)
    {
        IEnemyComponent enemyComponent = enemyObj.GetComponent<IEnemyComponent>();

        _enemysController.RemoveEnemy(enemyComponent);
        Destroy(enemyObj);
    }

    private IEnumerator GenerateCoroutine()
    {
        if (_generatePositions.Length == 0)
        {
            yield break;
        }

        while (true)
        {
            yield return new WaitForSeconds(_generateInterval);

            RandomGenerate();
        }
    }
    private void RandomGenerate()
    {
        Transform generatePos = null;

        for (int i = 0; i < _generatePositions.Length; i++)
        {
            generatePos = _generatePositions[i];

            if (generatePos == null)
            {
                continue;
            }

            if (generatePos.childCount > 0)
            {
                generatePos = null;
                continue;
            }

            break;
        }     

        if (generatePos == null)
        {
            return;
        }

        _enemysController.GenerateTestEnemy(generatePos);
    }

    // オブジェクトが破棄されたらコルーチンも終了させる
    private void OnDisable()
    {
        if (_generateCoroutine != null)
            StopCoroutine(_generateCoroutine);
    }
}
