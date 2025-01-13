using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject victoryPanel;

    [HideInInspector] public bool _gameFinished = false;

    public Volume volume;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(this);
    }

    public void Victory()
    {
        victoryPanel.SetActive(true);
        _gameFinished = true;
    }

   

}
