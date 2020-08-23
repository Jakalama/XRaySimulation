﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PatientTableTest
{
    private GameObject testObj;
    private Furniture furniture;
    private FurnitureController controller;

    [SetUp]
    public void Setup()
    {
        testObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/PatientTable"));
        furniture = testObj.GetComponent<Furniture>();
        controller = new PatientTableController();
        controller.SetTransform(testObj.transform);

        // set the Controller field of furniture, since it is set in the Start-Method
        System.Reflection.FieldInfo info = furniture.GetType().GetField("Controller", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        info.SetValue(furniture, controller);
    }

    [Test]
    [TestCase(true, false)]
    public void ArrowUpMovesTableUpWhenTableIsTriggerd_Test(bool up, bool down)
    {
        furniture.isTriggerd = true;

        Vector3 before = testObj.transform.position;

        controller.Interact(new bool[] { up, down }, 1f);

        Vector3 after = testObj.transform.position;

        Assert.Greater(after.y, before.y);
    }

    [Test]
    [TestCase(false, true)]
    public void ArrowDownMovesTableDownWhenTableIsTriggerd_Test(bool up, bool down)
    {
        furniture.isTriggerd = true;

        Vector3 before = testObj.transform.position;

        controller.Interact(new bool[] { up, down }, 1f);

        Vector3 after = testObj.transform.position;

        Assert.Less(after.y, before.y);
    }

    [Test]
    [TestCase(true, true)]
    public void ArrowUpAndArrowDownArePressedMovesTableUpWhenTableIsTriggerd_Test(bool up, bool down)
    {
        furniture.isTriggerd = true;

        Vector3 before = testObj.transform.position;

        controller.Interact(new bool[] { up, down }, 1f);

        Vector3 after = testObj.transform.position;

        Assert.Greater(after.y, before.y);
    }

    [Test]
    [TestCase(new bool[] { false, false, false })]
    [TestCase(new bool[] { true, true, true, true, true })]
    public void TheTableDontMoveWhenThereAreTooManyInstructions_Test(bool[] instructions)
    {
        furniture.isTriggerd = true;

        Vector3 before = testObj.transform.position;

        controller.Interact(instructions, 1f);

        Vector3 after = testObj.transform.position;

        Assert.AreEqual(after.y, before.y);
    }

    [Test]
    [TestCase(new bool[] { false })]
    [TestCase(new bool[] { true })]
    [TestCase(new bool[] { })]
    public void TheTableDontMoveWhenThereAreNotEnoughInstructions_Test(bool[] instructions)
    {
        furniture.isTriggerd = true;

        Vector3 before = testObj.transform.position;

        controller.Interact(instructions, 1f);

        Vector3 after = testObj.transform.position;

        Assert.AreEqual(after.y, before.y);
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.DestroyImmediate(testObj);
        controller = null;
    }
}