using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Constraints;
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

    [Test]
    [TestCase(new float[] { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f}, 5.5f)]
    [TestCase(new float[] { 1f, 2f, 1f, 1f, 1.5f, 2f}, 1.417f)]
    public void GetAVGDose_Test(float[] doses, float expected)
    {
        float actual = DoseCalculator.GetAVGDose(doses);

        Assert.AreEqual(expected, actual);
    }
}