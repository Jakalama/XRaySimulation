using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class DocMovementTest
{
    [Test]
    public void DocMovementIsExistent()
    {
        GameObject go = GameObject.Instantiate(new GameObject());
        go.AddComponent(typeof(DocMovement));

        DocMovement dm = go.GetComponent<DocMovement>();

        Assert.IsNotNull(dm);
    }
}

public class DocMovement : MonoBehaviour
{
    private void Start()
    {
        

    }

    private void Update()
    {
        
    }
}
