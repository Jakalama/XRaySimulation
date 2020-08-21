using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FurnitureTest
{
    private GameObject testObj;
    private Furniture furniture;

    [SetUp]
    public void Setup()
    {
        testObj = GameObject.Instantiate(new GameObject());
        furniture = testObj.AddComponent<Furniture>();
    }

    [Test]
    public void FurnitureHasSphereCollider_Test()
    {
        Assert.IsNotNull(testObj.GetComponent<SphereCollider>());
        // Use the Assert class to test conditions
    }

    [Test]
    public void FurnitureColliderIsTrigger_Test()
    {
        Assert.IsTrue(testObj.GetComponent<SphereCollider>().isTrigger);
    }



    [UnityTest]
    public IEnumerator FurnitureTriggerGetsTriggerdThroughPlayer_Test()
    {
        GameObject playerObj = GameObject.Instantiate(new GameObject());
        playerObj.AddComponent<BoxCollider>();
        playerObj.transform.position = new Vector3(100f, 0f, 0f);

        yield return new WaitForEndOfFrame(); //

        playerObj.transform.position = testObj.transform.position;

        yield return new WaitForEndOfFrame(); //

        Assert.IsTrue(furniture.isTriggerd);
    }
}
