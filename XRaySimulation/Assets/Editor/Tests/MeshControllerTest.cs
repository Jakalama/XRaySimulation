using System;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System.Linq;

public class MeshControllerTest
{
    private GameObject docMeshObj;
    private MeshController controller;

    // Can't use normal Setup, because one test use different mocks.
    // The default SetUp-Method can't have parameters.
    public void SetUp(string mockName = "Mock_simple")
    {
        GameObject docMeshPref = Resources.Load<GameObject>("Prefabs/" + mockName);
        docMeshObj = GameObject.Instantiate(docMeshPref);

        MeshContainer container = new MeshContainer(docMeshPref.transform);
        controller = new MeshController(container, docMeshPref.transform);
    }

    [Test]
    public void MeshContainerIsNotNull_Test()
    {
        SetUp();

        System.Reflection.FieldInfo info = controller.GetType().GetField("meshContainer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        MeshContainer mc = (MeshContainer) info.GetValue(controller);

        Assert.IsNotNull(mc);
    }

    [Test]
    public void MeshTransformIsNotNull_Test()
    {
        SetUp();

        System.Reflection.FieldInfo info = controller.GetType().GetField("meshTransform", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Transform transform = (Transform) info.GetValue(controller);

        Assert.IsNotNull(transform);
    }

    [Test]
    public void VerticeDataIsNotNull_Test()
    {
        SetUp();

        Assert.IsNotNull(controller.VerticeData);
    }

    [Test]
    public void RelevantVerticesIsNotNull_Test()
    {
        SetUp();

        Assert.IsNotNull(controller.RelevantVertices);
    }

    [Test]
    public void RelevantVerticesIsNotNullAfterUpdate_Test()
    {
        SetUp();

        controller.UpdateVertices(new Vector3());

        Assert.IsNotNull(controller.RelevantVertices);
    }

    [Test]
    [TestCase("Mock_simple", 4)]
    [TestCase("Mock_complex", 16)]
    public void RelevantVerticesHasCorrectNumberAfterUpdateWithSourceOnXAxis_Test(string mockName, int expected)
    {
        SetUp(mockName);

        controller.UpdateVertices(new Vector3(5f, 0f, 0f));

        Assert.AreEqual(expected, controller.RelevantVertices.Count);
    }

    [Test]
    [TestCase("Mock_simple", 4)]
    [TestCase("Mock_complex", 9)]
    public void RelevantVerticesHasCorrectNumberAfterUpdateWithSourceOnYAxis_Test(string mockName, int expected)
    {
        SetUp(mockName);

        controller.UpdateVertices(new Vector3(0f, 5f, 0f));

        Assert.AreEqual(expected, controller.RelevantVertices.Count);
    }

    [Test]
    [TestCase("Mock_simple", 4)]
    [TestCase("Mock_complex", 20)]
    public void RelevantVerticesHasCorrectNumberAfterUpdateWithSourceOnZAxis_Test(string mockName, int expected)
    {
        SetUp(mockName);

        controller.UpdateVertices(new Vector3(0f, 0f, 5f));

        Assert.AreEqual(expected, controller.RelevantVertices.Count);
    }

    [Test]
    [TestCase("Mock_simple", 4)]
    [TestCase("Mock_complex", 16)]
    public void RelevantVerticesHasCorrectNumberAfterUpdateWithSourceOnNegativeXAxis_Test(string mockName, int expected)
    {
        SetUp(mockName);

        controller.UpdateVertices(new Vector3(-5f, 0f, 0f));

        Assert.AreEqual(expected, controller.RelevantVertices.Count);
    }

    [Test]
    [TestCase("Mock_simple", 4)]
    [TestCase("Mock_complex", 9)]
    public void RelevantVerticesHasCorrectNumberAfterUpdateWithSourceOnnegativeYAxis_Test(string mockName, int expected)
    {
        SetUp(mockName);

        controller.UpdateVertices(new Vector3(0f, -5f, 0f));

        Assert.AreEqual(expected, controller.RelevantVertices.Count);
    }

    [Test]
    [TestCase("Mock_simple", 4)]
    [TestCase("Mock_complex", 16)]
    public void RelevantVerticesHasCorrectNumberAfterUpdateWithSourceOnNegativeZAxis_Test(string mockName, int expected)
    {
        SetUp(mockName);

        controller.UpdateVertices(new Vector3(0f, 0f, -5f));

        Assert.AreEqual(expected, controller.RelevantVertices.Count);
    }

    [Test]
    public void RelevantVertexPositionsReturnsCorrectNumberOfPositions_Test()
    {
        SetUp();

        controller.RelevantVertices = new List<int> { 0, 1, 2 };
        controller.VerticeData = new VertexData[]
        {
            new VertexData(new Vector3(1f, 0f, 0f), 0f),
            new VertexData(new Vector3(0f, 1f, 0f), 0f),
            new VertexData(new Vector3(0f, 0f, 1f), 0f),
        };

        Assert.AreEqual(3, controller.GetRelevantVerticePositions().Length);
    }

    [Test]
    public void RelevantVertexPositionsReturnsCorrectPositions_Test()
    {
        SetUp();

        VertexData[] expected = new VertexData[]
        {
            new VertexData(new Vector3(1f, 0f, 0f), 0f),
            new VertexData(new Vector3(0f, 1f, 0f), 0f),
            new VertexData(new Vector3(0f, 0f, 1f), 0f),
        };

        controller.RelevantVertices = new List<int> { 0, 1, 2 };
        controller.VerticeData = expected;

        Assert.AreEqual(expected[0].Position, controller.GetRelevantVerticePositions()[0]);
        Assert.AreEqual(expected[1].Position, controller.GetRelevantVerticePositions()[1]);
        Assert.AreEqual(expected[2].Position, controller.GetRelevantVerticePositions()[2]);
    }

    [Test]
    public void StoreDoses_Test()
    {
        SetUp();

        controller.VerticeData = new VertexData[3]
        {
            new VertexData(new Vector3(10f, 10f, 10f), 0f ),
            new VertexData(new Vector3(8f, 1f, -1f), 2f ),
            new VertexData(new Vector3(2f, 1f, 1.5f), 10f )
        };

        controller.RelevantVertices = new List<int>()
        {
            0, 1, 2
        };

        float[] doses = new float[] { 3f, 2f, 1.256987f };

        controller.StoreDoses(doses);

        Assert.AreEqual(new float[] { 3f, 4f, 11.256987f }, controller.VerticeData.Select(x => x.Dose).ToArray());
    }

    [Test]
    public void AverageDose_Test()
    {
        SetUp();

        controller.VerticeData = new VertexData[3]
        {
            new VertexData(new Vector3(10f, 10f, 10f), 0f ),
            new VertexData(new Vector3(8f, 1f, -1f), 2f ),
            new VertexData(new Vector3(2f, 1f, 1.5f), 10f )
        };

        float avg = (0f + 2f + 10f) / 3f;

        Assert.AreEqual(avg, controller.AverageDose);
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(docMeshObj);
        controller = null;
    }
}