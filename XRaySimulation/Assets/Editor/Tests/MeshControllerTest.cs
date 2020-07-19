using System;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System.Linq;

public class MeshControllerTest
{
    private MeshController controller;

    [SetUp]
    public void SetUp()
    {
        MeshContainer container = null;
        GameObject docMeshPref = Resources.Load<GameObject>("Prefabs/Mock_simple");
        GameObject docMeshObj = GameObject.Instantiate(docMeshPref);
        container = docMeshObj.GetComponent<MeshContainer>();

        controller = new MeshController(container);
    }

    [TearDown]
    public void TearDown()
    {

    }

    [Test]
    public void MeshContainerIsNotNull_Test()
    {
        Assert.IsNotNull(controller.MeshContainer);
    }

    [Test]
    public void VerticeDataExists_Test()
    {
        Assert.IsNotNull(controller.VerticeData);
    }

    [Test]
    public void RelevantVerticesExists_Test()
    {
        Assert.IsNotNull(controller.RelevantVertices);
    }

    [Test]
    public void RelevantVerticesIsNotNullAfterUpdate_Test()
    {
        controller.UpdateRelevantVertices();

        Assert.IsNotNull(controller.RelevantVertices);
    }

    [Test]
    [TestCase("Prefabs/Mock_simple", 4)]
    [TestCase("Prefabs/Mock_complex", 16)]
    public void RelevantVerticesHasCorrectNumAfterUpdate_Test(string mockName, int expected)
    {
        MeshContainer conatiner = GetMeshContainer(mockName);
        controller.MeshContainer = conatiner;
        controller.UpdateRelevantVertices();

        Assert.AreEqual(expected, controller.RelevantVertices.Count);
    }

    [Test]
    public void RelevantVertexPositions_Test()
    {
        //ToDo:
        Assert.True(true);
    }

    [Test]
    public void StoreDoses_Test()
    {
        //ToDo: Should be TestCase
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
        controller.VerticeData = new VertexData[3]
        {
            new VertexData(new Vector3(10f, 10f, 10f), 0f ),
            new VertexData(new Vector3(8f, 1f, -1f), 2f ),
            new VertexData(new Vector3(2f, 1f, 1.5f), 10f )
        };

        float avg = (0f + 2f + 10f) / 3f;

        Assert.AreEqual(avg, controller.AverageDose);
    }

    private MeshContainer GetMeshContainer(string mockName)
    {
        MeshContainer dm = null;
        GameObject docMeshPref = Resources.Load<GameObject>(mockName);
        GameObject docMeshObj = GameObject.Instantiate(docMeshPref);
        dm = docMeshObj.GetComponent<MeshContainer>();

        return dm;
    }
}