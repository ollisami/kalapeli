using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;

public class PohjaSpawner : MonoBehaviour {

    public GameObject pohjaPreafab;
    public GameObject pohjaHolderPrefab;
    public float size;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnPohja()
    {
        List<GameObject> pohjaObjects = new List<GameObject>();
        int count = 0;
        GameObject mother = null;
        for (int i = -50; i <= 50; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if(mother == null || count > 100)
                {
                    mother = Instantiate(pohjaHolderPrefab, Vector3.zero, Quaternion.identity);
                    mother.transform.parent = this.transform;
                    count = 0;
                }
                float z = -2;
                GameObject temp = Instantiate(pohjaPreafab, new Vector3(j, z, i), Quaternion.identity);
                temp.transform.parent = mother.transform;
                pohjaObjects.Add(temp);
                count++;
            }
        }

        foreach (GameObject go in pohjaObjects)
        {
            go.GetComponent<EditTerrainCube>().UpdateNeighbours();
            Vector3 pos = go.transform.position;
            if (pos.z <= -50 || pos.z >= 49 || pos.x <= 0 || pos.x > size - 3)
            {
                pos.y = 2;
            }
            go.transform.position = pos;
            
        }
    }

    public void DestroyPohja()
    {
        foreach(Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(PohjaSpawner))]
public class PohjaHandler : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PohjaSpawner editScript = (PohjaSpawner)target;
        if (GUILayout.Button("Spawn Cubes"))
        {
            editScript.SpawnPohja();
        }

        if (GUILayout.Button("Destroy Cubes"))
        {
            editScript.DestroyPohja();
        }
    }
}
#endif
