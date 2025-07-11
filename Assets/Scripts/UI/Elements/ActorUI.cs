using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        public HPBar HPBar;

        private IHealth _health;

        private void OnDestroy()
        {
            if (_health is not null)
                _health.HealthChanged -= UpdateHPBar;
        }        

        public void Construct(IHealth health)
        {
            _health = health;

            _health.HealthChanged += UpdateHPBar;
        }

        public void UpdateHPBar(float value) =>
            HPBar.SetValue(_health.CurrentHealth, _health.MaxHealth);
    }
}
