using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PrefabSelector : MonoBehaviour
{
    public GameObject[] prefabs;

    public GameObject SelectRandomPrefab()
    {
        GameObject randomSelectedPrefab = prefabs[Random.Range(0, 4)];

        return randomSelectedPrefab;
    }

    public GameObject Sarasa(int fila)
    {
        if (fila < 3)
        {
            return prefabs[1];
        }
        else
        {
            return prefabs[3];
        }
    }
}
