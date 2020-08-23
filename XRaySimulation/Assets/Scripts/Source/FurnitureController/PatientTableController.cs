using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatientTableController : FurnitureController
{
    public PatientTableController()
    {
        
    }

    override public void Interact(bool[] instructions, float time)
    {
        if (instructions.Length < 2 || instructions.Length > 3)
            return;

        if (instructions[0])
            controlledTransform.Translate(Vector3.up * time);
        else if (instructions[1])
            controlledTransform.Translate(Vector3.down * time);
    }
}