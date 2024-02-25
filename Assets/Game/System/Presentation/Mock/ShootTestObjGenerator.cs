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

    // �R���[�`����ێ����Ă������߂̕ϐ�
    private Coroutine _generateCoroutine;

    private void Start()
    {
        _enemysController = FindObjectOfType<EnemysController>();
    }

    public void GenerateAll()
    {
        // �����ʒu�̐�������������
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
        // �ォ��I���ł���悤�ɃR���[�`����ێ����Ă���
        _generateCoroutine = StartCoroutine(GenerateCoroutine());
    }

    public void StopRandomGenerate()
    {
        StopCoroutine(_generateCoroutine);
    }

    // �S�Ă�ShootTestObj���폜����
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

    // �I�u�W�F�N�g���j�����ꂽ��R���[�`�����I��������
    private void OnDisable()
    {
        if (_generateCoroutine != null)
            StopCoroutine(_generateCoroutine);
    }
}
