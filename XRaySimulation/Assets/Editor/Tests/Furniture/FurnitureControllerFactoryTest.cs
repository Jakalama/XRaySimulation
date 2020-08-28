using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class FurnitureControllerFactoryTest
{
    [Test]
    public void FactoryReturnIsNotNull_Test()
    {
        GameObject obj = new GameObject();
        FurnitureController controller = FurnitureControllerFactory.Create(FurnitureType.None, obj.transform);

        Assert.IsNotNull(controller);
    }

    [Test]
    //[TestCase(typeof(ActivationController), FurnitureType.CArm)]
    //[TestCase(typeof(ActivationController), FurnitureType.Closet)]
    //[TestCase(typeof(ActivationController), FurnitureType.Door)]
    [TestCase(typeof(PatientTableController), FurnitureType.PatientTable)]
    [TestCase(typeof(MoveableController), FurnitureType.ProtectionWall)]
    [TestCase(typeof(MoveableController), FurnitureType.Table)]
    public void ReturnedControllerIsOfCorrectType_Test(Type type, FurnitureType furnitureType)
    {
        GameObject obj = new GameObject();
        FurnitureController controller = FurnitureControllerFactory.Create(furnitureType, obj.transform);

        Assert.AreEqual(type, controller.GetType());
    }
}