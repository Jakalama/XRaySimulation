using System;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class HSVColorTest
{
    
    //[Test]
    //public void HSVConstructurHSVA()
    //{
    //    float h = 0.5f;
    //    float s = 0.5f;
    //    float v = 1f;
    //    float a = 1f;

    //    HSVColor hsvColor = new HSVColor(h, s, v, a);

    //    Assert.AreEqual(h, hsvColor.h);
    //    Assert.AreEqual(s, hsvColor.s);
    //    Assert.AreEqual(v, hsvColor.v);
    //    Assert.AreEqual(a, hsvColor.a);
    //}

    //[Test]
    //public void HSVConstructurHSV()
    //{
    //    float h = 0.5f;
    //    float s = 0.5f;
    //    float v = 1f;

    //    HSVColor hsvColor = new HSVColor(h, s, v);

    //    Assert.AreEqual(h, hsvColor.h);
    //    Assert.AreEqual(s, hsvColor.s);
    //    Assert.AreEqual(v, hsvColor.v);
    //    Assert.AreEqual(1f, hsvColor.a);
    //}

    [Test]
    public void HSVConstructorRGBColor()
    {
        float r = 1f;
        float g = 0.4f;
        float b = 0.2f;
        Color rgbColor = new Color(r, g, b);

        HSVColor hsbColor = new HSVColor(rgbColor);

        float h = 1f;
        float s = 1f;
        float v = 1f;
        float a = 1f;

        Color.RGBToHSV(rgbColor, out h, out s, out v);

        Assert.AreEqual(h, hsbColor.h);
        Assert.AreEqual(s, hsbColor.s);
        Assert.AreEqual(v, hsbColor.v);
        Assert.AreEqual(a, hsbColor.a);
    }
}