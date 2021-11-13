using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Defeat : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverCanvas;
    [SerializeField]
    Text _scoreText;

    public void GameOver(int score)
    {
        gameOverCanvas.SetActive(true);
        _scoreText.text = score.ToString();
    }
}
