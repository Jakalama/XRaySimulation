using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class DocMovementTest
{
    public Transform Mock;
    public MoveController Controller;

    [SetUp]
    public void SetUp()
    {
        Mock = GameObject.Instantiate(new GameObject()).transform;
        Controller = new MoveController(Mock);
    }

    [Test]
    public void DocMovementIsExistent_Test()
    {
        GameObject go = GameObject.Instantiate(new GameObject());
        DocMovement dm = go.AddComponent<DocMovement>();

        Assert.IsNotNull(dm);
    }

    [Test]
    [TestCase(0.1f)]
    [TestCase(0.75639f)]
    [TestCase(1f)]
    [TestCase(0.5f)]
    public void MoveInputUPisGreater_Test(float value)
    {
        Vector3 before = Mock.position;

        Controller.Move(0f, value, 1f);

        Vector3 after = Mock.position;

        Assert.Greater(after.z, before.z);
    }

    [Test]
    [TestCase(0f)]
    public void MoveInputUPisEqual_Test(float value)
    {
        Vector3 before = Mock.position;

        Controller.Move(0f, value, 1f);

        Vector3 after = Mock.position;

        Assert.AreEqual(after.z, before.z);
    }

    [Test]
    [TestCase(-0.1f)]
    [TestCase(-0.75639f)]
    [TestCase(-1f)]
    [TestCase(-0.5f)]
    public void MoveInputDOWNisLess_Test(float value)
    {
        Vector3 before = Mock.position;

        Controller.Move(0f, value, 1f);

        Vector3 after = Mock.position;

        Assert.Less(after.z, before.z);
    }

    [Test]
    [TestCase(0)]
    public void MoveInputDOWNisEqual_Test(float value)
    {
        Vector3 before = Mock.position;

        Controller.Move(0f, value, 1f);

        Vector3 after = Mock.position;

        Assert.AreEqual(after.z, before.z);
    }

    [Test]
    [TestCase(0.1f)]
    [TestCase(0.75639f)]
    [TestCase(1f)]
    [TestCase(0.5f)]
    public void MoveInputRIGHTisGreater_Test(float value)
    {
        Vector3 before = Mock.position;

        Controller.Move(value, 0f, 1f);

        Vector3 after = Mock.position;

        Assert.Greater(after.x, before.x);
    }

    [Test]
    [TestCase(0)]
    public void MoveInputRIGHTisEqual_Test(float value)
    {
        Vector3 before = Mock.position;

        Controller.Move(value, 0f, 1f);

        Vector3 after = Mock.position;

        Assert.AreEqual(after.x, before.x);
    }

    [Test]
    [TestCase(-0.1f)]
    [TestCase(-0.75639f)]
    [TestCase(-1f)]
    [TestCase(-0.5f)]
    public void MoveInputLEFTisLess_Test(float value)
    {
        Vector3 before = Mock.position;

        Controller.Move(value, 0f, 1f);

        Vector3 after = Mock.position;

        Assert.Less(after.x, before.x);
    }

    [Test]
    [TestCase(0)]
    public void MoveInputLEFTisEqual_Test(float value)
    {
        Vector3 before = Mock.position;

        Controller.Move(value, 0f, 1f);

        Vector3 after = Mock.position;

        Assert.AreEqual(after.x, before.x);
    }
}