using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ProtectionWallTest
{
    private GameObject testObj;
    private Furniture furniture;
    private FurnitureController controller;

    [SetUp]
    public void Setup()
    {
        testObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/ProtectionWall"));
        testObj.name = "ProtectionWall";

        furniture = testObj.GetComponent<Furniture>();
        controller = new MoveableController(testObj.transform);
    }

    [Test]
    public void FirstPressFWillLockTheWallToThePlayerMovementWhenIsTriggered_Test()
    {
        GameObject playerObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player_Mock"));
        playerObj.name = "Player";

        controller.Interact(new bool[] { true }, 1f);

        Assert.IsNotNull(playerObj.transform.Find("ProtectionWall"));

        GameObject.DestroyImmediate(playerObj);
    }

    [Test]
    public void SecondPressFWillReleaseTheWallFromThePlayerMovementWhenIsTriggerd_Test()
    {
        GameObject playerObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player_Mock"));
        playerObj.name = "Player";

        controller.Interact(new bool[] { true }, 1f);

        Assert.IsNotNull(playerObj.transform.Find("ProtectionWall"));

        controller.Interact(new bool[] { true }, 1f);

        Assert.IsNull(playerObj.transform.Find("ProtectionWall"));
        Assert.IsNotNull(GameObject.Find("ProtectionWall"));

        GameObject.DestroyImmediate(playerObj);
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.DestroyImmediate(testObj);
        controller = null;
    }
}