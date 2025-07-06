using Assets.Scripts.Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.OldEnemy
{
    public class Enemy : PlayerBase
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private EnemyAttackHandler _enemyAttackHandler;

        public void Death()
        {
            DisableEnemy();
            //base.Death();
        }

        private void DisableEnemy()
        {
            _agent.enabled = false;
            _enemyMovement.enabled = false;
            _enemyAttackHandler.enabled = false;
        }
    }
}
