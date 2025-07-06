using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class Aggro : MonoBehaviour
    {
        public TriggerObserver TriggerObserver;
        public Follow Follow;

        public float Cooldown;

        private Coroutine _aggroCoroutiune;

        private bool _hasAggroTarget;

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }

        private void TriggerExit(Collider collider)
        {
            if (_hasAggroTarget)
            {
                _hasAggroTarget = false;
                _aggroCoroutiune = StartCoroutine(SwitchFollowOffAfterCooldown());
            }
        }

        private void TriggerEnter(Collider collider)
        {
            if (!_hasAggroTarget)
            {
                _hasAggroTarget = true;
                StopAggroCoroutine();
                SwitchFollowOn();
            }
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(Cooldown);
            SwitchFollowOff();
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutiune != null)
            {
                StopCoroutine(_aggroCoroutiune);
                _aggroCoroutiune = null;
            }
        }

        private void SwitchFollowOn() =>
            Follow.enabled = true;

        private void SwitchFollowOff() =>
            Follow.enabled = false;
    }
}
