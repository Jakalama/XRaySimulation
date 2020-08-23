using System;
using System.Collections.Generic;
using UnityEngine;

public static class FurnitureControllerFactory
{
    public static FurnitureController Create(FurnitureType type)
    {
        switch (type)
        {
            case FurnitureType.None:
                return new FurnitureController();
            case FurnitureType.CArm:
                return new FurnitureController();
            case FurnitureType.Closet:
                return new FurnitureController();
            case FurnitureType.Door:
                return new FurnitureController();
            case FurnitureType.PatientTable:
                return new PatientTableController();
            case FurnitureType.ProtectionWall:
                return new MoveableController();
            case FurnitureType.Table:
                return new MoveableController();
        }

        return new FurnitureController();
     }
}

