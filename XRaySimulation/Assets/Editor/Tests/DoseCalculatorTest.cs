using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class DoseCalculatorTest
{
    [Test]
    [TestCase(1, 0.796f)]
    [TestCase(2f, 0.199f)]
    [TestCase(2.1f, 0.180f)]
    [TestCase(0f, float.PositiveInfinity)]
    [TestCase(150f, 0f)]
    [TestCase(float.NaN, 0f)]
    [TestCase(-1f, 0.796f)]
    [TestCase(-150f, 0f)]
    public void CalculateDoseTest(float distance, float expected)
    {
        float dose = DoseCalculator.Calculate(distance);

        Assert.AreEqual(expected, dose);
    }
}