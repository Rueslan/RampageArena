using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyMovement enemyMovement))
        {
            enemyMovement.ChangeWay();
        }
    }
}
