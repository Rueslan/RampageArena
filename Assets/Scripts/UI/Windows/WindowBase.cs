using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public Button CloseButton;

        protected IPersistentProgressService ProgressService;
        protected PlayerProgress Progress => ProgressService.PlayerProgress;

        public void Construct(IPersistentProgressService progressService) => 
            ProgressService = progressService;

        private void Awake() => 
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        protected virtual void OnAwake() => 
            CloseButton.onClick.AddListener(() => Destroy(gameObject));

        private void OnDestroy() => 
            Cleanup();

        protected virtual void Initialize()
        {

        }

        protected virtual void SubscribeUpdates()
        {

        }

        protected virtual void Cleanup()
        {

        }
    }
}
