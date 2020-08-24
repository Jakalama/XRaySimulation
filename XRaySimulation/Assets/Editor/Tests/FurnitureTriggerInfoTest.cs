using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

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

    public void FurnitureInfoUIIsActiveWhenFurnitureIsTriggerd_Test()
    {
        FurnitureTriggerInfo.SetActiveFurniture(FurnitureType.CArm, new FurnitureInfo());

        Assert.IsTrue(GameObject.Find("GUI/FurnitureInfo").activeSelf);
    }

    [TestCase(FurnitureType.CArm)]
    public void LoadsCorrectFurnitureNameIntoUIWhenIsTriggerd_Test(FurnitureType type)
    {
        string expected = type.ToString();
        FurnitureInfo info = new FurnitureInfo(expected, expected, new KeyCode[] { });

        FurnitureTriggerInfo.SetActiveFurniture(type, info);

        Assert.AreEqual(expected, GameObject.Find("GUI/FurnitureInfo/Name").GetComponent<TextMeshProUGUI>().text);
    }

    [TestCase(FurnitureType.CArm)]
    public void LoadsCorrectFurnitureDescriptionIntoUIWhenIsTriggerd_Test(FurnitureType type)
    {
        string expected = type.ToString();
        FurnitureInfo info = new FurnitureInfo(expected, expected, new KeyCode[] { });

        FurnitureTriggerInfo.SetActiveFurniture(type, info);

        Assert.AreEqual(expected, GameObject.Find("GUI/FurnitureInfo/Description").GetComponent<TextMeshProUGUI>().text);
    }

    [TestCase(FurnitureType.CArm, new KeyCode[] { KeyCode.A, KeyCode.F })]
    public void LoadsCorrectFurnitureKeyCodesIntoUIWhenIsTriggerd_Test(FurnitureType type, KeyCode[] keys)
    {
        string name = type.ToString();
        FurnitureInfo info = new FurnitureInfo(name, name, keys);

        FurnitureTriggerInfo.SetActiveFurniture(type, info);

        Assert.AreEqual(keys[0].ToString(), GameObject.Find("GUI/FurnitureInfo/Keys/1/KeyName").GetComponent<TextMeshProUGUI>().text);
        Assert.AreEqual(keys[1].ToString(), GameObject.Find("GUI/FurnitureInfo/Keys/2/KeyName").GetComponent<TextMeshProUGUI>().text);
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.DestroyImmediate(gui);
    }
}