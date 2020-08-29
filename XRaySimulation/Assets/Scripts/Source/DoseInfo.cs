﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class DoseInfo : MonoBehaviour
{
    public static DoseInfo Instance;
    public DoseInfoController Controller;

    public void Start()
    {
        Instance = this;

        Transform meshCameraTransform = GameObject.Find("Player/MeshCamera").transform;

        Controller = new DoseInfoController(this.transform, meshCameraTransform);
    }

    public void Update()
    {
        RotateMeshCamera();    
    }

    private void RotateMeshCamera()
    {
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
            y += 1;
        if (Input.GetKey(KeyCode.DownArrow))
            y -= 1;
        if (Input.GetKey(KeyCode.RightArrow))
            x += 1;
        if (Input.GetKey(KeyCode.LeftArrow))
            x -= 1;

        Controller.Rotate(x, y, Time.deltaTime);
    }
}