using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireManager : MonoBehaviour
{
    public delegate void Fire();
    public event Fire OnFire;
    void Start()
    {
        StartCoroutine(FireRoutine());
    }
    IEnumerator FireRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0f, 2.9f));
            if (OnFire != null)
            {
                OnFire();
            }
        }
    }
}
