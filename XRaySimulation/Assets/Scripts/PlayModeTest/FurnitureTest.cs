using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class FurnitureTest
{
    private GameObject testObj;
    private Furniture furniture;
    private GameObject gui;

    private const float RADIUS = 2f;

    [SetUp]
    public void Setup()
    {
        testObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Table"));
        furniture = testObj.GetComponent<Furniture>();
        testObj.GetComponent<SphereCollider>().radius = RADIUS;

        gui = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UI/GUI"));
        gui.name = "GUI";
    }

    [Test]
    public void FurnitureIsExistent_Test()
    {
        Assert.IsNotNull(furniture);
    }

    [Test]
    public void FurnitureHasSphereCollider_Test()
    {
        Assert.IsNotNull(testObj.GetComponent<SphereCollider>());
    }

    [Test]
    public void FurnitureColliderIsTrigger_Test()
    {
        Assert.IsTrue(testObj.GetComponent<SphereCollider>().isTrigger);
    }

    // Don't know why this test is failing with other Prefabs than the Player_Mock
    // A manual test shows that this tested beahviour is working fine!
    [UnityTest]
    public IEnumerator FurnitureGetsTriggerdThroughPlayer_Test()
    {
        // Instantiate a player
        GameObject playerObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player_Mock"));
        playerObj.transform.position = new Vector3(RADIUS - RADIUS / 2f, 0f, 0f);

        // Wait till unity calculated the physics once
        yield return new WaitForFixedUpdate();

        Assert.IsNotNull(playerObj.GetComponent<BoxCollider>());
        Assert.IsTrue(furniture.isTriggerd);

        GameObject.Destroy(playerObj);
    }

    [UnityTest]
    public IEnumerator FurnitureGetsTriggerdThroughPlayerWhenPlayerIsAtTriggerSphereEdge_Test()
    {
        // Instantiate a player
        GameObject playerObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player_Mock"));
        playerObj.transform.position = new Vector3(RADIUS, 0f, 0f);

        // Wait till unity calculated the physics once
        yield return new WaitForFixedUpdate();

        Assert.IsNotNull(playerObj.GetComponent<BoxCollider>());
        Assert.IsTrue(furniture.isTriggerd);

        GameObject.Destroy(playerObj);
    }

    [UnityTest]
    public IEnumerator JustOneFurnitureGetsTriggeredThroughPlayer_Test()
    {
        // Instantiate a player
        GameObject playerObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player_Mock"));
        playerObj.transform.position = new Vector3(RADIUS, 0f, 0f);

        // Wait till unity calculated the physics once
        yield return new WaitForFixedUpdate();

        // Instantiate a second table, in a distance near the player, so it can get triggerd
        GameObject secondTable = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Table"));
        Furniture secondFurniture = secondTable.GetComponent<Furniture>();
        secondTable.transform.position = new Vector3(2f * RADIUS, 0f, 0f);

        // Wait till unity calculated the physics once
        yield return new WaitForFixedUpdate();

        Assert.IsNotNull(playerObj.GetComponent<BoxCollider>());
        Assert.IsTrue(furniture.isTriggerd);
        Assert.IsFalse(secondFurniture.isTriggerd);

        GameObject.Destroy(playerObj);
        GameObject.Destroy(secondTable);
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.Destroy(testObj);
        FurnitureTriggerInfo.DeactivateFurniture();
    }
}
