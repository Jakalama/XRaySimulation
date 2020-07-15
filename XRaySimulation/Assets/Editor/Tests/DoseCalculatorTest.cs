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
    public void CalculateDose_Test(float distance, float expected)
    {
        float dose = DoseCalculator.Calculate(distance);

        Assert.AreEqual(expected, dose);
    }

    [Test]
    [TestCase(new float[] { }, new float[] { })]
    [TestCase(new float[] { 1, 2f, 2.1f, 0f, 150f, float.NaN, -1f, -150f}, new float[] { 152.980f, 38.245f, 34.689f, float.PositiveInfinity, 0.007f, 0f, 152.980f, 0.007f})]
    public void CalculateDoses_Test(float[] distances, float[] expected)
    {
        float[] doses = DoseCalculator.Calculate(distances);

        Assert.AreEqual(expected, doses);
    }
}