using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class VertexDataTest
{
    private VertexData data;

    [SetUp]
    public void SetUp()
    {
        data = new VertexData(new Vector3(), 0f);
    }

    [Test]
    public void EmptyConstructor_Test()
    {
        data = new VertexData();

        Assert.IsNotNull(data);
    }

    [Test]
    public void Constructor_Test()
    {
        Assert.IsNotNull(data);
    }

    [Test]
    public void ConstructorPosition_Test()
    {
        Assert.IsNotNull(data.Position);
    }

    [Test]
    public void ConstructorDose_Test()
    {
        Assert.IsNotNull(data.Dose);
    }

    [Test]
    public void ConstructorVerticeWithSamePos_Test()
    {
        Assert.IsNotNull(data.VerticeWithSamePos);
    }

    [Test]
    public void PositionIsCorrect_Test()
    {
        Vector3 expected = new Vector3(1f, 2f, 3f);

        data = new VertexData(expected, 0f);

        Assert.AreEqual(expected, data.Position);
    }

    [Test]
    public void DoseIsCorrect_Test()
    {
        float expected = 5.1f;

        data = new VertexData(new Vector3(), expected);

        Assert.AreEqual(expected, data.Dose);
    }

    [Test]
    public void IsDirtyCorrect_Test()
    {
        Assert.IsFalse(data.isDirty);
    }

    [Test]
    public void VerticeWithSamePosIsCorrect_Test()
    {
        Assert.AreEqual(0, data.VerticeWithSamePos.Length);
    }
}