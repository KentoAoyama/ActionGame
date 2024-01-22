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

    // �R���[�`����ێ����Ă������߂̕ϐ�
    private Coroutine _generateCoroutine;

    private void Start()
    {
        // �ォ��I���ł���悤�ɃR���[�`����ێ����Ă���
        _generateCoroutine = StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        while (true)
        {
            yield return new WaitForSeconds(_generateInterval);

            ShootTestObj shootTestObj = Instantiate(
                _shootTestObjPrefab, 
                _generatePositions[Random.Range(0, _generatePositions.Length)].position,
                Quaternion.identity);

            shootTestObj.transform.SetParent(transform);

            shootTestObj.Initialized();
        }
    }

    // �I�u�W�F�N�g���j�����ꂽ��R���[�`�����I��������
    private void OnDisable()
    {
        StopCoroutine(_generateCoroutine);
    }
}
