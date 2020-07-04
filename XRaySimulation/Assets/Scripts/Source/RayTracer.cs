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
}
