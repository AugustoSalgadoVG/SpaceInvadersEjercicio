using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float _objectHeight;
    private float _objectWidth;
    private Vector3 _enemySpawnPoint;
    public GameObject enemyPrefab;
    private int _totalRowsToSpawn = 4;

    void Start()
    {
        SetObjectProperties();
        SetStartingEnemySpawnPoint();
        SpawnStartingEnemies();
    }
    
    void SetStartingEnemySpawnPoint()
    {
        _enemySpawnPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, Camera.main.nearClipPlane));
        _enemySpawnPoint.x = _enemySpawnPoint.x + _objectWidth / 2;
        _enemySpawnPoint.y = _enemySpawnPoint.y - _objectHeight / 2;
    }

    void SetObjectProperties()
    {
        _objectHeight = enemyPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        _objectWidth = enemyPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void SpawnStartingEnemies()
    {
        bool isEnoughSpaceInTheRowToSpawn = true;
        int rowsSpawned = 0;

        while (rowsSpawned < _totalRowsToSpawn)
        {
            Debug.Log("Width: " + _objectWidth);
            Debug.Log("Width de la screen: " + Screen.width);
            Debug.Log("Punto siguiente: " + _enemySpawnPoint);
            Debug.Log("Hay lugar: " + isEnoughSpaceInTheRowToSpawn);
            Instantiate(enemyPrefab, _enemySpawnPoint, Quaternion.identity);
            isEnoughSpaceInTheRowToSpawn = CheckSpaceLeftOnScreen();
            SetNextSpawnPoint(rowsSpawned, isEnoughSpaceInTheRowToSpawn);
            if (!isEnoughSpaceInTheRowToSpawn)
            {
                rowsSpawned += 1;
            }
        }
        
    }

    void SetNextSpawnPoint(int rowsSpawned, bool isEnoughSpaceInTheRowToSpawn)
    {
        if (isEnoughSpaceInTheRowToSpawn)
        {
            _enemySpawnPoint.x += _objectWidth;
        }
        else
        {
            _enemySpawnPoint.y -= _objectHeight;
            _enemySpawnPoint.x = 0;
        }
    }
    bool CheckSpaceLeftOnScreen()
    {
        bool isSpaceEnough;
        float width;
        Vector2 width2 = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.width, 0));
        width = width2.y;
        

        if (_enemySpawnPoint.x + _objectWidth >= width)
        {
            isSpaceEnough = true;
        }
        else
        {
            isSpaceEnough = false;
        }
        
        return isSpaceEnough;
    }
}
