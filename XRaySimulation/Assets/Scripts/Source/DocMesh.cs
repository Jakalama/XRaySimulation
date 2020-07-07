using System;
using System.Collections.Generic;
using UnityEngine;

public class DocMesh : MonoBehaviour
{
    public static DocMesh Instance;

    public Dictionary<int, Tuple<Vector3, float>> Vertices;
    public List<int> RelevantVertices;

    private void Start()
    {
        Instance = this;
        Init();
    }

    public void Init()
    {
        Vertices = new Dictionary<int, Tuple<Vector3, float>>();
        RelevantVertices = new List<int>();
    }

    public Vector3[] GetVertices()
    {
        MeshFilter mf = this.transform.GetComponent<MeshFilter>();
        Vector3[] vertices = mf.sharedMesh.vertices;

        return vertices;
    }

    public void UpdateRelevantVertices(Vector3[] vertices)
    {
        Vector3[] normals = GetNormals();

        for (int i = 0; i < normals.Length; i++)
        {
            if (IsNormalRelevant(normals[i], vertices[i]))
                RelevantVertices.Add(i);
        }
    }

    //private void UpdateVertexColors()
    //{
    //    for (int i = 0; i < RelevantVertices.Count; i++)
    //    {
    //        int index = RelevantVertices[i];
    //        float dose = DoseCalculator.Calculate(10f);
    //        Color32 col = ColorCalculator.Calculate(dose);
    //    }
    //}

    private Vector3[] GetNormals()
    {
        MeshFilter mf = this.transform.GetComponent<MeshFilter>();
        Vector3[] normals = mf.sharedMesh.normals;

        return normals;
    }

    private bool IsNormalRelevant(Vector3 normal, Vector3 pointPlane)
    {
        float dot = Vector3.Dot(new Vector3(5, 0, 0), normal);

        //Debug.DrawRay(pointPlane, normal, Color.black, 100f);
        
        if (dot < 0)
            return true;

        return false;
    }
}
