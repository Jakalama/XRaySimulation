using System;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System.Linq;

public class MeshContainerTest
{
    private MeshContainer container;
    private GameObject meshObj;

    private void SetUp(string prefabName)
    {
        GameObject docMeshPref = Resources.Load<GameObject>(prefabName);
        meshObj = GameObject.Instantiate(docMeshPref);
        container = new MeshContainer(meshObj.transform);
    }

    [Test]
    [TestCase("Prefabs/Mock_simple")]
    [TestCase("Prefabs/Mock_complex")]
    public void MeshContainerExists_Test(string mockName)
    {
        SetUp(mockName);
        Assert.IsNotNull(container);
    }

    [Test]
    [TestCase("Prefabs/Mock_simple")]
    [TestCase("Prefabs/Mock_complex")]
    public void GameObjectHasMeshFilterComponent_Test(string mockName)
    {
        SetUp(mockName);
        Assert.IsNotNull(meshObj.GetComponent<MeshFilter>());
    }

    [Test]
    [TestCase("Prefabs/Mock_simple")]
    [TestCase("Prefabs/Mock_complex")]
    public void GetVerticesReturnsNotNull_Test(string mockName)
    {
        SetUp(mockName);
        Assert.IsNotNull(container.GetVertices());
    }

    [Test]
    [TestCase("Prefabs/Mock_simple", 24)]
    [TestCase("Prefabs/Mock_complex", 54)]
    public void GetVerticesReturnsCorrectNumberOfVertices_Test(string mockName, int expected)
    {
        SetUp(mockName);
        Vector3[] vertices = container.GetVertices();

        Assert.AreEqual(expected, vertices.Length);
    }

    [Test]
    [TestCase("Prefabs/Mock_simple")]
    [TestCase("Prefabs/Mock_complex")]
    public void GetNormalsReturnsNotNull_Test(string mockName)
    {
        SetUp(mockName);
        Assert.IsNotNull(container.GetNormals());
    }

    [Test]
    [TestCase("Prefabs/Mock_simple", 24)]
    [TestCase("Prefabs/Mock_complex", 54)]
    public void GetNormalsReturnsCorrectNumberOfNormals_Test(string mockName, int expected)
    {
        SetUp(mockName);
        Vector3[] normals = container.GetNormals();

        Assert.AreEqual(expected, normals.Length);
    }

    [Test]
    [TestCase("Prefabs/Mock_simple", 0, 0f, 1f, 0f)]
    [TestCase("Prefabs/Mock_simple", 1, 0f, 1f, 0f)]
    [TestCase("Prefabs/Mock_simple", 2, 0f, 1f, 0f)]
    [TestCase("Prefabs/Mock_simple", 3, 0f, 1f, 0f)]
    public void MeshNormalsAreCorrect_Test(string mockName, int index, float x, float y, float z)
    {
        SetUp(mockName);
        Vector3 expected = new Vector3(x, y, z);
        Vector3 normal = container.GetNormals()[index];

        Assert.AreEqual(expected.x, normal.x, delta: 0.001f);
        Assert.AreEqual(expected.y, normal.y, delta: 0.001f);
        Assert.AreEqual(expected.z, normal.z, delta: 0.001f);
    }

    [Test]
    [TestCase("Prefabs/Mock_simple", 0, -0.0087f, 0.4999f, 0.866f)]
    [TestCase("Prefabs/Mock_simple", 1, -0.0087f, 0.4999f, 0.866f)]
    [TestCase("Prefabs/Mock_simple", 2, -0.0087f, 0.4999f, 0.866f)]
    [TestCase("Prefabs/Mock_simple", 3, -0.0087f, 0.4999f, 0.866f)]
    public void MeshNormalsAreCorrectAfterRotation_Test(string mockName, int index, float x, float y, float z)
    {
        SetUp(mockName);
        meshObj.transform.Rotate(new Vector3(60f, -1f, 45.7f));

        Vector3 expected = new Vector3(x, y, z);
        Vector3 normal = container.GetNormals()[index];

        Assert.AreEqual(expected.x, normal.x, delta: 0.001f);
        Assert.AreEqual(expected.y, normal.y, delta: 0.001f);
        Assert.AreEqual(expected.z, normal.z, delta: 0.001f);
    }
}
