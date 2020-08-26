﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class FurnitureInfoTest
{
    [Test]
    [TestCase("Table", "You are able to move it around!", new KeyCode[] {KeyCode.F})]
    public void ConstructorName_Test(string expected, string description, KeyCode[] keys)
    {
        FurnitureInfo info = new FurnitureInfo(expected, description, keys);

        Assert.AreEqual(expected, info.Name);
    }

    [Test]
    [TestCase("Table", "You are able to move it around!", new KeyCode[] { KeyCode.F })]
    public void ConstructorDescription_Test(string name, string expected, KeyCode[] keys)
    {
        FurnitureInfo info = new FurnitureInfo(name, expected, keys);

        Assert.AreEqual(expected, info.Description);
    }

    [Test]
    [TestCase("Table", "You are able to move it around!", new KeyCode[] { KeyCode.F })]
    public void ConstructorKeyCodes_Test(string name, string description, KeyCode[] expected)
    {
        FurnitureInfo info = new FurnitureInfo(name, description, expected);

        Assert.AreEqual(expected, info.KeyCodes);
    }

    [Test]
    public void IsSerializeable_Test()
    {
        Assert.IsTrue(typeof(FurnitureInfo).IsSerializable);
    }
}