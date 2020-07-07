using System;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

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
    public void DocMeshVerticesExists_Test(string mockName)
    {
        SetDocMesh(mockName);
        DocMesh dm = DocMesh.Instance;

        Assert.IsNotNull(dm.Vertices);
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
}
