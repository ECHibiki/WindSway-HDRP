using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh m;

    Vector3[] vert;
    Vector2[] uv;
    int[] tri;

    void Start()
    {
        m = new Mesh();
        this.GetComponent<MeshFilter>().mesh = m;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vert = new Vector3[]
        {
            new Vector3(-1,0,-1),
            new Vector3(-1,0,1),
            new Vector3(1,0,-1),
            new Vector3(1,0,1)
        };
        tri = new int[]
        {
            0,1,2,
            1,3,2
        };
        uv = new Vector2[]
        {
            new Vector2(0,0),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(1,1)
        };
    }

    void UpdateMesh()
    {
        m.Clear();
        m.vertices = vert;
        m.triangles = tri;

        m.RecalculateNormals();
        m.RecalculateBounds();
        m.RecalculateTangents();
        m.uv = uv;
    }
}
