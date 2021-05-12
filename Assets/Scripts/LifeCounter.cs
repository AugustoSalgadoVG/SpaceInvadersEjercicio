using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    public GameObject lifeIndicatorPrefab;
    public LevelManager levelManager;
    private int _originalLifes = 3;
    private int _lifesLeft;
    private float _lifeIndicatorWidth;
    private Vector2 _spawnPoint;
    private LifeIndicator[] _lifeIndicators;

    private void Start()
    {
        _lifeIndicatorWidth = lifeIndicatorPrefab.GetComponent<SpriteRenderer>().bounds.extents.x;
        _spawnPoint = Camera.main.ScreenToWorldPoint(new Vector3(10,4,Camera.main.nearClipPlane));
        SpawnLifeIndicators();
        GetAllIndicators();
        _lifesLeft = _originalLifes;
    }

    void SpawnLifeIndicators()
    {
        for (int i = 0; i < _originalLifes; i++)
        {
            Instantiate(lifeIndicatorPrefab, _spawnPoint, Quaternion.identity);
            _spawnPoint.x += _lifeIndicatorWidth + 0.3f;
        }
    }

    void GetAllIndicators()
    {
        _lifeIndicators = FindObjectsOfType<LifeIndicator>();
    }
    public void GetHitByABullet()
    {
        _lifesLeft -= 1;
        RefreshCounter();
        if (_lifesLeft == 0)
        {
            levelManager.EndGame();
        }
    }
    public void GetHitByAnEnemyShip()
    {
        _lifesLeft = 0;
        RefreshCounter();
        levelManager.EndGame();
    }
    public void RefreshCounter()
    {
        Destroy(_lifeIndicators[_originalLifes - _lifesLeft - 1].gameObject);
    }

}
