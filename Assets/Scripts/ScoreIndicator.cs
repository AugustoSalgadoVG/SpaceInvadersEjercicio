using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreIndicator : MonoBehaviour
{
    private Text _scoreText;
    private void Start()
    {
        _scoreText = gameObject.GetComponent<Text>();
    }
    
    public void RefreshScoreIndicator(int newScore)
    {
        _scoreText.text = newScore.ToString();
    }
}
