using UnityEngine;

public class EnemyTargetArea : MonoBehaviour
{
    private void Update()
    {
        // í‚ÉƒJƒƒ‰‚Ì•ûŒü‚ÉŒü‚¯‚é
        transform.LookAt(Camera.main.transform);
    }
}