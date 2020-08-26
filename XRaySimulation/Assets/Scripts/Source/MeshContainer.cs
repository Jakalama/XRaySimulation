using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshContainer
{
    private Transform meshTransform;
    private MeshFilter meshFilter;

    public MeshContainer(Transform transform)
    {
        this.meshTransform = transform;

        this.meshFilter = meshTransform.GetComponent<MeshFilter>();
    }

    public Vector3[] GetVertices()
    {
        Vector3[] vertices = meshFilter.sharedMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            // rotate each vertex around pivot poitn of the transform
            Vector3 rotatedVec = meshTransform.rotation * vertices[i];

            vertices[i] = meshTransform.position + rotatedVec;
        }

        return vertices;
    }

    public Vector3[] GetNormals()
    {
        Vector3[] normals = meshFilter.sharedMesh.normals;

        for (int i = 0; i < normals.Length; i++)
        {
            // rotate each normals acording to the transform rotation
            normals[i] = meshTransform.rotation * normals[i];
        }

        return normals;
    }

    public void ApplyColors(Color32[] newColors)
    {
        meshFilter.sharedMesh.colors32 = newColors;
    }
}