using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShipController : MonoBehaviour
{
    public bool isAboutToDie;
    public int lifes;
    public int color;
    public float width;
    public GameObject bullet;
    public EnemyShipDestructionManager enemyShipDestructionManager;
    private float _height;
    private WaitForSeconds _nextMovement;
    private EnemyFireManager _enemyFireManager;
    private EnemyMoveManager _enemyMoveManager;

    void Start()
    {
        SetProperties();
        _enemyFireManager.OnFire += Fire;
        _enemyMoveManager.OnMoveShip += MoveShip;
    }
    void SetProperties()
    {
        _height = GetComponent<SpriteRenderer>().bounds.size.y;
        _enemyFireManager = FindObjectOfType<EnemyFireManager>();
        _enemyMoveManager = FindObjectOfType<EnemyMoveManager>();
        enemyShipDestructionManager = GetComponent<EnemyShipDestructionManager>();
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    public void MoveShip(int accumlatedMovesInSequence, int moveDirection)
    {
        if (accumlatedMovesInSequence == LevelManager.totalEnemyMovesToSides)
        {
            gameObject.transform.Translate(0 , _height * -1, 0);
        }
        else
        {
            gameObject.transform.Translate(width * moveDirection, 0, 0);
        }
    }
    public void Fire()
    { 
        /*  El ejercico indicaba que se dispararía en intervalos aleatorios de no más de 3 segundos. Probé usando la rutina
         *  EnemyFireManager y luego haciendo que una nave aleatoria tome la posta de ser quien dispara, pero pasaba con
         *  poca frecuencia y me veía forzado a poner la rutina a ejecutarse en intervalos muy pequeños.
         *  Me pareció mejor hacer que todas las naves escucharan el evento y (si no tienen otra abajo) disparen con una
         *  chance de 2 en 10. Así, es muy poco probable que pasen 3 segundos sin un disparo, y la frecuencia y lugar
         *  del que salen es menos predecible.
         */
        
        if (ShipIsInLastRow() && Random.Range(0f,10f) > 8)
        {
            Vector3 bulletSpawnPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z); 
            Instantiate(bullet, bulletSpawnPoint, Quaternion.identity);
        }
    }
    bool ShipIsInLastRow()
    {
        /*  En el ejercicio se indicaba que solo la última fila de enemigos dispararía. Como no se aclaraba que pasaría si, por ejemplo,
         *  queda un solo enemigo en la última fila (sería raro que solo una nave pudiera disparar); me pareció que no estaría mal
         *  hacer que pueda disparar cualquier nave que no tuviera otras naves abajo.
         */
        bool shipHasAnotherShipUnder = false;
        
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity);
        if (rayHit.collider != null && rayHit.transform.CompareTag("Enemy"))
        {
            shipHasAnotherShipUnder = true;
        }

        return !shipHasAnotherShipUnder;
    }
    public void RecieveDamage()
    {
        if (lifes == 1)
        {
            enemyShipDestructionManager.Die();
        }
        else
        {
            lifes--;
        }
    }
    private void OnDisable()
    {
        _enemyFireManager.OnFire -= Fire;
        _enemyMoveManager.OnMoveShip -= MoveShip;
    }
}
