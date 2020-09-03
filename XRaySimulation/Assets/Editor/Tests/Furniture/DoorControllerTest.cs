using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class DoorControllerTest
{
    private GameObject testObj;
    private Furniture furniture;
    private FurnitureController controller;

    [SetUp]
    public void SetUp()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Door");
        testObj = GameObject.Instantiate(prefab);
        testObj.name = "Door";

        furniture = testObj.GetComponent<Furniture>();
        controller = new DoorController(testObj.transform);
    }

    private T GetPrivateField<T>(string fieldName)
    {
        System.Reflection.FieldInfo info = controller.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (T)info.GetValue(controller);
    }

    [Test]
    public void TestObjectIsNotNull_Test()
    {
        Assert.IsNotNull(testObj);
    }

    [Test]
    public void FurnitrueIsNotNull_Test()
    {
        Assert.IsNotNull(furniture);
    }

    [Test]
    public void ControllerIsNotNull_Test()
    {
        Assert.IsNotNull(controller);
    }

    [Test]
    public void LeftDoorIsNotNull_Test()
    {
        Transform left = GetPrivateField<Transform>("leftDoor");

        Assert.IsNotNull(left);
    }

    [Test]
    public void RightDoorIsNotNull_Test()
    {
        Transform right = GetPrivateField<Transform>("rightDoor");

        Assert.IsNotNull(right);
    }

    [Test]
    [TestCase(new bool[] { })]
    [TestCase(new bool[] { true, true })]
    [TestCase(new bool[] { false, false })]
    public void NothingWillHappenWhenWronAmountOfInstructionsGiven_Test(bool[] instructions)
    {
        controller.Interact(instructions, 1f);

        bool isOpen = GetPrivateField<bool>("isOpen");

        Assert.IsFalse(isOpen);
    }

    [Test]
    public void FirstPressOfFWillSetIsOpenToTrue_Test()
    {
        controller.Interact(new bool[] { true }, 1f);

        bool isOpen = GetPrivateField<bool>("isOpen");

        Assert.IsTrue(isOpen);
    }

    [Test]
    public void SecondPressOfFWillSetIsOpenFalse_Test()
    {
        controller.Interact(new bool[] { true }, 1f);
        controller.Interact(new bool[] { true }, 1f);

        bool isOpen = GetPrivateField<bool>("isOpen");

        Assert.IsFalse(isOpen);
    }

    [Test]
    public void FirstPressOfFWillOpenDoor_Test()
    {
        Transform left = GetPrivateField<Transform>("leftDoor");
        Transform right = GetPrivateField<Transform>("rightDoor");

        Vector3 leftBefore = left.localPosition;
        Vector3 rightBefore = right.localPosition;

        controller.Interact(new bool[] { true }, 1);

        Vector3 leftAfter = left.localPosition;
        Vector3 rightAfter = right.localPosition;

        Assert.Less(leftAfter.z, leftBefore.z);
        Assert.Greater(rightAfter.z, rightBefore.z);
    }

    [Test]
    public void SecondPressOfFWillCloseDoor_Test()
    {
        Transform left = GetPrivateField<Transform>("leftDoor");
        Transform right = GetPrivateField<Transform>("rightDoor");

        Vector3 leftBefore = left.localPosition;
        Vector3 rightBefore = right.localPosition;

        controller.Interact(new bool[] { true }, 1);
        controller.Interact(new bool[] { true }, 1);

        Vector3 leftAfter = left.localPosition;
        Vector3 rightAfter = right.localPosition;

        Assert.AreEqual(leftAfter.z, leftBefore.z);
        Assert.AreEqual(rightAfter.z, rightBefore.z);
    }


    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(testObj);
        controller = null;
    }
}