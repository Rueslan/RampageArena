using Assets.Scripts.Interfaces;
using Assets.Scripts.Player;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private GameObject[] _waterSplashEffects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out IHealth health))
        {
            health.CurrentHealth = 0;
            foreach (var effect in _waterSplashEffects)
            {
                Instantiate(effect, other.transform.position, Quaternion.identity);
            }

        }
    }

}
