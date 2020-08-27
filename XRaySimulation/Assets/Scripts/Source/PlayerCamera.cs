using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private ICameraController CameraController;

    public void Start()
    {
        CameraController = new FirstPersonCamera(this.transform, 0f);

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        CameraController.Rotate(x, y, Time.fixedDeltaTime, Input.GetKey(KeyCode.LeftShift));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            ChangeCamera();
    }

    public void ChangeCamera()
    {
        Transform fpv = this.transform.Find("FPV");
        Transform tpv = this.transform.Find("TPV");

        if (fpv.gameObject.activeSelf)
        {
            fpv.gameObject.SetActive(false);
            tpv.gameObject.SetActive(true);

            CameraController = new ThirdPersonCamera(this.transform, CameraController.GetYRotation());
        }
        else
        {
            fpv.gameObject.SetActive(true);
            tpv.gameObject.SetActive(false);

            CameraController = new FirstPersonCamera(this.transform, CameraController.GetYRotation());
        }
    }
}
