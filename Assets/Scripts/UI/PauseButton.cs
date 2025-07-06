using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class PauseButton : MonoBehaviour
    {
        private PausePanel _pausePanel;

        [Inject]
        public void Construct(PausePanel pausePanel)
        {
            _pausePanel = pausePanel;
        }

        public void ActivatePausePanel()
        {
            _pausePanel.PauseToggle();
        }
    }
}
