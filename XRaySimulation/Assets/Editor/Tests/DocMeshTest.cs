using System;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System.Linq;

public class DocMeshTest
{
    private void SetDocMesh(string name)
    {
        DocMesh dm = null;
        GameObject docMeshPref = Resources.Load<GameObject>(name);
        GameObject docMeshObj = GameObject.Instantiate(docMeshPref);
        dm = docMeshObj.GetComponent<DocMesh>();
        DocMesh.Instance = dm;
        DocMesh.Instance.Init();
    }

    [SetUp]
    public void BeforeEveryTest()
    {

    }

    [Test]
    [TestCase("Prefabs/Doc")]
    [TestCase("Prefabs/Mock_complex")]
    public void DocMeshInstanceExists_Test(string mockName)
    {
        SetDocMesh(mockName);
        DocMesh dm = DocMesh.Instance;

        Assert.IsNotNull(dm);
    }

    [Test]
    [TestCase("Prefabs/Doc")]
    [TestCase("Prefabs/Mock_complex")]
    public void DocMeshVerticeDataExists_Test(string mockName)
    {
        SetDocMesh(mockName);
        DocMesh dm = DocMesh.Instance;

        Assert.IsNotNull(dm.VerticeData);
    }

    [Test]
    [TestCase("Prefabs/Doc")]
    [TestCase("Prefabs/Mock_complex")]
    public void DocMeshRelevantVerticesExists_Test(string mockName)
    {
        SetDocMesh(mockName);
        DocMesh dm = DocMesh.Instance;

        Assert.IsNotNull(dm.RelevantVertices);
    }

    [Test]
    [TestCase("Prefabs/Doc")]
    [TestCase("Prefabs/Mock_complex")]
    public void DocMeshVerticesIsNotNull_Test(string mockName)
    {
        SetDocMesh(mockName);
        DocMesh dm = DocMesh.Instance;
        Vector3[] vertices = dm.GetVertices();

        Assert.IsNotNull(vertices);
    }

    [Test]
    [TestCase("Prefabs/Doc", 24)]
    [TestCase("Prefabs/Mock_complex", 54)]
    public void SimpleDocMeshHasCorrectNumOfVertices_Test(string mockName, int expected)
    {
        SetDocMesh(mockName);
        DocMesh dm = DocMesh.Instance;
        Vector3[] vertices = dm.GetVertices();

        Assert.AreEqual(expected, vertices.Length);
    }

    [Test]
    [TestCase("Prefabs/Doc")]
    [TestCase("Prefabs/Mock_complex")]
    public void DocMeshRelevantVerticesIsNotNull_Test(string mockName)
    {
        SetDocMesh(mockName);
        DocMesh dm = DocMesh.Instance;
        Vector3[] vertices = dm.GetVertices();
        dm.UpdateRelevantVertices(vertices);

        Assert.IsNotNull(dm.RelevantVertices);
    }

    [Test]
    [TestCase("Prefabs/Doc", 4)]
    [TestCase("Prefabs/Mock_complex", 16)]
    public void DocMeshHasCorrectNumOfRelevantVertices_Test(string mockName, int expected)
    {
        SetDocMesh(mockName);
        DocMesh dm = DocMesh.Instance;
        Vector3[] vertices = dm.GetVertices();
        dm.UpdateRelevantVertices(vertices);

        //Debug.Log(dm.RelevantVertices.Count);
        
        Assert.AreEqual(expected, dm.RelevantVertices.Count);
    }

    [Test]
    [TestCase("Prefabs/Doc")]
    [TestCase("Prefabs/Mock_complex")]
    public void DocMeshStoreMultipleDosesForVertices(string mockName)
    {
        SetDocMesh(mockName);
        DocMesh dm = DocMesh.Instance;

        dm.VerticeData = new VertexData[3]
        {
            new VertexData(new Vector3(10f, 10f, 10f), 0f ),
            new VertexData(new Vector3(8f, 1f, -1f), 2f ),
            new VertexData(new Vector3(2f, 1f, 1.5f), 10f )
        };

        float[] doses = new float[] { 3f, 2f, 1.256987f };
        dm.StoreDoses(doses);

        Assert.AreEqual(new float[] { 3f, 4f, 11.256987f}, dm.VerticeData.Select(x => x.Dose).ToArray());
    }

    [Test]
    [TestCase("Prefabs/Doc")]
    [TestCase("Prefabs/Mock_complex")]
    public void DocMeshAverageDose(string mockName)
    {
        SetDocMesh(mockName);
        DocMesh dm = DocMesh.Instance;

        dm.VerticeData = new VertexData[3]
        {
            new VertexData(new Vector3(10f, 10f, 10f), 0f ),
            new VertexData(new Vector3(8f, 1f, -1f), 2f ),
            new VertexData(new Vector3(2f, 1f, 1.5f), 10f )
        };

        float avg = (0f + 2f + 10f) / 3f;

        Assert.AreEqual(avg, dm.AverageDose);
    }
}
