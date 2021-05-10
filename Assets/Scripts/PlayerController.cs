using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageInput();
    }

    void ManageInput()
    {
        
        ManageMovement();
        ManageShooting();
    }

    void ManageMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {

            transform.Translate(- velocity, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(velocity, 0, 0);
        }
    }

    void ManageShooting()
    {
        
    }
}
