using System;
using UnityEngine;
using NUnit.Framework;

public class MoveControllerTest
{
    private Transform mock;
    private MoveController controller;

    [SetUp]
    public void SetUp()
    {
        mock = GameObject.Instantiate(new GameObject()).transform;
        mock.position = Vector3.zero;
        mock.rotation = Quaternion.identity;

        CharacterController c = mock.gameObject.AddComponent<CharacterController>();
      
        controller = new MoveController(mock);
    }

    private T GetPrivateField<T>(string fieldName)
    {
        System.Reflection.FieldInfo info = controller.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (T)info.GetValue(controller);
    }

    [Test]
    public void PlayerMovementIsExistent_Test()
    {
        GameObject go = GameObject.Instantiate(new GameObject());
        PlayerMovement dm = go.AddComponent<PlayerMovement>();

        Assert.IsNotNull(dm);
    }

    [Test]
    public void PlayerTransformIsNotNull_Test()
    {
        Transform transform = GetPrivateField<Transform>("playerTransform");

        Assert.IsNotNull(transform);
    }

    [Test]
    public void CharacterControllerIsNotNull_Test()
    {
        CharacterController characterController = GetPrivateField<CharacterController>("characterController");

        Assert.IsNotNull(characterController);
    }

    [Test]
    [TestCase(0.1f)]
    [TestCase(0.75639f)]
    [TestCase(1f)]
    [TestCase(0.5f)]
    public void MovesAlongPositiveZforInputUP_Test(float value)
    {
        Vector3 before = mock.position;

        controller.Move(0f, value, 1f);

        Vector3 after = mock.position;

        Assert.Greater(after.z, before.z);
        Assert.AreEqual(before.x, after.x, 0.01f);
        Assert.AreEqual(before.y, after.y, 0.01f);
    }

    [Test]
    [TestCase(0f)]
    public void MovesNotForInputUPisZero_Test(float value)
    {
        Vector3 before = mock.position;

        controller.Move(0f, value, 1f);

        Vector3 after = mock.position;

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
        Vector3 before = mock.position;

        controller.Move(0f, value, 1f);

        Vector3 after = mock.position;

        Assert.Less(after.z, before.z);
        Assert.AreEqual(before.x, after.x, 0.01f);
        Assert.AreEqual(before.y, after.y, 0.01f);
    }

    [Test]
    [TestCase(0)]
    public void MovesNotforInputDOWNisZero_Test(float value)
    {
        Vector3 before = mock.position;

        controller.Move(0f, value, 1f);

        Vector3 after = mock.position;

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
        Vector3 before = mock.position;

        controller.Move(value, 0f, 1f);

        Vector3 after = mock.position;

        Assert.Greater(after.x, before.x);
        Assert.AreEqual(before.z, after.z, 0.01f);
        Assert.AreEqual(before.y, after.y, 0.01f);
    }

    [Test]
    [TestCase(0)]
    public void MovesNotforInputRIGHTisZero_Test(float value)
    {
        Vector3 before = mock.position;

        controller.Move(value, 0f, 1f);

        Vector3 after = mock.position;

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
        Vector3 before = mock.position;

        controller.Move(value, 0f, 1f);

        Vector3 after = mock.position;

        Assert.Less(after.x, before.x);
        Assert.AreEqual(before.z, after.z, 0.01f);
        Assert.AreEqual(before.y, after.y, 0.01f);
    }

    [Test]
    [TestCase(0)]
    public void MovesNotforInputLEFTisZero_Test(float value)
    {
        Vector3 before = mock.position;

        controller.Move(value, 0f, 1f);

        Vector3 after = mock.position;

        Assert.AreEqual(after.x, before.x);
        Assert.AreEqual(before.z, after.z, 0.01f);
        Assert.AreEqual(before.y, after.y, 0.01f);
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(mock.gameObject);
        controller = null;
    }
}