using System;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class HSBColorTest
{
    
    [Test]
    public void HSBConstructurHSBA()
    {
        float h = 0.5f;
        float s = 0.5f;
        float b = 1f;
        float a = 1f;

        HSBColor color = new HSBColor(h, s, b, a);

        Assert.AreEqual(h, color.h);
        Assert.AreEqual(s, color.s);
        Assert.AreEqual(b, color.b);
        Assert.AreEqual(a, color.a);
    }

    [Test]
    public void HSBConstructurHSB()
    {
        float h = 0.5f;
        float s = 0.5f;
        float b = 1f;

        HSBColor color = new HSBColor(h, s, b);

        Assert.AreEqual(h, color.h);
        Assert.AreEqual(s, color.s);
        Assert.AreEqual(b, color.b);
        Assert.AreEqual(1f, color.a);
    }

    [Test]
    public void HSBConstructorColor()
    {
        float r = 1f;
        float g = 0.4f;
        float b = 0.2f;
        Color col = new Color(r, g, b);

        HSBColor color = new HSBColor(col);

        float h = 1f;
        float s = 1f;
        float b_ = 1f;
        float a = 1f;

        Assert.AreEqual(h, color.h);
        Assert.AreEqual(s, color.s);
        Assert.AreEqual(b_, color.b);
        Assert.AreEqual(a, color.a);
    }
}

public class HSBColor
{
    public float h;
    public float s;
    public float b;
    public float a;

    public HSBColor(float h, float s, float b)
    {
        this.h = h;
        this.s = s;
        this.b = b;
        this.a = 1f;
    }

    public HSBColor(float h, float s, float b, float a)
    {
        this.h = h;
        this.s = s;
        this.b = b;
        this.a = a;
    }

    public HSBColor(Color col)
    {

    }
}
