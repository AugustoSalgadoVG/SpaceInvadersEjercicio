using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipDestructionManager : MonoBehaviour
{

    private EnemyShipController _enemyShipController;
    private LevelManager _levelManager;
    private AudioSource _explotionAudio;

    /*  Esta es la forma en la que resolví la búsqueda y eliminación de naves adyacentes. Me hacen un poco de ruido
     *  a la vista los metodos KillAdyacentToEtc porque son todos muy parecidos, pero no me quedaba otra que hacer
     *  un raycast en cada direccion y al menos haciéndolo en métodos separados se ve más prolijo
     */
    void Start()
    {
        _enemyShipController = GetComponent<EnemyShipController>();
        _explotionAudio = FindObjectOfType<ExplotionAudioSource>().GetComponent<AudioSource>();
        _levelManager = FindObjectOfType<LevelManager>();
    }
    public void Die()
    {
        _enemyShipController.isAboutToDie = true;
        KillAdyacentShipsWithSameColor();
        Destroy(gameObject);
        _explotionAudio.Play();
        _levelManager.EnemyShipWasDestroyed();
    }
    void KillAdyacentShipsWithSameColor()
    {
        KillAdyacentToRight();
        KillAdyacentToLeft();
        KillAdyacentAbove();
        KillAdyacentUnder();
    }

    void KillAdyacentToRight()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.right, _enemyShipController.width);
        KillIfExists(rayHit);
    }
    void KillAdyacentToLeft()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.left, _enemyShipController.width);
        KillIfExists(rayHit);
    }
    void KillAdyacentAbove()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.up, _enemyShipController.width);
        KillIfExists(rayHit);
    }
    void KillAdyacentUnder()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, _enemyShipController.width);
        KillIfExists(rayHit);
    }
    void KillIfExists(RaycastHit2D rayHit)
    {
        if (rayHit.collider != null && rayHit.transform.GetComponent<EnemyShipController>() != null &&
            rayHit.transform.GetComponent<EnemyShipController>().color == _enemyShipController.color && !rayHit.transform.GetComponent<EnemyShipController>().isAboutToDie)
        {
            rayHit.transform.GetComponent<EnemyShipController>().enemyShipDestructionManager.Die();
        }
    }
}
