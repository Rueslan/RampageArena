using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private LevelLoader levelLoader;

    private void Awake()
    {
        levelLoader = GetComponent<LevelLoader>();
    }

    public void PlayGame()
    {
        PlayersList._playersOnline.Clear();
        levelLoader.LoadLevel(1);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
