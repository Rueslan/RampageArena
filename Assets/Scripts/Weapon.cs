using Assets.Scripts.CFXR;
using Assets.Scripts.Common;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected float _flightSpeed = 2;
        [SerializeField] protected float _rotationSpeed = 1;
        [SerializeField] protected float _lifeTime = 3;
        [SerializeField] public float _damage = 3;
        [SerializeField] protected float _thrust = 10;
        [SerializeField] protected float _finalThrust = 70;

        protected Vector3 direction;

        public GameObject owner;

        private BloodSplashHandler _bloodSplashHandler;

        protected void Start()
        {
            _bloodSplashHandler = GetComponent<BloodSplashHandler>();
            direction = transform.parent.forward;
            transform.parent = null;
            Destroy(gameObject, _lifeTime);
        }

        protected void FixedUpdate()
        {
            FlightTransformation();
        }

        protected virtual void FlightTransformation()
        {
            transform.position = transform.position + direction * Time.deltaTime * _flightSpeed;
            transform.Rotate(-_rotationSpeed, 0, 0);
        }

        protected void OnCollisionEnter(Collision collision)
        {
            switch (collision.transform.tag)
            {
                case "Wood":
                    SoundManager.instance.PlaySound(SoundManager.audioClip.WoodHit);
                    break;
                case "Rock":
                    SoundManager.instance.PlaySound(SoundManager.audioClip.RockHit);
                    break;
                case "Bush":
                    SoundManager.instance.PlaySound(SoundManager.audioClip.WoodHit);
                    break;
                default:
                    break;
            }

            if (collision.transform.parent.TryGetComponent(out IHealth health))
            {
                health.ApplyDamage(_damage);
                HandleCollision(collision, health);
            }

            Destroy(gameObject);
        }

        private void HandleCollision(Collision collision, IHealth health)
        {
            if (health.CurrentHealth <= 0)
            {
                collision.transform.parent.GetComponent<RagdollHandler>()?.DeathHit(direction * _finalThrust, transform.position);
                EventManager.CallEnemyDead(owner, collision.gameObject);
            }
            else
            {
                collision.rigidbody?.AddForce(direction * _thrust, ForceMode.Impulse);
                _bloodSplashHandler.SplashBlood(collision.gameObject.transform.position, direction);
            }
        }
    }
}
