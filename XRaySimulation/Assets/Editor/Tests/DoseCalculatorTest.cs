using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class DoseCalculatorTest
{
    [Test]
    [TestCase(1, 1.333f)]
    [TestCase(2f, 0.333f)]
    [TestCase(2.1f, 0.302f)]
    [TestCase(0f, float.PositiveInfinity)]
    [TestCase(150f, 0f)]
    [TestCase(float.NaN, 0f)]
    [TestCase(-1f, 1.333f)]
    [TestCase(-150f, 0f)]
    public void CalculateDose_Test(float distance, float expected)
    {
        float dose = DoseCalculator.Calculate(distance, 100f, 1f);

        Assert.AreEqual(expected, dose);
    }

    [Test]
    [TestCase(new float[] { }, new float[] { })]
    [TestCase(new float[] { 1, 2f, 2.1f, 0f, 150f, float.NaN, -1f, -150f}, new float[] { 1.333f, 0.333f, 0.302f, float.PositiveInfinity, 0f, 0f, 1.333f, 0f})]
    public void CalculateDoses_Test(float[] distances, float[] expected)
    {
        float[] doses = DoseCalculator.Calculate(distances, 100f, 1f);

        Assert.AreEqual(expected, doses);
    }
}