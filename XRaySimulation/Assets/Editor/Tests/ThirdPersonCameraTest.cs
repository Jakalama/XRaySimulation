using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ThirdPersonCameraTest
{
    public Transform Mock;
    public Transform CameraTransform;
    public ThirdPersonCamera Controller;

    private const float SPEED = 250f; 

    [SetUp]
    public void SetUp()
    {
        Mock = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player_Mock")).transform;
        CameraTransform = Mock.Find("TPV");

        Controller = new ThirdPersonCamera(Mock, 0f);

        //Set SPEED const
        System.Reflection.FieldInfo info = Controller.GetType().GetField("SPEED", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        info.SetValue(Controller, SPEED);
    }

    [Test]
    public void PlayerCameraIsExistent_Test()
    {
        PlayerCamera pc = Mock.GetComponent<PlayerCamera>();
        
        Assert.IsNotNull(pc);
    }

    [Test]
    [TestCase(0.1f)]
    [TestCase(0.75639f)]
    [TestCase(1f)]
    [TestCase(0.5f)]
    public void MouseInputUPRotatesCameraDown_Test(float value)
    {
        float expected = value * 1f * SPEED;

        // since the desired rotation is clamped
        expected = Mathf.Clamp(expected, -60, 60);
        // because Unity returns rotations from 0 to 360 degrees
        expected = 360f - expected;

        Controller.Rotate(0f, value, 1f);
        Vector3 after = CameraTransform.rotation.eulerAngles;

        Assert.AreEqual(expected, (float)Math.Round(after.x, 3));
    }

    [Test]
    [TestCase(-0.1f)]
    [TestCase(-0.75639f)]
    [TestCase(-1f)]
    [TestCase(-0.5f)]
    public void MouseInputDOWNRotatesCameraUp_Test(float value)
    {
        float expected = value * 1f * SPEED;

        // since the desired rotation is clamped
        expected = Mathf.Clamp(expected, -60, 60);
        // because Unity returns rotations from 0 to 360 degrees
        expected = -expected;

        Controller.Rotate(0f, value, 1f);
        Vector3 after = CameraTransform.rotation.eulerAngles;

        Assert.AreEqual(expected, (float) Math.Round(after.x, 3));
    }

    [Test]
    [TestCase(0.1f)]
    [TestCase(0.75639f)]
    [TestCase(1f)]
    [TestCase(0.5f)]
    public void MouseInputRIGHTRotatesPlayerRight_Test(float value)
    {
        float expected = value * 1f * SPEED;

        Controller.Rotate(value, 0f, 1f);
        Vector3 after = Mock.transform.rotation.eulerAngles;

        Assert.AreEqual(expected, after.y);
    }

    [Test]
    [TestCase(-0.1f)]
    [TestCase(-0.75639f)]
    [TestCase(-1f)]
    [TestCase(-0.5f)]
    public void MouseInputLEFTRotatesPlayerLeft_Test(float value)
    {
        float expected = value * 1f * SPEED;
        // since Unity returns rotations form 0 to 360 degrees
        expected = 360f + expected;

        Controller.Rotate(value, 0f, 1f);
        Vector3 after = Mock.transform.rotation.eulerAngles;

        Assert.AreEqual(expected, after.y);
    }

    [Test]
    [TestCase(0f)]
    [TestCase(10f)]
    [TestCase(-130f)]
    [TestCase(1f)]
    [TestCase(-0.0010f)]
    public void ReturnsCorrectYRotation_Test(float value)
    {
        Controller.Rotate(value, 0f, 1f);
        float after = Controller.GetYRotation();

        Assert.AreEqual(value * SPEED, after);
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.DestroyImmediate(Mock.gameObject);
    }
}
