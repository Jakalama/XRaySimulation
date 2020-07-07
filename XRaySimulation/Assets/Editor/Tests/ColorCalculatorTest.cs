using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ColorCalculatorTest
{
    [Test]
    [TestCase(0f, 0f, 1f, 0f)]
    [TestCase(10f, 0f, 1f, 43f / 255f)]
    [TestCase(200f, 1f, 0f, 0f)]
    public void ReturnsExpectedColor(float dose, float r, float g, float b)
    {
        Color32 rgbColor = new Color(r, g, b);
        Color32 color = ColorCalculator.Calculate(dose);

        Assert.AreEqual(rgbColor, color);
    }
}

public static class ColorCalculator
{
    private static readonly HSVColor startColor = new HSVColor(new Color(0f, 1f, 0f));
    private static float maxDose = 200f;

    public static Color32 Calculate(float dose)
    {
        float baseH = startColor.h;

        //Debug.Log("BaseH: " + baseH);

        float stepSize = maxDose / 360f;
        float newH = (dose / maxDose) * stepSize + baseH;

        if (newH > 1f)
            newH -= 1f;

        Debug.Log("stepSize: " + stepSize + ", newH" + newH);

        HSVColor hsvColor = startColor.Set(newH);

        //Debug.Log(hsvColor.h + " " + hsvColor.s + " " + hsvColor.v);

        return (Color32) hsvColor.ToRGB();
    }
}
