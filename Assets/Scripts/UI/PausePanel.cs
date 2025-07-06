using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.UI
{
    public class PausePanel : MonoBehaviour
    {
        private Player.Player _player;
        private bool gamePaused;
        private SceneRenderer _sceneRenderer;

        [Inject]
        public void Construct(Player.Player player, SceneRenderer sceneRenderer)
        {
            _player = player;
            _sceneRenderer = sceneRenderer;
        }

        private void Awake()
        {
            EventManager.PlayerDead.AddListener(GameOver);
            gameObject.SetActive(false);
        }

        public void Restart()
        {
            foreach (GameObject go in PlayersList._playersOnline.Skip(1))
            {
                Destroy(go);
            }
            PlayersList._playersOnline.Clear();
            ResumeGame();
            _player.Restore();
            _sceneRenderer.DisableVignette();
            GameController.instance._gameFinished = false;
        }

        public void Exit()
        {
            SoundManager.instance.StopSound(SoundManager.audioClip.Heartbeat);
            SceneManager.LoadScene(0);
        }

        public void PauseToggle()
        {
            if (gamePaused)
                ResumeGame();
            else
                PauseGame();
        }

        public void PauseGame()
        {
            EventManager.CallGamePaused();
            gameObject.SetActive(true);
            Time.timeScale = 0;
            gamePaused = true;
        }

        public void ResumeGame()
        {
            EventManager.CallGameResumed();
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            gamePaused = false;
        }

        public void GameOver()
        {
            gameObject.SetActive(true);
            //Time.timeScale = 0;
            GameController.instance._gameFinished = true;
        }
    }
}
