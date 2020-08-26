using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class RayTracerTest
{
    private RayTracer tracer;
    private Transform source;

    private readonly Vector3 MOCK_PLACE = Vector3.right * 3f;

    [SetUp]
    public void Setup()
    {
        source = GameObject.Instantiate(new GameObject("Source")).transform;
        tracer = new RayTracer(source.transform);
    }

    [Test]
    public void RayTracerIsNotNull_Test()
    {
        Assert.IsNotNull(tracer);
    }

    [Test]
    public void GetDistances_Test()
    {
        Vector3[] positions = new Vector3[]
        {
            new Vector3(0f, 0f, 0f),
            new Vector3(1f, 0f, 0f),
            new Vector3(0f, 1f, 0f),
            new Vector3(0f, 0f, 1f),
            new Vector3(-1f, 0f, 0f),
            new Vector3(0f, -1f, 0f),
            new Vector3(0f, 0f, -1f),
            new Vector3(0f, 2f, 0f),
            new Vector3(1f, 1f, 1f),
            new Vector3(0f, 0f, 100f)
        };

        float[] expected = new float[]
        {
            0f, 1f, 1f, 1f, 1f, 1f, 1f, 2f, 1.732f, 100f
        };

        float[] distances = tracer.GetDistances(positions);

        Assert.AreEqual(expected, distances);
    }

    [Test]
    public void CreateRayReturnsFalseWhenNoHitDetected_Test()
    {
        bool hitted = tracer.CreateRay(MOCK_PLACE);

        Assert.IsFalse(hitted);
    }

    [Test]
    [TestCase("Mock_simple")]
    [TestCase("Mock_complex")]
    public void CreateRayReturnsTrueIfDocHitDetected_Test(string mockName)
    {
        GameObject mock = CreateMock(mockName, "Doc");

        bool hitted = tracer.CreateRay(MOCK_PLACE);

        Assert.IsTrue(hitted);

        GameObject.DestroyImmediate(mock);
    }

    [Test]
    [TestCase("Mock_simple")]
    [TestCase("Mock_complex")]
    public void CreateRayReturnsFalseIfOtherHitDetected_Test(string mockName)
    {
        GameObject mock = CreateMock(mockName, "Other");

        bool hitted = tracer.CreateRay(MOCK_PLACE);

        Assert.IsFalse(hitted);

        GameObject.DestroyImmediate(mock);
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.DestroyImmediate(source.gameObject);
        tracer = null;
    }

    private GameObject CreateMock(string prefabName, string tag)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + prefabName);
        GameObject mock = GameObject.Instantiate(prefab, MOCK_PLACE, Quaternion.identity);
        mock.name = tag;
        mock.tag = tag;

        return mock;
    }
}