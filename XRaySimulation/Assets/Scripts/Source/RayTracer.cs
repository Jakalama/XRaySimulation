using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTracer : MonoBehaviour
{
    public static RayTracer Instance;

    private void Start()
    {
        Instance = this;
    }

    public bool CreateRay(Vector3 origin, Vector3 destination)
    {
        RaycastHit hit;
        Ray ray = new Ray(origin, destination.normalized);
        
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Doc")
                return true;

        }

        return false;
    }

    public bool CreateRaySourceToDoc(Vector3 destination)
    {
        return true;
    }

    public float[] GetDistances(Vector3[] positions)
    {
        int num = positions.Length;
        float[] distances = new float[num];

        for (int i = 0; i < num; i++)
        {
            distances[i] = (float)Math.Round(Vector3.Distance(Vector3.zero, positions[i]), 3);
        }

        return distances;
    }
}
