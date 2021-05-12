using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Dejo abierta la posibilidad de cambiar la cantidad de filas de enemigos y la distancia entre cada uno
    public int totalRowsToSpawn;
    public float gapBetweenEnemies;
    public int totalEnemiesSpawned;
    
    private float _objectHeight;
    private float _objectWidth;
    private float _endOfScreenPointXAxis;
    private Vector3 _enemySpawnPoint;
    private PrefabSelector _prefabSelector;
    void Start()
    {
        SetProperties();
        SetStartingEnemySpawnPoint();
    }
    void SetStartingEnemySpawnPoint()
    {
        _enemySpawnPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, Camera.main.nearClipPlane));
        _enemySpawnPoint.x = _enemySpawnPoint.x + _objectWidth / 2;
        _enemySpawnPoint.y = _enemySpawnPoint.y - _objectHeight / 2;
        _enemySpawnPoint.z = 0;
    }

    void SetProperties()
    {
        /* Voy a necesitar: 1) Un script propio que devuelve alguno de los prefabs de las naves enemigas aleatoriamente
         *                  2) El tamaño de los prefabs para poder saber en qué punto spawneo el próximo         
         *                  3) Los márgenes horizontales de la pantalla para saber hasta donde puedo spawnear 
         */
        
        _prefabSelector = GetComponent<PrefabSelector>();
        _objectHeight = _prefabSelector.prefabs[0].GetComponent<SpriteRenderer>().bounds.size.y;
        _objectWidth = _prefabSelector.prefabs[0].GetComponent<SpriteRenderer>().bounds.size.x;
        _endOfScreenPointXAxis = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;
    }
    
    public void SpawnEnemies()
    {
        /*  Antes que harcodear la cantidad de enemigos por fila (el ejercicio pedia mas de 10 solamente) me pareció interesante
        *   spawnear todos los que entren en el ancho de la pantalla siempre que quede lugar para que las naves se muevan la cantidad
        *   de veces definida en el LevelManager
        */
        
        bool isEnoughSpaceInTheRowToSpawn = true;
        int rowsSpawned = 0;
        totalEnemiesSpawned = 0;
        
        SetStartingEnemySpawnPoint();
        while (rowsSpawned < totalRowsToSpawn)
        {
            SpawnRandomEnemyAtPoint(_enemySpawnPoint, rowsSpawned);
            isEnoughSpaceInTheRowToSpawn = CheckSpaceLeftOnScreen();
            SetNextSpawnPoint(rowsSpawned, isEnoughSpaceInTheRowToSpawn);
            if (!isEnoughSpaceInTheRowToSpawn)
            {
                rowsSpawned += 1;
            }
        }
        
    }
    void SpawnRandomEnemyAtPoint(Vector3 spawnPoint, int rows)
    {
        GameObject prefabToSpawn = _prefabSelector.SelectRandomPrefab();
        Instantiate(prefabToSpawn, spawnPoint, Quaternion.identity);
        totalEnemiesSpawned++;
    }
    
    void SetNextSpawnPoint(int rowsSpawned, bool isEnoughSpaceInTheRowToSpawn)
    {
        if (isEnoughSpaceInTheRowToSpawn)
        {
            _enemySpawnPoint.x += _objectWidth + gapBetweenEnemies;
        }
        else
        {
            _enemySpawnPoint.y -= _objectHeight + gapBetweenEnemies;
            _enemySpawnPoint.x = _endOfScreenPointXAxis * - 1 + _objectWidth / 2;
        }
    }
    bool CheckSpaceLeftOnScreen()
    {
        bool isEnoughSpace = _enemySpawnPoint.x + (_objectWidth + gapBetweenEnemies) * LevelManager.totalEnemyMovesToSides <= _endOfScreenPointXAxis;

        return isEnoughSpace;
    }

}
