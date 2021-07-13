using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveManager : MonoBehaviour
{
    public float moveFrequency;
    public delegate void MoveShip(int accumlatedMovesInSequence, int moveDirection);
    public event MoveShip OnMoveShip;
    private WaitForSeconds _nextMovement;
    private int _moveDirection = 1;
    private int _alreadyDoneMovesToSides = 0;
    
    void Start()
    {
        _nextMovement = new WaitForSeconds(moveFrequency);
        StartCoroutine(MoveShipRoutine());
    }
    IEnumerator MoveShipRoutine()
    {
        while (true)
        {
            yield return _nextMovement;
            if (OnMoveShip != null)
            {
                OnMoveShip(_alreadyDoneMovesToSides, _moveDirection);
            }
            _alreadyDoneMovesToSides++;
            if (_alreadyDoneMovesToSides > LevelManager.totalEnemyMovesToSides)
            {
                _alreadyDoneMovesToSides = 0;
                _moveDirection = _moveDirection * -1;
            }
        }
    }
    public void RestartMovement()
    {
        _alreadyDoneMovesToSides = 0;
        _moveDirection = 1;
    }
}
