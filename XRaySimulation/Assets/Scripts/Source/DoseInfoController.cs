using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoseInfoController
{
    private Transform infoTransform;
    private TextMeshProUGUI textMesh;
    private Transform meshCameraTransform;
    private MeshCamera cameraController;

    private readonly string UNIT = " Gy";

    public DoseInfoController(Transform transform, Transform meshCameraTransform)
    {
        this.infoTransform = transform;

        this.textMesh = infoTransform.Find("AVG Dose").GetComponent<TextMeshProUGUI>();
        this.meshCameraTransform = meshCameraTransform;

        this.cameraController = new MeshCamera(this.meshCameraTransform, 0f);
    }

    public void SetAVGDose(float dose)
    {
        textMesh.text = dose + UNIT;
    }

    public void Rotate(float x, float y, float time)
    {
        cameraController.Rotate(x, y, time);
    }
}
