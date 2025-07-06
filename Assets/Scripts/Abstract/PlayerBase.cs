using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Abstract
{
    public abstract class PlayerBase : MonoBehaviour
    {
        public string NickName = string.Empty;

        [SerializeField] protected float _deathTimeOffset = 3f;
        [SerializeField] protected GameObject _DeathSmokeEffect;

        protected CapsuleCollider _capsuleCollider;
        protected RagdollHandler _ragdollHandler;
        protected Animator _animator;

        protected virtual void Awake()
        {
            _ragdollHandler = GetComponent<RagdollHandler>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _animator = GetComponent<Animator>();
        }

        //protected void Start() => 
        //    PlayersList.AddPlayer(gameObject);

        //internal virtual void Death()
        //{
        //    PlayersList.RemovePlayer(gameObject);
        //    _animator.enabled = false;
        //    _capsuleCollider.enabled = false;
        //    _ragdollHandler.SetHipsActive(true);
        //    StartCoroutine(DeathCoroutine());
        //    Destroy(gameObject, _deathTimeOffset);
        //}

        //private IEnumerator DeathCoroutine()
        //{
        //    yield return new WaitForSeconds(_deathTimeOffset - 0.1f);
        //    var pos = transform.GetChild(0).transform.position;
        //    Instantiate(_DeathSmokeEffect, new Vector3(pos.x, 1.5f, pos.z), Quaternion.identity);
        //}
    }
}
