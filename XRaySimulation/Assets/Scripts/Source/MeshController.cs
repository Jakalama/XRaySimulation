using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshController
{
    public MeshContainer MeshContainer;

    public VertexData[] VerticeData;
    public List<int> RelevantVertices;

    public float AverageDose { get { return CalculateAverageDose(); } private set { } }

    public MeshController(MeshContainer container)
    {
        MeshContainer = container;

        VerticeData = SetVerticeData();
        RelevantVertices = new List<int>();
    }

    private VertexData[] SetVerticeData()
    {
        Vector3[] vertices = MeshContainer.GetVertices();
        VertexData[] data = new VertexData[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            data[i] = new VertexData(vertices[i], 0f);
        }

        return data;
    }

    public void UpdateRelevantVertices()
    {
        RelevantVertices.Clear();

        Vector3[] normals = MeshContainer.GetNormals();
        Vector3[] vertices = MeshContainer.GetVertices();

        for (int i = 0; i < normals.Length; i++)
        {
            if (IsNormalRelevant(normals[i], vertices[i]))
                RelevantVertices.Add(i);
        }
    }

    private bool IsNormalRelevant(Vector3 normal, Vector3 pointPlane)
    {
        float dot = Vector3.Dot(new Vector3(5, 0, 0), normal);

        if (dot < 0)
            return true;

        return false;
    }

    public Vector3[] GetRelevantVerticePositions()
    {
        int num = RelevantVertices.Count;
        Vector3[] positions = new Vector3[num];

        for (int i = 0; i < num; i++)
        {
            positions[i] = VerticeData[RelevantVertices[i]].Position + MeshContainer.transform.position;
        }

        return positions;
    }

    public void StoreDoses(float[] doses)
    {
        for (int i = 0; i < doses.Length; i++)
        {
            StoreDose(i, doses[i]);
        }
    }

    private void StoreDose(int index, float dose)
    {
        VerticeData[RelevantVertices[index]].Dose += dose;
    }

    private float CalculateAverageDose()
    {
        float sum = 0f;
        for (int i = 0; i < VerticeData.Length; i++)
        {
            sum += VerticeData[i].Dose;
        }

        return sum / (float)VerticeData.Length;
    }
}

