using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipCollisionBehaviour : MonoBehaviour
{
    private EnemyShipController _shipController;
    void Start()
    {
        _shipController = GetComponent<EnemyShipController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerBullet>() != null)
        {
            _shipController.RecieveDamage();
        }
    }
}
