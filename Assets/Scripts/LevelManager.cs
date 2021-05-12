using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject pauseUI;
    public static int totalEnemyMovesToSides = 4;
    public bool isGamePaused = false;
    public int score;
    private int _enemiesDestroyedInRound = 0;
    private EnemySpawner _enemySpawner;
    private EnemyShipDestructionManager _enemyShipDestructionManager;
    private EnemyMoveManager _enemyMoveManager;
    private ScoreIndicator _scoreCounter;

    private void Start()
    {
        SetProperties();
        StartGame();
    }
    void SetProperties()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
        _enemyShipDestructionManager = FindObjectOfType<EnemyShipDestructionManager>();
        _enemyMoveManager = FindObjectOfType<EnemyMoveManager>();
        _scoreCounter = FindObjectOfType<ScoreIndicator>();
    }
    void StartGame()
    {
        _enemySpawner.SpawnEnemies();

    }
    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }
    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        isGamePaused = false;
        Time.timeScale = 1;
    }
    public void EndGame()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    public void EnemyShipWasDestroyed()
    {
        UpdateScore();
        CheckIfEnemyRespawnIsNeeded();
    }
    void UpdateScore()
    {
        score += 8;
        _enemiesDestroyedInRound++;
        _scoreCounter.RefreshScoreIndicator(score);
    }
    void CheckIfEnemyRespawnIsNeeded()
    {
        if (_enemiesDestroyedInRound == _enemySpawner.totalEnemiesSpawned)
        {
            _enemiesDestroyedInRound = 0;
            RespawnEnemies();
        }
    }
    private void RespawnEnemies()
    {
        _enemySpawner.SpawnEnemies();
        _enemyMoveManager.RestartMovement();
    }

}
