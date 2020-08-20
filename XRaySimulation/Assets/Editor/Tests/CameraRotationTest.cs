using System;
using UnityEngine;
using NUnit.Framework;

public class RotationControllerTest
{
    public Transform Mock;
    public Transform CameraTransform;
    public RotationController Controller;

    [SetUp]
    public void SetUp()
    {
        Mock = GameObject.Instantiate(new GameObject()).transform;

        GameObject CameraObject = GameObject.Instantiate(new GameObject(), Mock);
        CameraObject.AddComponent<Camera>();
        CameraTransform = CameraObject.transform;

        Controller = new RotationController(Mock);
    }

    [Test]
    public void CameraRotationExistent_Test()
    {
        GameObject go = GameObject.Instantiate(new GameObject());
        PlayerRotation cr = go.AddComponent<PlayerRotation>();

        Assert.IsNotNull(cr);
    }

    [Test]
    [TestCase(0.1f)]
    [TestCase(0.75639f)]
    [TestCase(1f)]
    [TestCase(0.5f)]
    public void MouseInputUPisLess_Test(float value)
    {
        Vector3 before = CameraTransform.rotation.eulerAngles;
        Controller.Rotate(0f, value, 1f);
        Vector3 after = CameraTransform.rotation.eulerAngles;

        // Move to test range.
        // Since the Unity rotation is presented from 0 to 360 degrees.
        // Which means a rotation is always greater than 0, or 0.
        float afterX = after.x - 360f;

        Assert.Less(afterX, before.x);
    }

    [Test]
    [TestCase(-0.1f)]
    [TestCase(-0.75639f)]
    [TestCase(-1f)]
    [TestCase(-0.5f)]
    public void MouseInputDownisGreater_Test(float value)
    {
        Vector3 before = CameraTransform.rotation.eulerAngles;
        Controller.Rotate(0f, value, 1f);
        Vector3 after = CameraTransform.rotation.eulerAngles;

        Assert.Greater(after.x, before.x);
    }

    [Test]
    [TestCase(0.1f)]
    [TestCase(0.75639f)]
    [TestCase(1f)]
    [TestCase(0.5f)]
    public void MouseInputRightisGreater_Test(float value)
    {
        Vector3 before = Mock.rotation.eulerAngles;
        Controller.Rotate(value, 0f, 1f);
        Vector3 after = Mock.rotation.eulerAngles;

        Assert.Greater(after.y, before.y);
    }

    [Test]
    [TestCase(-0.1f)]
    [TestCase(-0.75639f)]
    [TestCase(-1f)]
    [TestCase(-0.5f)]
    public void MouseInputLeftisLess_Test(float value)
    {
        Vector3 before = Mock.rotation.eulerAngles;
        Controller.Rotate(value, 0f, 1f);
        Vector3 after = Mock.rotation.eulerAngles;

        // Move to test range.
        // Since the Unity rotation is presented from 0 to 360 degrees.
        // Which means a rotation is always greater than 0, or 0.
        float afterY = after.y - 360f;

        Assert.Less(afterY, before.y);
    }

    [Test]
    [TestCase(-0.1f)]
    [TestCase(-1f)]
    [TestCase(0f)]
    public void ClampUpRotationForm90To90_Test(float value)
    {
        Controller.SetXRotation(90f);

        Vector3 before = CameraTransform.localEulerAngles;
        Controller.Rotate(0f, value, 1f);
        Vector3 after = CameraTransform.localEulerAngles;

        Assert.AreEqual(after.x, before.x);
    }

    [Test]
    [TestCase(-0.1f)]
    [TestCase(-1f)]
    [TestCase(0f)]
    public void ClampUpRotationFormLess90To90_Test(float value)
    {
        Controller.SetXRotation(89f);

        Vector3 before = CameraTransform.localEulerAngles;
        Controller.Rotate(0f, value, 1f);
        Vector3 after = CameraTransform.localEulerAngles;

        Assert.GreaterOrEqual(after.x, 89f);
        Assert.LessOrEqual(after.x, 90f);
    }

    [Test]
    [TestCase(0.1f)]
    [TestCase(1f)]
    [TestCase(0f)]
    public void ClampDownRotationFormMinus90ToMinus90_Test(float value)
    {
        Controller.SetXRotation(-90f);

        Vector3 before = CameraTransform.localEulerAngles;
        Controller.Rotate(0f, value, 1f);
        Vector3 after = CameraTransform.localEulerAngles;

        Assert.AreEqual(after.x, before.x);
    }

    [Test]
    [TestCase(0.1f)]
    [TestCase(1f)]
    [TestCase(0f)]
    public void ClampDownRotationFormLessMinus90ToMinus90_Test(float value)
    {
        Controller.SetXRotation(-89f);

        Vector3 before = CameraTransform.localEulerAngles;
        Controller.Rotate(0f, value, 1f);
        Vector3 after = CameraTransform.localEulerAngles;

        // Move to test range.
        // Since the Unity rotation is presented from 0 to 360 degrees.
        // Which means a rotation is always greater than 0, or 0.
        float afterX = after.x - 360f;

        Assert.LessOrEqual(afterX, -89f);
        Assert.GreaterOrEqual(afterX, -90f);
    }
}