using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private ICameraController CameraController;

    public void Start()
    {
        CameraController = new FirstPersonCamera(this.transform, 0f);

        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        float x = UnityInput.Instance.GetAxis("Mouse X");
        float y = UnityInput.Instance.GetAxis("Mouse Y");

        CameraController.Rotate(x, y, Time.deltaTime, UnityInput.Instance.GetKey(KeyCode.LeftShift));
    }

    private void Update()
    {
        if (UnityInput.Instance.GetKeyDown(KeyCode.Tab))
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
