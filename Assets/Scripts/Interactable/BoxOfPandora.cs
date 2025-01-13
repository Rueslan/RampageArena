using UnityEngine;

public class BoxOfPandora : MonoBehaviour
{
    [SerializeField] private GameObject _destroyParticles;
    [SerializeField] private float _destroyDelay = 2f;
    [SerializeField] private AudioSource _audioSource;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BuffSpeed buffSpeed))
        {
            _audioSource.Play();
            buffSpeed.IncreaseFireRate();
            buffSpeed.IncreaseSpeed();
            _destroyParticles.SetActive(true);
            GetComponent<MeshRenderer>().enabled = false;
            _animator.SetTrigger("Bang");
            Destroy(transform.parent.gameObject, _destroyDelay);
        }
    }
}
