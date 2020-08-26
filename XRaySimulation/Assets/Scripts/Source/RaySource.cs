using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RaySource : MonoBehaviour
{
    public static RaySource Instance;

    public RayTracer rayTracer;

    private void Start()
    {
        Instance = this;

        this.rayTracer = new RayTracer(this.transform);
    }
}