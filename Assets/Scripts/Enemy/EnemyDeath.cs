using Assets.Scripts.UI;
using Scripts.Enemy;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyHealth health;
        public EnemyAnimator animator;
        public NavMeshAgent agent;
        public AgentMoveToPlayer follow;
        public HPBar HPbar;

        public event Action OnDeath;

        public GameObject DeathFx;
        public int destroyTime;

        private void Start() => 
            health.HealthChanged += HealthChanged;

        private void OnDestroy() => 
            health.HealthChanged -= HealthChanged;

        private void HealthChanged(float obj)
        {
            if (health.CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            health.HealthChanged -= HealthChanged;
            follow.enabled = false;
            HPbar.gameObject.SetActive(false);
            agent.enabled = false;
            animator.PlayDeath();
            
            StartCoroutine(DestroyTimer());

            OnDeath?.Invoke();
        }

        private void PlayDeathFx() => 
            Instantiate(DeathFx, transform.position, Quaternion.identity);

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(destroyTime);
            PlayDeathFx();
            Destroy(gameObject);
        }
    }
}
