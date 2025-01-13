using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float AgreRadius = 40f;

    //[SerializeField] private PlayerAnimator _playerAnimationHandler;
    [SerializeField] private NavMeshAgent _agent;

    private Transform _target;

    void Start()
    {
        ChangeWay();
        StartCoroutine(SetTarget());
    }

    private void FixedUpdate()
    {
        //if (_agent.velocity != Vector3.zero)
        //    _playerAnimationHandler.isMoving = true;
        //else
        //    _playerAnimationHandler.isMoving = false;
    }

    private IEnumerator SetTarget()
    {
        while (enabled)
        {
            if (_target == null)
            {
                if (PlayersList._playersOnline.Count >= 2)
                {
                    _target = GetClosestEnemy(PlayersList._playersOnline.Where(x => x != gameObject).ToList()).transform;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, _target.position) <= AgreRadius)
                {
                    _agent.SetDestination(_target.position);
                }
                else
                {
                    ChangeWay();
                    _target = null;
                }
                if (_agent.velocity == Vector3.zero)
                {
                    ChangeWay();
                }
            }

            yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        }
    }

    private GameObject GetClosestEnemy(List<GameObject> enemies)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    internal void ChangeWay()
    {
        RandomNavmeshLocation(30);
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

}
