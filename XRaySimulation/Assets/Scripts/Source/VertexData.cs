using System;
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
