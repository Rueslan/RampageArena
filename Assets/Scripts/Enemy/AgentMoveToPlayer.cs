using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        public NavMeshAgent Agent;
        private Transform _playerTransform;

        public void Construct(Transform playerTransform) =>
            _playerTransform = playerTransform;

        private void Update() => 
            SetDestenationForAgent();

        private void SetDestenationForAgent()
        {
            if (_playerTransform && Agent.isActiveAndEnabled)
                Agent.destination = _playerTransform.position;
        }
    }
}
