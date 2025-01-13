using UnityEngine;

namespace Assets.Scripts.Infrastructure.States
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper BootstrapperPrefab;
        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();
            if (bootstrapper == null)
                Instantiate(BootstrapperPrefab);
        }

    }
}
