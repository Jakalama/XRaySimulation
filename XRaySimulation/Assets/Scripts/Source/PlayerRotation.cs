using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private RotationController RotationController;

    public void Start()
    {
        RotationController = new RotationController(this.transform);

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        RotationController.Rotate(x, y, Time.fixedDeltaTime);
    }
}
