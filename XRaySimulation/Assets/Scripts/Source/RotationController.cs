using System;
using System.Collections.Generic;
using UnityEngine;

public class RotationController
{
    private Transform playerTransform;
    private Transform cameraTransform;
    private float xRotation;

    private const float RotationSpeed = 250f;
    private const float MAX_X_ROT = 90f;

    public RotationController(Transform rotatedTransform)
    {
        this.playerTransform = rotatedTransform;
        this.cameraTransform = playerTransform.GetComponentInChildren<Camera>().transform;

        xRotation = 0;
    }

    public void Rotate(float x, float y, float time)
    {
        float mouseX = x * time * RotationSpeed;
        float mouseY = y * time * RotationSpeed;

        SetXRotation(-mouseY);

        playerTransform.Rotate(Vector3.up, mouseX, Space.World);
    }

    public void SetXRotation(float value)
    {
        xRotation += value;
        xRotation = Mathf.Clamp(xRotation, -MAX_X_ROT, MAX_X_ROT);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
