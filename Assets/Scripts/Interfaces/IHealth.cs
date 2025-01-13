using System;

namespace Assets.Scripts.Interfaces
{
    public interface IHealth
    {
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }

        public event Action<float> HealthChanged;
        public void ApplyDamage(float value);
        public void ApplyHeal(float value);
    }
}
