using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HSVColor
{
    public float h;
    public float s;
    public float v;

    public HSVColor(Color col)
    {
        Color.RGBToHSV(col, out this.h, out this.s, out this.v);
    }

    public HSVColor GetNew(float newH)
    {
        Color rgbColor = Color.HSVToRGB(newH, this.s, this.v);
        return new HSVColor(rgbColor);
    }

    public Color ToRGB()
    {
        return Color.HSVToRGB(this.h, this.s, this.v);
    }
}
