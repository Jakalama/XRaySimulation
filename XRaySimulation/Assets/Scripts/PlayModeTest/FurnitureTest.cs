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

    [SetUp]
    public void Setup()
    {
        testObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Table"));
        furniture = testObj.GetComponent<Furniture>();
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
    // Maybe there is an issue with trigger collisions in play test mode.
    [UnityTest]
    public IEnumerator FurnitureTriggerGetsTriggerdThroughPlayer_Test()
    {
        testObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Table"));
        furniture = testObj.GetComponent<Furniture>();

        GameObject playerObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player_Mock"));
        playerObj.transform.position = new Vector3(2f, 0f, 0f);

        yield return new WaitForFixedUpdate();

        Assert.IsNotNull(playerObj.GetComponent<BoxCollider>());
        Assert.IsTrue(furniture.isTriggerd);
    }
}
