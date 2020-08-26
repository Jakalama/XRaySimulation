using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshController
{
    public VertexData[] VerticeData;
    public List<int> RelevantVertices;

    private MeshContainer meshContainer;
    private Transform meshTransform;

    public float AverageDose { get { return CalculateAverageDose(); } private set { } }

    public MeshController(MeshContainer container, Transform transform)
    {
        this.meshContainer = container;
        this.meshTransform = transform;

        this.VerticeData = SetVerticeData();
        this.RelevantVertices = new List<int>();
    }

    private VertexData[] SetVerticeData()
    {
        Vector3[] vertices = meshContainer.GetVertices();
        VertexData[] data = new VertexData[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            data[i] = new VertexData(vertices[i], 0f);
        }

        return data;
    }

    public void UpdateVertices(Vector3 raySource)
    {
        RelevantVertices.Clear();

        Vector3[] normals = meshContainer.GetNormals();
        Vector3[] vertices = meshContainer.GetVertices();

        for (int i = 0; i < normals.Length; i++)
        {
            VerticeData[i].Position = vertices[i];

            if (IsNormalRelevant(normals[i], VerticeData[i].Position, raySource))
                RelevantVertices.Add(i);
        }
    }

    private bool IsNormalRelevant(Vector3 normal, Vector3 pointPlane, Vector3 raySource)
    {
        // calculates the angle between the normals and the ray-source
        float angle = Vector3.Dot(normal, raySource - pointPlane);

        // draw orientations of normals and distance vector of vertice and source
        //Debug.DrawRay(pointPlane, normal, Color.red, 0.1f);
        //Debug.DrawRay(pointPlane, new Vector3(0, 0, 0) - pointPlane, Color.blue, .1f);

        if (angle >= 0)
            return true;

        return false;
    }

    public Vector3[] GetRelevantVerticePositions()
    {
        int num = RelevantVertices.Count;
        Vector3[] positions = new Vector3[num];

        for (int i = 0; i < num; i++)
        {
            positions[i] = GetRelevantVertexPosition(i);
        }

        return positions;
    }

    private Vector3 GetRelevantVertexPosition(int index)
    {
        return VerticeData[RelevantVertices[index]].Position;
    }

    public void SortOutUnhittedVertices(RayTracer tracer)
    {
        for (int i = 0; i < RelevantVertices.Count; i++)
        {
            if (!tracer.CreateRay(GetRelevantVertexPosition(i)))
                RelevantVertices.Remove(RelevantVertices[i]);
        }
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

