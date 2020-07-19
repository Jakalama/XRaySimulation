using System;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System.Linq;

public class MeshContainerTest
{
    private void SetMeshContainer(string name)
    {
        MeshContainer dm = null;
        GameObject docMeshPref = Resources.Load<GameObject>(name);
        GameObject docMeshObj = GameObject.Instantiate(docMeshPref);
        dm = docMeshObj.GetComponent<MeshContainer>();
        MeshContainer.Instance = dm;
    }

    [SetUp]
    public void BeforeEveryTest()
    {

    }

    [Test]
    [TestCase("Prefabs/Doc")]
    [TestCase("Prefabs/Mock_complex")]
    public void MeshContainerInstanceExists_Test(string mockName)
    {
        SetMeshContainer(mockName);
        MeshContainer dm = MeshContainer.Instance;

        Assert.IsNotNull(dm);
    }

    [Test]
    [TestCase("Prefabs/Doc")]
    [TestCase("Prefabs/Mock_complex")]
    public void MeshVerticesIsNotNull_Test(string mockName)
    {
        SetMeshContainer(mockName);
        MeshContainer dm = MeshContainer.Instance;
        Vector3[] vertices = dm.GetVertices();

        Assert.IsNotNull(vertices);
    }

    [Test]
    [TestCase("Prefabs/Doc", 24)]
    [TestCase("Prefabs/Mock_complex", 54)]
    public void MeshHasCorrectNumOfVertices_Test(string mockName, int expected)
    {
        SetMeshContainer(mockName);
        MeshContainer dm = MeshContainer.Instance;
        Vector3[] vertices = dm.GetVertices();

        Assert.AreEqual(expected, vertices.Length);
    }

    [Test]
    [TestCase("Prefabs/Doc")]
    [TestCase("Prefabs/Mock_complex")]
    public void MeshNormalsIsNotNull_Test(string mockName)
    {
        SetMeshContainer(mockName);
        MeshContainer dm = MeshContainer.Instance;
        Vector3[] normals = dm.GetNormals();

        Assert.IsNotNull(normals);
    }

    [Test]
    [TestCase("Prefabs/Doc", 24)]
    [TestCase("Prefabs/Mock_complex", 54)]
    public void MeshHasCorrectNumOfNormals_Test(string mockName, int expected)
    {
        SetMeshContainer(mockName);
        MeshContainer dm = MeshContainer.Instance;
        Vector3[] normals = dm.GetNormals();

        Assert.AreEqual(expected, normals.Length);
    }

    [Test]
    [TestCase("Prefabs/Doc", 0, 0f, 1f, 0f)]
    [TestCase("Prefabs/Doc", 1, 0f, 1f, 0f)]
    [TestCase("Prefabs/Doc", 2, 0f, 1f, 0f)]
    [TestCase("Prefabs/Doc", 3, 0f, 1f, 0f)]
    //[TestCase("Prefabs/Mock_complex", 0, -0.9f, 0f, 0.3f)]
    //[TestCase("Prefabs/Mock_complex", 1, -0.9f, 0f, 0.3f)]
    //[TestCase("Prefabs/Mock_complex", 2, -0.9f, 0f, 0.3f)]
    //[TestCase("Prefabs/Mock_complex", 3, -0.9f, 0f, 0.3f)]
    public void MeshNormalsAreCorrect(string mockName, int index, float x, float y, float z)
    {
        SetMeshContainer(mockName);
        MeshContainer dm = MeshContainer.Instance;
        Vector3 normal = dm.GetNormals()[index];

        Assert.AreEqual(new Vector3((float) x, (float) y, (float) z), normal);
    }
}
