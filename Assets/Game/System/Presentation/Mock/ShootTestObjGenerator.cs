using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootTestObjGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform[] _generatePositions;
    
    [SerializeField]
    private ShootTestObj _shootTestObjPrefab;

    [SerializeField]
    private float _generateInterval = 1f;

    // コルーチンを保持しておくための変数
    private Coroutine _generateCoroutine;

    private void Start()
    {
        // 後から終了できるようにコルーチンを保持しておく
        _generateCoroutine = StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        if (_generatePositions.Length == 0)
        {
            yield break;
        }

        while (true)
        {
            yield return new WaitForSeconds(_generateInterval);

            var generatePos = _generatePositions[Random.Range(0, _generatePositions.Length)];

            if (generatePos == null)
            {
                yield break;
            }

            ShootTestObj shootTestObj = Instantiate(
                _shootTestObjPrefab, 
                generatePos.position,
                Quaternion.identity);

            //shootTestObj.transform.SetParent();

            shootTestObj.Initialized();
        }
    }

    // オブジェクトが破棄されたらコルーチンも終了させる
    private void OnDisable()
    {
        StopCoroutine(_generateCoroutine);
    }
}
