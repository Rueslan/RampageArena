using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public class ForTests : MonoBehaviour
    {
        public Player.Player player;
        private Transform _playerTransform;

        public void Construct(Transform playerTransform) =>
            _playerTransform = playerTransform;

        private void Update()
        {
            if (_playerTransform)
            {
                if (Input.GetKeyDown(KeyCode.H))
                    _playerTransform.GetComponent<IHealth>().ApplyHeal(20);

                if (Input.GetKeyDown(KeyCode.K))
                    _playerTransform.GetComponent<IHealth>().ApplyDamage(20);

            }
        }

    }
}
