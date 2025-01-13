using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private readonly ICoroutineRunner _coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner) => 
        _coroutineRunner = coroutineRunner;

    public void Load(string name, Action OnLoaded = null) =>
        _coroutineRunner.StartCoroutine(LoadScene(name, OnLoaded));

    private IEnumerator LoadScene(string name, Action OnLoaded = null)
    {
        if (SceneManager.GetActiveScene().name == name)
        {
            OnLoaded?.Invoke();
            yield break;
        }

        AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);

        while (!waitNextScene.isDone)
        {
            yield return null;
        }

        OnLoaded?.Invoke();
    }
}