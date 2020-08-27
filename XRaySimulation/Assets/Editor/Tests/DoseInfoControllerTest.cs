using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoseInfoControllerTest
{
    private Transform doseInfoTransform;
    private DoseInfo doseInfo;
    private DoseInfoController controller;
    private Transform meshCameraTransform;

    [SetUp]
    public void SetUp()
    {
        // intantiate the UI DoseInfo
        GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/DoseInfo");
        doseInfoTransform = GameObject.Instantiate(prefab).transform;

        doseInfo = doseInfoTransform.gameObject.GetComponent<DoseInfo>();

        meshCameraTransform = GameObject.Instantiate(new GameObject("MeshCamera")).transform;
        controller = new DoseInfoController(doseInfoTransform, meshCameraTransform);
    }

    [Test]
    public void DoseInfoIsNotNull_Test()
    {
        Assert.IsNotNull(doseInfo);
    }

    [Test]
    public void ControllerIsNotNull_Test()
    {
        Assert.IsNotNull(controller);
    }

    [Test]
    public void DoseInfoTransformIsNotNull_Test()
    {
        System.Reflection.FieldInfo info = controller.GetType().GetField("infoTransform", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Transform transform = (Transform) info.GetValue(controller);

        Assert.IsNotNull(transform);
    }

    [Test]
    public void DoseInfoTransformIsCorrectTransform_Test()
    {
        System.Reflection.FieldInfo info = controller.GetType().GetField("infoTransform", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Transform transform = (Transform)info.GetValue(controller);

        Assert.AreEqual(doseInfoTransform, transform);
    }

    [Test]
    public void SetAVGDose_Test()
    {
        float dose = 10f;
        string expected = dose + " Gy";

        controller.SetAVGDose(dose);

        TextMeshProUGUI textMesh = doseInfoTransform.Find("AVG Dose").GetComponent<TextMeshProUGUI>();
        string actual = textMesh.text;

        Assert.AreEqual(expected, actual);
    }

    private const float SPEED = 250f;

    [Test]
    public void ArrowUPRotatesMeshCameraUp_Test()
    {
        float expected = 1f * SPEED;

        // since the desired rotation is clamped
        expected = Mathf.Clamp(expected, -60, 60);
        // because Unity returns rotations from 0 to 360 degrees
        expected = 360f - expected;

        controller.Rotate(0f, 1f, 1f);
        Vector3 after = meshCameraTransform.rotation.eulerAngles;

        Assert.AreEqual(expected, (float)Math.Round(after.x, 3));
    }

    [Test]
    public void ArrowDOWNRotatesMeshCameraDown_Test()
    {
        float expected = -1f * SPEED;

        // since the desired rotation is clamped
        expected = Mathf.Clamp(expected, -60, 60);
        // because Unity returns rotations from 0 to 360 degrees
        expected = -expected;

        controller.Rotate(0f, -1f, 1f);
        Vector3 after = meshCameraTransform.rotation.eulerAngles;

        Assert.AreEqual(expected, (float)Math.Round(after.x, 3));
    }

    [Test]
    public void ArrowRIGHTRotatesMeshCameraRight_Test()
    {
        float expected = 1f * SPEED;

        controller.Rotate(1f, 0f, 1f);
        Vector3 after = meshCameraTransform.rotation.eulerAngles;

        Assert.AreEqual(expected, after.y);
    }

    [Test]
    public void ArrowLEFTRotatesMeshCameraLeft_Test()
    {
        float expected = -1f * SPEED;
        // because Unity returns rotations from 0 to 360 degrees
        expected = 360 + expected;

        controller.Rotate(-1f, 0f, 1f);
        Vector3 after = meshCameraTransform.rotation.eulerAngles;

        Assert.AreEqual(expected, after.y);
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(doseInfoTransform.gameObject);
        GameObject.DestroyImmediate(meshCameraTransform.gameObject);
    }
}