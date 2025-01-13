using Assets.Scripts.Interfaces;
using Assets.Scripts.UI;
using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    public EnemyAnimator animator;

    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                ChangeHealthBarFiller();
            }
        }
    }

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public event Action<float> HealthChanged;

    private void Start() => 
        GetComponent<ActorUI>().Construct(this);

    public void ApplyHeal(float healValue) =>
        CurrentHealth = Mathf.Min(CurrentHealth + healValue, MaxHealth);

    public void ApplyDamage(float damageValue)
    {
        if (damageValue < 0) return;

        CurrentHealth -= damageValue;

        PlayHitSound();

        if (CurrentHealth <= 0)
            Death();
        else
            animator.PlayHit();
    }

    private void Death() =>
        HealthChanged?.Invoke(0);

    private void ChangeHealthBarFiller() => 
        HealthChanged?.Invoke(currentHealth / maxHealth);

    private void PlayHitSound() =>
        SoundManager.instance.PlaySound(SoundManager.audioClip.BodyHit);
}
