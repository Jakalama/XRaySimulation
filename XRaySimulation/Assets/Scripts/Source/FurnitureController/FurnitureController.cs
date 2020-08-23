using System;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureController
{
    protected Transform controlledTransform;

    public FurnitureController()
    {
        
    }

    public void SetTransform(Transform controlledTransform)
    {
        this.controlledTransform = controlledTransform;
    }

    public virtual void Interact(bool[] instructions, float time)
    {

    }
}