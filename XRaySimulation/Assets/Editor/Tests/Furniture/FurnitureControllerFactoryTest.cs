using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class FurnitureControllerFactoryTest
{
    [Test]
    public void FactoryReturnIsNotNull_Test()
    {
        FurnitureController controller = FurnitureControllerFactory.Create(FurnitureType.None);

        Assert.IsNotNull(controller);
    }

    [Test]
    //[TestCase(typeof(CArmController), FurnitureType.CArm)]
    //[TestCase(typeof(ClosetController), FurnitureType.Closet)]
    //[TestCase(typeof(DoorController), FurnitureType.Door)]
    [TestCase(typeof(PatientTableController), FurnitureType.PatientTable)]
    [TestCase(typeof(MoveableController), FurnitureType.ProtectionWall)]
    //[TestCase(typeof(MoveableController), FurnitureType.Table)]
    public void ReturnedControllerIsOfCorrectType_Test(Type type, FurnitureType furnitureType)
    {
        FurnitureController controller = FurnitureControllerFactory.Create(furnitureType);

        Assert.AreEqual(type, controller.GetType());
    }
}