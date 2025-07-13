using Assets.Scripts.Infrastructure.States;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        private const string PlayerTag = "Player";
        public string TransferTo;

        private IGameStateMachine _stateMachine;
        private bool triggered;

        public void Construct(IGameStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        private void OnTriggerEnter(Collider other)
        {
            if (triggered)
                return;
            triggered = true;

            if (other.CompareTag(PlayerTag))
                _stateMachine.Enter<LoadLevelState, string>(TransferTo);
        }

    }
}
