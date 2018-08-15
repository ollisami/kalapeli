using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshCombiner : MonoBehaviour
{
    void Start()
    {
        Combine();
    }

    public void Combine()
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
            coll.sharedMesh = transform.GetComponent<MeshFilter>().sharedMesh;
        }
        transform.gameObject.SetActive(true);
        transform.position = Vector3.zero;

    }

    public void DestroyChildren()
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MeshCombiner))]
public class Combine : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        if (GUILayout.Button("Combine"))
        {
            MeshCombiner[] combiners = GameObject.FindObjectsOfType<MeshCombiner>();
            foreach (MeshCombiner mc in combiners)
            {
                mc.Combine();
            }      
        }

        if (GUILayout.Button("Destroy children"))
        {
            MeshCombiner[] combiners = GameObject.FindObjectsOfType<MeshCombiner>();
            foreach (MeshCombiner mc in combiners)
            {
                mc.DestroyChildren();
            }
        }
    }
}
#endif
