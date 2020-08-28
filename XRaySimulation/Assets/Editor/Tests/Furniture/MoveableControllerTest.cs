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
        testObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Table"));
        testObj.name = "Table";

        furniture = testObj.GetComponent<Furniture>();
        controller = new MoveableController(testObj.transform);
    }

    [Test]
    public void FirstPressFWillLockTheTableToThePlayerMovementWhenIsTriggered_Test()
    {
        GameObject playerObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player_Mock"));
        playerObj.name = "Player";

        controller.Interact(new bool[] { true }, 1f);

        Assert.IsNotNull(playerObj.transform.Find("Table"));

        GameObject.DestroyImmediate(playerObj);
    }

    [Test]
    public void SecondPressFWillReleaseTheTableFromThePlayerMovementWhenIsTriggerd_Test()
    {
        GameObject playerObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player_Mock"));
        playerObj.name = "Player";

        controller.Interact(new bool[] { true }, 1f);

        Assert.IsNotNull(playerObj.transform.Find("Table"));

        controller.Interact(new bool[] { true }, 1f);

        Assert.IsNull(playerObj.transform.Find("Table"));
        Assert.IsNotNull(GameObject.Find("Table"));

        GameObject.DestroyImmediate(playerObj);
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.DestroyImmediate(testObj);
        controller = null;
    }
}