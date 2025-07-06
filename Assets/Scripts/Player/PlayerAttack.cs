using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using System.Collections;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using Assets.Scripts.Services.Input;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerAttack : MonoBehaviour, ISavedProgressReader
    {
        public PlayerAnimator animator;
        private IInputService _input;

        public Weapon weapon;

        public float fireRate = 1;
        private bool allowAttack = true;

        private readonly float _offsetY = 1.2f;
        private readonly float _offsetZ = 1.5f;

        //private static int _layerMask;

        private Stats _stats;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();

            //_layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_input.isAttackButtonUp() && !animator.IsAttacking)
                animator.PlayAttack();

            //if (Input.GetKeyDown(KeyCode.Space))
            //    Attack();
        }

        public void OnAttack()
        {
            if (allowAttack && weapon is not null)
                StartCoroutine(AttackCoroutine());
        }

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, transform.position.y + _offsetY, transform.position.z) + transform.forward * _offsetZ;

        private IEnumerator AttackCoroutine()
        {
            allowAttack = false;
            PlayAttackSound();
            CreateWeapon();

            yield return new WaitForSeconds(fireRate);
            allowAttack = true;
        }

        private static void PlayAttackSound() =>
            SoundManager.instance.PlaySound(SoundManager.audioClip.Throw);

        private void CreateWeapon() =>
            Instantiate(weapon.With(w=> w._damage = _stats.Damage), StartPoint(), Quaternion.Euler(new Vector3(0, 0, 90)), transform)
                            .With(w => w.transform.localScale = Vector3.one * 3)
                            .With(w => w.owner = transform.gameObject);

        public void LoadProgress(PlayerProgress progress) =>
            _stats = progress.PlayerStats;

    }
}
