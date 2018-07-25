using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditTerrainCube : MonoBehaviour {

    public List<GameObject> neighbour = new List<GameObject>();
    Vector3 pos;

    // Use this for initialization
    void Start () {
        UpdateNeighbours();
        pos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		if(pos != transform.position)
        {
            CheckNeighbours();
        }
	}

    public void CheckNeighbours()
    {
        pos = transform.position;

        foreach (GameObject n in neighbour)
        {
            Vector3 newPos;

            while (pos.y - n.transform.position.y > 1)
            {
                newPos = new Vector3(n.transform.position.x, n.transform.position.y + 1, n.transform.position.z);
                n.transform.position = newPos;
                if(n.GetComponent<EditTerrainCube>() != null)
                {
                    n.GetComponent<EditTerrainCube>().CheckNeighbours();
                }
            }

            while (pos.y - n.transform.position.y < -1)
            {
                newPos = new Vector3(n.transform.position.x, n.transform.position.y - 1, n.transform.position.z);
                n.transform.position = newPos;
                if (n.GetComponent<EditTerrainCube>() != null)
                {
                    n.GetComponent<EditTerrainCube>().CheckNeighbours();
                }
            }
        }
    }

    public void UpdateNeighbours()
    {
        neighbour.Clear();
        int layerMask = 1 << 8;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.6F, layerMask))
        {
            neighbour.Add(hit.collider.gameObject);
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, 0.6F, layerMask))
        {
            neighbour.Add(hit.collider.gameObject);
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 0.6F, layerMask))
        {
            neighbour.Add(hit.collider.gameObject);
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.left), out hit, 0.6F, layerMask))
        {
            neighbour.Add(hit.collider.gameObject);
        }
    }
}
