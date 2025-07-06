using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Interactable
{
    public class Healpack : MonoBehaviour
    {
        [SerializeField] private int _healAmount = 20;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private GameObject _model;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth health))
            {
                _audioSource.Play();
                health.ApplyHeal(_healAmount);

                Destroy(_model.gameObject);
                Destroy(gameObject, 1f);
            }
        }
    }
}
