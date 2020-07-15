using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class RayTracerTest
{
    private RayTracer rayTracer;
    private Vector3 origin = new Vector3(0f, 0f, 0f);
    private Vector3 destination = new Vector3(3f, 0f, 0f);

    [OneTimeSetUp]
    public void OnceBeforeTests()
    {
        LoadTestScene();

        GameObject rayTracerPref = Resources.Load<GameObject>("Prefabs/XRaySource");
        GameObject rayTracerObj = GameObject.Instantiate(rayTracerPref);
        rayTracer = rayTracerObj.GetComponent<RayTracer>();
        RayTracer.Instance = rayTracer;
    }

    public IEnumerator LoadTestScene()
    {
        yield return UnloadCurrentScene();

        yield return new WaitForSeconds(1f);

        EditorSceneManager.OpenScene("Assets/Scenes/TestScene.unity");
    }

    public IEnumerator UnloadCurrentScene()
    {

        AsyncOperation unloadOperation = EditorSceneManager.UnloadSceneAsync(EditorSceneManager.GetActiveScene());

        while (!unloadOperation.isDone)
            yield return null;
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

    [Test]
    [TestCase("Mock_simple")]
    [TestCase("Mock_complex")]
    public void GetDistances_Test(string mockName)
    {
        GameObject mock = CreateMock("Doc", mockName);
        RayTracer rt = RayTracer.Instance;

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

        float[] distances = rt.GetDistances(positions);

        Assert.AreEqual(expected, distances);
    }

    private GameObject CreateMock(string name, string mock)
    {
        GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/" + mock), destination, Quaternion.identity);
        obj.transform.tag = name;
        obj.tag = name;

        return obj;
    }
}


