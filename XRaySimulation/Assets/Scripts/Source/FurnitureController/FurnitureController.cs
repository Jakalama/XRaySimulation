using System;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureController
{
    public bool isTriggerd;

    protected Transform controlledTransform;

    public FurnitureController(Transform controlledTransform)
    {
        this.controlledTransform = controlledTransform;
    }

    public virtual void Interact(bool[] instructions, float time)
    {

    }

    public void Activate(FurnitureType type, FurnitureInfo info)
    {
        if (FurnitureTriggerInfo.Type == FurnitureType.None)
        {
            isTriggerd = true;
            FurnitureTriggerInfo.SetActiveFurniture(type, info);
        }
    }

    public void Deactivate()
    {
        if (isTriggerd)
        {
            isTriggerd = false;
            FurnitureTriggerInfo.DeactivateFurniture();
        }
    }
}