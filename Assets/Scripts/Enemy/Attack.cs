using Assets.Scripts.Interfaces;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        public EnemyAnimator Animator;

        public float AttackCooldown = 3f;
        public float Cleavage = 0.5f;
        public float EffectiveDistance = 3f;
        public float Damage = 10f;

        private Transform _playerTransform;
        private float _attackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive = false;

        public void Construct(Transform playerTransform) =>
            _playerTransform = playerTransform;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1);
                hit.transform.parent.GetComponent<IHealth>().ApplyDamage(Damage);
            }
        }

        public void DisableAttack() =>
            _attackIsActive = false;

        public void EnableAttack() =>
            _attackIsActive = true;

        private bool Hit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitsCount > 0;
        }

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * EffectiveDistance;

        private void OnEndAttack()
        {
            _attackCooldown = AttackCooldown;
            _isAttacking = false;
        }

        private void StartAttack()
        {
            transform.LookAt(_playerTransform.position);
            Animator.PlayAttack();
            _isAttacking = true;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackCooldown -= Time.deltaTime;
        }

        private bool CanAttack() =>
            _attackIsActive && !_isAttacking && CooldownIsUp() && _playerTransform != null;

        private bool CooldownIsUp() =>
            _attackCooldown <= 0;
    }

}
