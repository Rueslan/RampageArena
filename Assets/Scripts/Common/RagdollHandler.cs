using Assets.Scripts.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    public PlayerDeath playerDeath;
    private List<Rigidbody> _rigitbodies;
    private List<Collider> _colliders;

    private void Awake()
    {
        _rigitbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        _colliders = new List<Collider>(GetComponentsInChildren<Collider>());
        _rigitbodies.RemoveAt(0);
        _colliders.RemoveAt(0);
    }

    private void Start() =>
        playerDeath.PlayerDead += OnPlayerDead;

    private void OnDestroy() =>
        playerDeath.PlayerDead -= OnPlayerDead;

    private void OnPlayerDead() =>
        SetHipsActive(true);

    public void SetHipsActive(bool value)
    {
        foreach (Rigidbody rigidbody in _rigitbodies)
            rigidbody.isKinematic = !value;

        foreach (Collider collider in _colliders)
            collider.enabled = value;
    }

    public void DeathHit(Vector3 force, Vector3 hitPosition) => 
        _rigitbodies.OrderBy(r => Vector3.Distance(r.position, hitPosition)).First().AddForceAtPosition(force, hitPosition, ForceMode.Impulse);
}
