using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HSVColor
{
    public float h;
    public float s;
    public float v;
    public float a;

    //public HSVColor(float h, float s, float v)
    //{
    //    this.h = h;
    //    this.s = s;
    //    this.v = v;
    //    this.a = 1f;
    //}

    //public HSVColor(float h, float s, float v, float a)
    //{
    //    this.h = h;
    //    this.s = s;
    //    this.v = v;
    //    this.a = a;
    //}

    public HSVColor(Color col)
    {
        Color.RGBToHSV(col, out this.h, out this.s, out this.v);
        this.a = 1f;
    }

    public HSVColor Set(float newH)
    {
        Color rgbColor = Color.HSVToRGB(newH, this.s, this.v);
        return new HSVColor(rgbColor);
    }

    public Color ToRGB()
    {
        return Color.HSVToRGB(this.h, this.s, this.v);
    }
}
