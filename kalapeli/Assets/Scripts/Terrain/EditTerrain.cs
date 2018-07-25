using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EditTerrainCube))]
public class EditTerrain : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditTerrainCube editScript = (EditTerrainCube)target;
        if (GUILayout.Button("Get Neighbours"))
        {
            editScript.UpdateNeighbours();
        }
    }
}

