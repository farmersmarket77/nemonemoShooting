using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    private Vector3[] vec_vertices;
    private int[] i_triangles;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    private void CreateShape()
    {
        vec_vertices = new Vector3[]
        {
            new Vector3(0,0,0),
            new Vector3(0,0,1),
            new Vector3(1,0,0),
            new Vector3(1,0,1)
        };

        i_triangles = new int[]
        {
            0, 1, 2,
            1, 2, 3
        };
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vec_vertices;
        mesh.triangles = i_triangles;
        mesh.RecalculateNormals();
    }
}
