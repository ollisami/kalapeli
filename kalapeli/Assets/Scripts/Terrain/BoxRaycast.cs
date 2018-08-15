using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class BoxRaycast : MonoBehaviour
{
    private List<Vector3> newVertices = new List<Vector3>();
    private List<int> newTriangles = new List<int>();
    private int faceCount = 0;

    void Awake()
    {
        CreateCube();
    }

    private void CreateCube()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3 pos = transform.position;
        pos = new Vector3(pos.x, Mathf.Round(pos.y), pos.z);
        transform.position = pos;

        int layerMask = 1 << 8;

        RaycastHit hit;
        if (!Physics.Raycast(pos, transform.TransformDirection(Vector3.up), out hit, 0.6F, layerMask))
        {
            CubeTop(-0.5F, 0.5F, -0.5F);
            if (pos.y < -3 && Random.value > 0.98F)
            {
                //GameObject.FindObjectOfType<KalaController>().SpawnKala(pos);
            }
        }
        if (!Physics.Raycast(pos, transform.TransformDirection(-Vector3.up), out hit, 0.6F, layerMask))
        {
            CubeBot(-0.5F, 0.5F, -0.5F);
        }
        if (!Physics.Raycast(pos, transform.TransformDirection(Vector3.forward), out hit, 0.6F, layerMask))
        {
            CubeNorth(-0.5F, 0.5F, -0.5F);
        }
        if (!Physics.Raycast(pos, transform.TransformDirection(-Vector3.forward), out hit, 0.6F, layerMask))
        {
            CubeSouth(-0.5F, 0.5F, -0.5F);
        }
        if (!Physics.Raycast(pos, transform.TransformDirection(Vector3.left), out hit, 0.6F, layerMask))
        {
            CubeWest(-0.5F, 0.5F, -0.5F);
        }
        if (!Physics.Raycast(pos, transform.TransformDirection(-Vector3.left), out hit, 0.6F, layerMask))
        {
            CubeEast(-0.5F, 0.5F, -0.5F);
        }

        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.RecalculateNormals();
    }

    void Cube()
    {

        newTriangles.Add(faceCount * 4); //1
        newTriangles.Add(faceCount * 4 + 1); //2
        newTriangles.Add(faceCount * 4 + 2); //3
        newTriangles.Add(faceCount * 4); //1
        newTriangles.Add(faceCount * 4 + 2); //3
        newTriangles.Add(faceCount * 4 + 3); //4
        faceCount++;
    }

    void CubeTop(float x, float y, float z)
    {
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x, y, z));
        Cube();
    }

    void CubeNorth(float x, float y, float z)
    {
        //CubeNorth
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x, y - 1, z + 1));
        Cube();
    }

    void CubeEast(float x, float y, float z)
    {
        //CubeEast
        newVertices.Add(new Vector3(x + 1, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x + 1, y, z + 1));
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
        Cube();
    }

    void CubeSouth(float x, float y, float z)
    {
        //CubeSouth
        newVertices.Add(new Vector3(x, y - 1, z));
        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x + 1, y, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z));
        Cube();
    }

    void CubeWest(float x, float y, float z)
    {
        //CubeWest
        newVertices.Add(new Vector3(x, y - 1, z + 1));
        newVertices.Add(new Vector3(x, y, z + 1));
        newVertices.Add(new Vector3(x, y, z));
        newVertices.Add(new Vector3(x, y - 1, z));
        Cube();
    }

    void CubeBot(float x, float y, float z)
    {
        //CubeBot
        newVertices.Add(new Vector3(x, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z));
        newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
        newVertices.Add(new Vector3(x, y - 1, z + 1));
        Cube();
    }
}
