using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Interfaces;
using System;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerHealth : MonoBehaviour, IHealth, ISavedProgress
    {
        [SerializeField] private float currentHealth;
        [SerializeField] private float maxHealth;

        public PlayerAnimator Animator;

        public event Action<float> HealthChanged;

        private State _state;

        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float CurrentHealth
        {
            get => _state.CurrentHP;
            set
            {
                if (_state.CurrentHP != value)
                {
                    _state.CurrentHP = value;
                    ChangeHealthBarFiller();
                }
            }
        }

        private void Awake() =>
            EventManager.PlayerRestore.AddListener(Restore);

        public void ApplyDamage(float damageValue)
        {
            if (damageValue <= 0) return;

            CurrentHealth -= damageValue;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Death();
                return;
            }

            if (CurrentHealth <= 20 && CurrentHealth > 0)
                PlayHeartbeatSound();

            Animator.PlayHit();
            PlayHitSound();
        }

        public void ApplyHeal(float healValue)
        {
            CurrentHealth = Mathf.Min(CurrentHealth + healValue, MaxHealth);

            if (CurrentHealth > 20)
                StopHeartbeatSound();
        }

        private void Restore()
        {
            CurrentHealth = MaxHealth;
            StopHeartbeatSound();
        }

        private void Death()
        {
            HealthChanged?.Invoke(0);
            StopHeartbeatSound();
        }

        private void ChangeHealthBarFiller() =>
            HealthChanged?.Invoke(currentHealth / maxHealth);

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.PlayerState.CurrentHP = CurrentHealth;
            progress.PlayerState.MaxHP = MaxHealth;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.PlayerState;
            CurrentHealth = _state.CurrentHP;
        }

        private void PlayHitSound() =>
            SoundManager.instance.PlaySound(SoundManager.audioClip.BodyHit);

        private void PlayHeartbeatSound() =>
            SoundManager.instance.PlaySound(SoundManager.audioClip.Heartbeat);

        private void StopHeartbeatSound() =>
            SoundManager.instance.StopSound(SoundManager.audioClip.Heartbeat);
    }
}
