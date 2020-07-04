using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ColorCalculatorTest
{
    [Test]
    [TestCase(10)]
    public void ReturnsExpectedColor(float dose, Color32 expected)
    {
        Color32 color = ColorCalculator.Calculate();

        Assert.AreEqual(color, (Color32) Color.white);
    }
}

public static class ColorCalculator
{
    public static Color32 Calculate()
    {
        //HSBColor

        return (Color32) Color.white;
    }
}
