using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class MoveableControllerTest
{
    private GameObject testObj;
    private Furniture furniture;
    private FurnitureController controller;

    [SetUp]
    public void Setup()
    {
        testObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/mobileTable"));
        testObj.name = "mobileTable";

        furniture = testObj.GetComponent<Furniture>();
        controller = new MoveableController(testObj.transform);
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
    [TestCase(new bool[] { })]
    [TestCase(new bool[] { true, true})]
    [TestCase(new bool[] { false, false })]
    public void NothingWillHappenWhenWronAmountOfInstructionsGiven_Test(bool[] instructions)
    {
        GameObject playerObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player_Mock"));
        playerObj.name = "Player";

        controller.Interact(instructions, 1f);

        Assert.IsNull(playerObj.transform.Find("mobileTable"));

        GameObject.DestroyImmediate(playerObj);
    }

    [Test]
    public void FirstPressFWillLockTheTableToThePlayerMovementWhenIsTriggered_Test()
    {
        GameObject playerObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player_Mock"));
        playerObj.name = "Player";

        controller.Interact(new bool[] { true }, 1f);

        Assert.IsNotNull(playerObj.transform.Find("mobileTable"));

        GameObject.DestroyImmediate(playerObj);
    }

    [Test]
    public void SecondPressFWillReleaseTheTableFromThePlayerMovementWhenIsTriggerd_Test()
    {
        GameObject playerObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player_Mock"));
        playerObj.name = "Player";

        controller.Interact(new bool[] { true }, 1f);

        Assert.IsNotNull(playerObj.transform.Find("mobileTable"));

        controller.Interact(new bool[] { true }, 1f);

        Assert.IsNull(playerObj.transform.Find("mobileTable"));
        Assert.IsNotNull(GameObject.Find("mobileTable"));

        GameObject.DestroyImmediate(playerObj);
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.DestroyImmediate(testObj);
        controller = null;
    }
}