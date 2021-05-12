using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionBehaviour : MonoBehaviour
{
    public LifeCounter playerlLifeCounter;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.GetComponent<EnemyBullet>())
        {
            playerlLifeCounter.GetHitByABullet();
        }

        if (other.transform.GetComponent<EnemyShipController>())
        {
            playerlLifeCounter.GetHitByAnEnemyShip();
        }
    }
}
