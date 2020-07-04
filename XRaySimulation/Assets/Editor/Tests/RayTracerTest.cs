using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class RayTracerTest
{
    private RayTracer rayTracer;
    private Vector3 origin = new Vector3(0f, 0f, 0f);
    private Vector3 destination = new Vector3(3f, 0f, 0f);

    [OneTimeSetUp]
    public void OnceBeforeTests()
    {
        GameObject rayTracerPref = Resources.Load<GameObject>("Prefabs/XRaySource");
        GameObject rayTracerObj = GameObject.Instantiate(rayTracerPref);
        rayTracer = rayTracerObj.GetComponent<RayTracer>();
        RayTracer.Instance = rayTracer;
    }

    [SetUp]
    public void BeforeVeryTest()
    {

    }

    [Test]
    public void CreateRayHits_Test()
    {
        RayTracer rt = RayTracer.Instance;
        bool hitted = rt.CreateRay(origin, destination);

        Assert.IsFalse(hitted);
    }

    [Test]
    public void CreateRayHitsDoc_Test()
    {
        GameObject mock = CreateMock("Doc", "Mock_simple");
        RayTracer rt = RayTracer.Instance;
        bool hitted = rt.CreateRay(origin, destination);

        Assert.IsTrue(hitted);
        GameObject.DestroyImmediate(mock);
    }

    [Test]
    public void CreateRayHitsOther_Test()
    {
        GameObject mock = CreateMock("Other", "Mock_simple");

        RayTracer rt = RayTracer.Instance;
        bool hitted = rt.CreateRay(origin, destination);

        Assert.IsFalse(hitted);
        GameObject.DestroyImmediate(mock);
    }

    [Test]
    [TestCase("Mock_simple")]
    [TestCase("Mock_complex")]
    public void CreateRaySourceToDoc_Test(string mockName)
    {
        GameObject mock = CreateMock("Doc", mockName);

        RayTracer rt = RayTracer.Instance;
        DocMesh dm = DocMesh.Instance;
        dm.Init();

        bool result = rt.CreateRaySourceToDoc(DocMesh.Instance.GetVertices()[0]);

        Assert.IsTrue(result);
    }

    private GameObject CreateMock(string name, string mock)
    {
        GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/" + mock), destination, Quaternion.identity);
        obj.transform.tag = name;
        obj.tag = name;

        return obj;
    }
}


