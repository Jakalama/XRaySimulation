using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshContainer : MonoBehaviour
{
    //ToDo: Remove for mulitple meshes support
    public static MeshContainer Instance;

    private void Start()
    {
        Instance = this;
    }

    public Vector3[] GetVertices()
    {
        MeshFilter mf = this.transform.GetComponent<MeshFilter>();
        Vector3[] vertices = mf.sharedMesh.vertices;

        return vertices;
    }

    public Vector3[] GetNormals()
    {
        MeshFilter mf = this.transform.GetComponent<MeshFilter>();
        Vector3[] normals = mf.sharedMesh.normals;

        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = (Quaternion.Euler(0f, 90f, 0f) * this.transform.rotation) * normals[i];
        }

        return normals;
    }

    public void ApplyColors(Color32[] newColors)
    {
        MeshFilter mf = this.transform.GetComponent<MeshFilter>();

        mf.sharedMesh.colors32 = newColors;
    }
}