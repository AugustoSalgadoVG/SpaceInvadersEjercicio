using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity;
    public float fireRate;
    public GameObject bulletPrefab;
    public LevelManager levelManager;
    private float _nextFire = 0f;
    void Update()
    {
        if (!levelManager.isGamePaused)
        {
            ManageInput();

        }
    }
    void ManageInput()
    {
        
        ManageMovement();
        ManageShooting();
    }

    void ManageMovement()
    {

        if (Input.GetKey(KeyCode.A) || Input.GetKey((KeyCode.LeftArrow)))
        {
            transform.Translate(- velocity, 0, 0);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey((KeyCode.RightArrow)))
        {
            transform.Translate(velocity, 0, 0);
        }

        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.P))
        {
            levelManager.PauseGame();
        }
    }
    void ManageShooting()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire)
        {
            _nextFire = Time.time + fireRate;
            Fire();
        }
    }
    void Fire()
    {
        Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
    }
}
