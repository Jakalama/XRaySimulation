using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ColorCalculatorTest
{

    [Test]
    [TestCase(new float[] { }, new float[] { }, new float[] { }, new float[] { })]
    [TestCase(new float[] { 0f, 10f, 200f }, new float[] { 1f, 212f / 255f, 85f / 255f}, new float[] { 1f, 1f, 0f }, new float[] { 0f, 0f, 1f })]
    public void CalculateReturnsExpectedColors_Test(float[] doses, float[] r, float[] g, float [] b)
    {
        Color32[] rgbColors = GetColors(r, g, b);

        Color32[] colors = ColorCalculator.Calculate(doses);

        Assert.AreEqual(rgbColors, colors);
    }

    private Color32[] GetColors(float[] r, float[] g, float[] b)
    {
        Color32[] rgbColors = new Color32[r.Length];

        for (int i = 0; i < r.Length; i++)
        {
            rgbColors[i] = new Color(r[i], g[i], b[i]);
        }

        return rgbColors;
    }
}