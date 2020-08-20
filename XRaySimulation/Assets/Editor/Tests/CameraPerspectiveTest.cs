using System;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class CameraPerspectiveTest
{
    GameObject mockObj;

    [Test]
    public void FPVisExistent_Test()
    {
        mockObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player_Mock"));
        Transform fpvTransform = mockObj.transform.Find("FPV");

        Assert.IsNotNull(fpvTransform);
    }

    [Test]
    public void TPVisExistent_Test()
    {
        mockObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player_Mock"));
        Transform tpvTransform = mockObj.transform.Find("TPV");

        Assert.IsNotNull(tpvTransform);
    }



    [TearDown]
    public void Teardown()
    {
        GameObject.DestroyImmediate(mockObj);
    }
}
