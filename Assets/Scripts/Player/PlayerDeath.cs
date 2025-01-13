using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerHealth), typeof(PlayerMovement), typeof(PlayerAttack))]
    [RequireComponent(typeof(RagdollHandler), typeof(CapsuleCollider), typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerAnimator), typeof(Animator))]
    public class PlayerDeath : MonoBehaviour
    {
        public PlayerHealth Health;
        public PlayerMovement Movement;
        public PlayerAttack Attack;
        public GameObject DeathFX;
        public CapsuleCollider CapsuleCollider;
        public CapsuleCollider HitBox;
        public Rigidbody Rigidbody;
        public Animator Animator;
        public PlayerAnimator PlayerAnimator;

        public event Action PlayerDead;

        private bool _isDead;
        private float _deathTimeOffset = 3f;

        private void Start() => 
            Health.HealthChanged += HealthChanged;

        private void OnDestroy() => 
            Health.HealthChanged -= HealthChanged;

        private void HealthChanged(float damageValue)
        {
            if (!_isDead && Health.CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;

            PlayersList.RemovePlayer(gameObject);
            PlayDeathSound();
            PlayerDead?.Invoke();
            SetControlsActive(false);
            StartCoroutine(DeathCoroutine());
            Destroy(gameObject, _deathTimeOffset);
        }

        private void PlayDeathSound() =>
            SoundManager.instance.PlaySound(SoundManager.audioClip.Death);

        private void SetControlsActive(bool value)
        {
            Animator.enabled = value;
            CapsuleCollider.enabled = value;
            PlayerAnimator.enabled = value;
            Movement.enabled = value;
            Attack.enabled = value;
            HitBox.enabled = value;
            HitBox.gameObject.SetActive(value);
            Rigidbody.useGravity = value;
        }

        private IEnumerator DeathCoroutine()
        {
            yield return new WaitForSeconds(_deathTimeOffset - 0.1f);
            var pos = transform.GetChild(0).transform.position;
            Instantiate(DeathFX, new Vector3(pos.x, 2f, pos.z), Quaternion.identity);
            //EventManager.CallPlayerDead();
        }
    }
}
