using UnityEngine;

public class EnemyTargetArea : MonoBehaviour
{
    private void Update()
    {
        // ��ɃJ�����̕����Ɍ�����
        transform.LookAt(Camera.main.transform);
    }
}