using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct VertexData
{
    public Vector3 Position;
    public float Dose;

    public VertexData(Vector3 pos, float dose)
    {
        Position = pos;
        Dose = dose;
    }
}

public class DocMesh : MonoBehaviour
{
    public static DocMesh Instance;

    public VertexData[] VerticeData;
    public List<int> RelevantVertices;

    public float AverageDose { get { return CalculateAverageDose(); } private set { } }

    private void Start()
    {
        Instance = this;
        Init();
    }

    private void Update()
    {
        UpdateRelevantVertices(GetVertices());

        float[] distances = RayTracer.Instance.GetDistances(GetRelevantVerticePositions());
        float[] addedDoses = DoseCalculator.Calculate(distances);

        StoreDoses(addedDoses);
        float[] accumulatedDoses = VerticeData.Select(x => x.Dose).ToArray();

        Color32[] colors = ColorCalculator.Calculate(accumulatedDoses);

        ApplyColors(colors);
    }

    public void Init()
    {
        VerticeData = SetVerticeData();
        RelevantVertices = new List<int>();
    }

    public Vector3[] GetVertices()
    {
        MeshFilter mf = this.transform.GetComponent<MeshFilter>();
        Vector3[] vertices = mf.mesh.vertices;

        return vertices;
    }

    public void UpdateRelevantVertices(Vector3[] vertices)
    {
        RelevantVertices.Clear();

        Vector3[] normals = GetNormals();

        for (int i = 0; i < normals.Length; i++)
        {
            if (IsNormalRelevant(normals[i], vertices[i]))
                RelevantVertices.Add(i);
        }
    }

    public void StoreDoses(float[] doses)
    {
        for (int i = 0; i < doses.Length; i++)
        {
            StoreSingleDose(i, doses[i]);
        }
    }

    private VertexData[] SetVerticeData()
    {
        Vector3[] vertices = GetVertices();
        VertexData[] data = new VertexData[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            data[i] = new VertexData(vertices[i], 0f);
        }

        return data;
    }

    private Vector3[] GetRelevantVerticePositions()
    {
        int num = RelevantVertices.Count;
        Vector3[] positions = new Vector3[num];

        for (int i = 0; i < num; i++)
        {
            positions[i] = VerticeData[RelevantVertices[i]].Position + this.transform.position;
        }

        return positions;
    }

    private void StoreSingleDose(int index, float dose)
    {
        VerticeData[RelevantVertices[index]].Dose += dose;
    }

    private Vector3[] GetNormals()
    {
        MeshFilter mf = this.transform.GetComponent<MeshFilter>();
        Vector3[] normals = mf.mesh.normals;

        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = (Quaternion.Euler(0f, 90f, 0f) * this.transform.rotation) * normals[i];
        }

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

    private float CalculateAverageDose()
    {
        float sum = 0f;
        for (int i = 0; i < VerticeData.Length; i++)
        {
            sum += VerticeData[i].Dose;
        }

        return sum / (float) VerticeData.Length;
    }

    private void ApplyColors(Color32[] newColors)
    {
        MeshFilter mf = this.transform.GetComponent<MeshFilter>();

        Color32[] currentColors = mf.mesh.colors32;
        Color32[] appliedColors;

        if (currentColors.Length == 0)
        {
            appliedColors = new Color32[VerticeData.Length];
        }
        else
        {
            appliedColors = currentColors;
        }

        for (int i = 0; i < newColors.Length; i++)
        {
            appliedColors[i] = newColors[i];
        }

        mf.sharedMesh.colors32 = appliedColors;
    }
}
