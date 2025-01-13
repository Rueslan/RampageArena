using TMPro;
using UnityEngine;
using Zenject;

public class ScoreHandler : MonoBehaviour
{
    private TMP_Text _scoreText;
    private int _score = 0;

    public void Construct(TMP_Text scoreText)
    {
        _scoreText = scoreText;
    }

    private void Awake()
    {
        //EventManager.EnemyDead.AddListener(AddScore);
    }

    private void AddScore(GameObject killer, GameObject victum)
    {
        if (killer == gameObject)
        {
            _scoreText.text = $"{++_score}";
        }        
    }
}
