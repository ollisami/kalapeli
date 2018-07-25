using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshCombiner : MonoBehaviour
{
    void Start()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }
        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine, true, true);
        mesh.RecalculateBounds();
        

        transform.GetComponent<MeshFilter>().mesh = mesh;

        MeshCollider coll = transform.GetComponent<MeshCollider>();
        if (coll != null)
        {
            coll.sharedMesh = null;
            coll.sharedMesh = transform.GetComponent<MeshFilter>().mesh;
        }
        transform.gameObject.SetActive(true);
        transform.position = Vector3.zero;

    }
}
