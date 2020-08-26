using System;
using UnityEngine;

public class RayTracer
{
    private Transform source;

    public RayTracer(Transform transform)
    {
        this.source = transform;
    }

    public bool CreateRay(Vector3 destination)
    {
        return CreateRay(source.position, destination);
    }

    private bool CreateRay(Vector3 start, Vector3 end)
    {
        Vector3 direction = end - start;

        RaycastHit hit;
        Ray ray = new Ray(start, direction.normalized);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Doc")
            {
                Debug.DrawLine(start, hit.point, Color.green, 0.1f);
                return true;
            }

            Debug.DrawLine(start, hit.point, Color.red, 0.1f);
        }

        Debug.DrawLine(start, end, Color.black, 0.1f);

        return false;
    }

    public float[] GetDistances(Vector3[] positions)
    {
        int num = positions.Length;
        float[] distances = new float[num];

        for (int i = 0; i < num; i++)
        {
            distances[i] = GetDistance(positions[i]);
        }

        return distances;
    }

    private float GetDistance(Vector3 position)
    {
        return (float)Math.Round(Vector3.Distance(source.position, position), 3);
    }
}