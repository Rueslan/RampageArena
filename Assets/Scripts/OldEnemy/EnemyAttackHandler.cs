using Assets.Scripts.Abstract;
using Assets.Scripts.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.OldEnemy
{
    public class EnemyAttackHandler : AttackHandlerBase
    {
        private NavMeshAgent _navMeshAgent;
        private RaycastHit hit;
        private readonly float raycastDistance = 20f;
        public EnemyAnimator Animator;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (Physics.Raycast(transform.position + Vector3.up * 2, transform.forward, out hit, raycastDistance))
            {
                if (hit.collider.TryGetComponent(out PlayerBase _))
                {
                    Attack();
                }
            }
        }

    }
}
