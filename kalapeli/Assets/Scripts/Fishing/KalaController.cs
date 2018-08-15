using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KalaController : MonoBehaviour {
    public GameObject[] kalaPrefabs;
    public GameObject[] puuPrefabs;

    private List<GameObject> kalat;
    private Kala targetKala;
    public TavoiteScreen tavoiteScreen;

    public int fishCount = 300;
    private List<Vector3> possibleSpawnPoints = new List<Vector3>();

    private void Update()
    {
        
    }

    void Start()
    {
        kalat = new List<GameObject>();
        int layerMask = 1 << 8;
        RaycastHit hit;

        for (int z = -245; z < 245; z += 5)
        {
            for (int x = 20; x < 480; x += 5)
            {
                for (int y = -20; y < -2; y += 2)
                {
                    Vector3 pos = new Vector3(x, y, z);
                    if (
                        Physics.Raycast(pos, -Vector3.up, out hit, 20.0F, layerMask) &&
                        Physics.Raycast(new Vector3(pos.x + 1, pos.y, pos.z), -Vector3.up, out hit, 20.0F, layerMask) &&
                        Physics.Raycast(new Vector3(pos.x - 1, pos.y, pos.z), -Vector3.up, out hit, 20.0F, layerMask) &&
                        Physics.Raycast(new Vector3(pos.x, pos.y, pos.z + 1), -Vector3.up, out hit, 20.0F, layerMask) &&
                        Physics.Raycast(new Vector3(pos.x, pos.y, pos.z - 1), -Vector3.up, out hit, 20.0F, layerMask))
                    {
                        possibleSpawnPoints.Add(pos);
                    }
                    pos.y = Random.Range(5,10);
                    if(
                        Random.value > 0.9F &&
                        Physics.Raycast(pos, -Vector3.up, out hit, 5.0F, layerMask) &&
                        Physics.Raycast(new Vector3(pos.x + 1, pos.y, pos.z), -Vector3.up, out hit, 1.5F, layerMask) &&
                        Physics.Raycast(new Vector3(pos.x - 1, pos.y, pos.z), -Vector3.up, out hit, 1.5F, layerMask) &&
                        Physics.Raycast(new Vector3(pos.x, pos.y, pos.z + 1), -Vector3.up, out hit, 1.5F, layerMask) &&
                        Physics.Raycast(new Vector3(pos.x, pos.y, pos.z - 1), -Vector3.up, out hit, 1.5F, layerMask))
                    {
                        Instantiate(puuPrefabs[Random.Range(0, puuPrefabs.Length)], hit.point, Quaternion.identity);
                    }
                }
            }
        }
        SpawnFishes();
        SetTargetKala();
    }



    public bool CheckTargetFish(Kala kala) {
        if(kala.laji.Equals(targetKala.laji)) {
            SetTargetKala();
            return true;
        }
        return false;
    }

    public void SetTargetKala() {
        if (kalat.Count == 0) return;

        GameObject t = null;
        int counter = 0;
        while (t == null && counter < 1000) {
            counter++;
            t = kalat[Random.Range(0, kalat.Count)];
            if(t.GetComponent<Kala>().laji.Equals("Seaweed")) {
                t = null;
            }
        }
        if (t == null) return;

        targetKala = t.GetComponent<Kala>();
        tavoiteScreen.SetTavoite(targetKala);
    }

    private void SpawnFishes() {
        while(possibleSpawnPoints.Count != 0 && kalat.Count < fishCount) {
            int randomIndex = Random.Range(0, possibleSpawnPoints.Count);
            SpawnKala(possibleSpawnPoints[randomIndex], 1);
            possibleSpawnPoints.RemoveAt(randomIndex);
        }
    }

    public void SpawnKala(Vector3 pos, float scale)
    {
        GameObject kala = Instantiate(kalaPrefabs[Random.Range(0, kalaPrefabs.Length)], pos, Quaternion.identity) as GameObject;
        //GameObject kala = Instantiate(kalaPrefabs[0], pos, Quaternion.identity) as GameObject;
        kala.GetComponent<Kala>().InitializeKala(scale);
        kala.transform.parent = this.transform;
        kalat.Add(kala);
    }
}
