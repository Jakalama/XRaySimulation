using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ThirdPersonCamera : ICameraController
{
    private Transform playerTransform;
    private Transform tpvTransform;
    private Transform cameraTransform;

    private float xRotation;
    private float yRotation;

    private const float SPEED = 250f;
    private const float MAX_X_ROT = 60f;
    private const float MIN_X_ROT = -60f;

    public ThirdPersonCamera(Transform rotatedTransform, float yStartRotation)
    {
        this.playerTransform = rotatedTransform;
        this.tpvTransform = playerTransform.Find("TPV");
        this.cameraTransform = tpvTransform.Find("Camera");

        this.xRotation = 0f;
        this.yRotation = yStartRotation;
    }

    public void Rotate(float x, float y, float time) 
    {
        yRotation += x * time * SPEED;
        xRotation -= y * time * SPEED;

        cameraTransform.LookAt(tpvTransform);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            SetXRotation(xRotation, yRotation);
        }
        else 
        {
            SetXRotation(-xRotation, yRotation);

            playerTransform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }

    public void SetXRotation(float valueX, float valueY)
    {
        xRotation = Mathf.Clamp(xRotation, MIN_X_ROT, MAX_X_ROT);

        tpvTransform.rotation = Quaternion.Euler(xRotation, valueY, 0f);
    }

    public float GetYRotation()
    {
        return yRotation;
    }
}
