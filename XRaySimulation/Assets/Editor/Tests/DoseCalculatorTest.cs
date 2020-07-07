using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class DoseCalculatorTest
{
    [Test]
    [TestCase(1, 152.980f)]
    [TestCase(2f, 38.245f)]
    [TestCase(2.1f, 34.689f)]
    [TestCase(0f, float.PositiveInfinity)]
    [TestCase(150f, 0.007f)]
    [TestCase(float.NaN, 0f)]
    [TestCase(-1f, 152.980f)]
    [TestCase(-150f, 0.007f)]
    public void CalculateDoseTest(float distance, float expected)
    {
        float dose = DoseCalculator.Calculate(distance);

        Assert.AreEqual(expected, dose);
    }
}