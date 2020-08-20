using System;
using UnityEngine;
using NUnit.Framework;

public class MoveControllerTest
{
    public Transform Mock;
    public MoveController Controller;

    [SetUp]
    public void SetUp()
    {
        Mock = GameObject.Instantiate(new GameObject()).transform;
        Mock.position = Vector3.zero;
        Mock.rotation = Quaternion.identity;


        CharacterController c = Mock.gameObject.AddComponent<CharacterController>();
      
        Controller = new MoveController(Mock);
    }

    [Test]
    public void PlayerMovementIsExistent_Test()
    {
        GameObject go = GameObject.Instantiate(new GameObject());
        PlayerMovement dm = go.AddComponent<PlayerMovement>();

        Assert.IsNotNull(dm);
    }

    [Test]
    [TestCase(0.1f)]
    [TestCase(0.75639f)]
    [TestCase(1f)]
    [TestCase(0.5f)]
    public void MovesAlongPositiveZforInputUP_Test(float value)
    {
        Vector3 before = Mock.position;

        Controller.Move(0f, value, 1f);

        Vector3 after = Mock.position;

        Assert.Greater(after.z, before.z);
        Assert.AreEqual(before.x, after.x, 0.01f);
        Assert.AreEqual(before.y, after.y, 0.01f);
    }

    [Test]
    [TestCase(0f)]
    public void MovesNotForInputUPisZero_Test(float value)
    {
        Vector3 before = Mock.position;

        Controller.Move(0f, value, 1f);

        Vector3 after = Mock.position;

        Assert.AreEqual(after.z, before.z);
        Assert.AreEqual(before.x, after.x, 0.01f);
        Assert.AreEqual(before.y, after.y, 0.01f);
    }

    [Test]
    [TestCase(-0.1f)]
    [TestCase(-0.75639f)]
    [TestCase(-1f)]
    [TestCase(-0.5f)]
    public void MovesAlongNegativeZforInputDOWN_Test(float value)
    {
        Vector3 before = Mock.position;

        Controller.Move(0f, value, 1f);

        Vector3 after = Mock.position;

        Assert.Less(after.z, before.z);
        Assert.AreEqual(before.x, after.x, 0.01f);
        Assert.AreEqual(before.y, after.y, 0.01f);
    }

    [Test]
    [TestCase(0)]
    public void MovesNotforInputDOWNisZero_Test(float value)
    {
        Vector3 before = Mock.position;

        Controller.Move(0f, value, 1f);

        Vector3 after = Mock.position;

        Assert.AreEqual(after.z, before.z);
        Assert.AreEqual(before.x, after.x, 0.01f);
        Assert.AreEqual(before.y, after.y, 0.01f);
    }

    [Test]
    [TestCase(0.1f)]
    [TestCase(0.75639f)]
    [TestCase(1f)]
    [TestCase(0.5f)]
    public void MovesAlongPositiveXforInputRIGHT_Test(float value)
    {
        Vector3 before = Mock.position;

        Controller.Move(value, 0f, 1f);

        Vector3 after = Mock.position;

        Assert.Greater(after.x, before.x);
        Assert.AreEqual(before.z, after.z, 0.01f);
        Assert.AreEqual(before.y, after.y, 0.01f);
    }

    [Test]
    [TestCase(0)]
    public void MovesNotforInputRIGHTisZero_Test(float value)
    {
        Vector3 before = Mock.position;

        Controller.Move(value, 0f, 1f);

        Vector3 after = Mock.position;

        Assert.AreEqual(after.x, before.x);
        Assert.AreEqual(before.z, after.z, 0.01f);
        Assert.AreEqual(before.y, after.y, 0.01f);
    }

    [Test]
    [TestCase(-0.1f)]
    [TestCase(-0.75639f)]
    [TestCase(-1f)]
    [TestCase(-0.5f)]
    public void MovesAlongNegativeXforInputLEFT_Test(float value)
    {
        Vector3 before = Mock.position;

        Controller.Move(value, 0f, 1f);

        Vector3 after = Mock.position;

        Assert.Less(after.x, before.x);
        Assert.AreEqual(before.z, after.z, 0.01f);
        Assert.AreEqual(before.y, after.y, 0.01f);
    }

    [Test]
    [TestCase(0)]
    public void MovesNotforInputLEFTisZero_Test(float value)
    {
        Vector3 before = Mock.position;

        Controller.Move(value, 0f, 1f);

        Vector3 after = Mock.position;

        Assert.AreEqual(after.x, before.x);
        Assert.AreEqual(before.z, after.z, 0.01f);
        Assert.AreEqual(before.y, after.y, 0.01f);
    }
}