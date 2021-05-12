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
    
    void Start()
    {
        _nextMovement = new WaitForSeconds(moveFrequency);
        StartCoroutine(MoveShipRoutine());
    }
    IEnumerator MoveShipRoutine()
    {
        int i = 0;
        while (true)
        {
            yield return _nextMovement;
            if (OnMoveShip != null)
            {
                OnMoveShip(i, _moveDirection);
            }
            i++;
            if (i > LevelManager.totalEnemyMovesToSides)
            {
                i = 0;
                _moveDirection = _moveDirection * -1;
            }
        }
    }
    public void RestartMovement()
    {
        _moveDirection = 1;
    }
}
