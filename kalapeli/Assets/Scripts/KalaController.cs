using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KalaController : MonoBehaviour {
    public GameObject[] kalaPrefabs;

    public void SpawnKala(Vector3 pos)
    {
        pos.y += 2;
        Instantiate(kalaPrefabs[Random.Range(0, kalaPrefabs.Length - 1)], pos, Quaternion.identity);
    }
}
