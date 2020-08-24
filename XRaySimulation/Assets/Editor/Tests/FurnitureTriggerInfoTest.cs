using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

public class FurnitureTriggerInfoTest
{
    GameObject gui;

    [SetUp]
    public void Setup()
    {
        gui = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UI/GUI"));
        gui.name = "GUI";
    }

    [Test]
    [TestCase(FurnitureType.CArm)]
    [TestCase(FurnitureType.Closet)]
    [TestCase(FurnitureType.Door)]
    [TestCase(FurnitureType.PatientTable)]
    [TestCase(FurnitureType.ProtectionWall)]
    [TestCase(FurnitureType.Table)]
    [TestCase(FurnitureType.None)]
    public void HoldsCorrectTypeOfFurniture_Test(FurnitureType expected)
    {
        FurnitureInfo info = new FurnitureInfo(expected.ToString(), expected.ToString(), new KeyCode[] { });

        FurnitureTriggerInfo.SetActiveFurniture(expected, info);

        Assert.AreEqual(expected, FurnitureTriggerInfo.Type);
    }

    [Test]
    public void TypeIsNoneAfterDeactivateIsCalled_Test()
    {
        FurnitureTriggerInfo.Type = FurnitureType.CArm;

        FurnitureTriggerInfo.DeactivateFurniture();

        Assert.AreEqual(FurnitureType.None, FurnitureTriggerInfo.Type);
    }

    [Test]
    public void FurnitureInfoUIIsActiveWhenFurnitureIsTriggerd_Test()
    {
        FurnitureInfo info = new FurnitureInfo("example", "example", new KeyCode[] { });

        FurnitureTriggerInfo.SetActiveFurniture(FurnitureType.CArm, info);

        Assert.IsTrue(GameObject.Find("GUI/FurnitureInfo").activeSelf);
    }

    [Test]
    public void FurnitureInfoIsNotActiveAterFurnitureIsDeactivated_Test()
    {
        FurnitureTriggerInfo.DeactivateFurniture();

        Assert.IsFalse(GameObject.Find("GUI").transform.Find("FurnitureInfo").gameObject.activeSelf);
    }

    [Test]
    [TestCase(FurnitureType.CArm)]
    public void LoadsCorrectFurnitureNameIntoUIWhenIsTriggerd_Test(FurnitureType type)
    {
        string expected = type.ToString();
        FurnitureInfo info = new FurnitureInfo(expected, expected, new KeyCode[] { });

        FurnitureTriggerInfo.SetActiveFurniture(type, info);

        Assert.AreEqual(expected, GameObject.Find("GUI/FurnitureInfo/Name").GetComponent<TextMeshProUGUI>().text);
    }

    [Test]
    [TestCase(FurnitureType.CArm)]
    public void LoadsCorrectFurnitureDescriptionIntoUIWhenIsTriggerd_Test(FurnitureType type)
    {
        string expected = type.ToString();
        FurnitureInfo info = new FurnitureInfo(expected, expected, new KeyCode[] { });

        FurnitureTriggerInfo.SetActiveFurniture(type, info);

        Assert.AreEqual(expected, GameObject.Find("GUI/FurnitureInfo/Description").GetComponent<TextMeshProUGUI>().text);
    }

    [Test]
    [TestCase(FurnitureType.CArm, new KeyCode[] { KeyCode.A, KeyCode.F })]
    public void LoadsCorrectFurnitureKeyCodesIntoUIWhenIsTriggerd_Test(FurnitureType type, KeyCode[] keys)
    {
        string name = type.ToString();
        FurnitureInfo info = new FurnitureInfo(name, name, keys);

        FurnitureTriggerInfo.SetActiveFurniture(type, info);

        Assert.AreEqual(keys[0].ToString(), GameObject.Find("GUI/FurnitureInfo/Keys/1/KeyName").GetComponent<TextMeshProUGUI>().text);
        Assert.AreEqual(keys[1].ToString(), GameObject.Find("GUI/FurnitureInfo/Keys/2/KeyName").GetComponent<TextMeshProUGUI>().text);
    }

    [Test]
    public void LoadsCorrectTextIntoKeysWhenOnlyOneKeyIsGiven_Test()
    {
        string name = FurnitureType.CArm.ToString();
        FurnitureInfo info = new FurnitureInfo(name, name, new KeyCode[] { KeyCode.A });

        FurnitureTriggerInfo.SetActiveFurniture(FurnitureType.CArm, info);

        Assert.AreEqual(KeyCode.A.ToString(), GameObject.Find("GUI/FurnitureInfo/Keys/1/KeyName").GetComponent<TextMeshProUGUI>().text);
        Assert.AreEqual("", GameObject.Find("GUI/FurnitureInfo/Keys/2/KeyName").GetComponent<TextMeshProUGUI>().text);
    }

    [Test]
    public void LoadsCorrectTextIntoKeysWhenNoKeyIsGiven_Test()
    {
        string name = FurnitureType.CArm.ToString();
        FurnitureInfo info = new FurnitureInfo(name, name, new KeyCode[] {  });

        FurnitureTriggerInfo.SetActiveFurniture(FurnitureType.CArm, info);

        Assert.AreEqual("", GameObject.Find("GUI/FurnitureInfo/Keys/1/KeyName").GetComponent<TextMeshProUGUI>().text);
        Assert.AreEqual("", GameObject.Find("GUI/FurnitureInfo/Keys/2/KeyName").GetComponent<TextMeshProUGUI>().text);
    }

    [Test]
    public void ThrowsWarinignMessageWhenKeyCodesHasMoreThanTwoValues_Test()
    {
        FurnitureTriggerInfo.SetActiveFurniture(FurnitureType.CArm, new FurnitureInfo("example", "example", new KeyCode[] { KeyCode.A, KeyCode.B, KeyCode.C }));

        LogAssert.Expect(LogType.Warning, "The GUI can only handle two different keys!");
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.DestroyImmediate(gui);
    }
}