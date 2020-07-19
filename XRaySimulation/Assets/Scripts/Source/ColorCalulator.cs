using System;
using System.Collections.Generic;
using UnityEngine;

public static class ColorCalculator
{
    private static readonly HSVColor startColor = new HSVColor(new Color(1f, 1f, 0f));
    private static float maxDose = 200000f;

    public static Color32 Calculate(float dose)
    {
        float baseH = startColor.h;

        float stepSize = maxDose / 360f;
        float newH = (dose / maxDose) * stepSize + baseH;
        newH = Mathf.Clamp(newH, 0f, 1f);

        HSVColor hsvColor = startColor.Set(newH);

        return (Color32)hsvColor.ToRGB();
    }

    public static Color32[] Calculate(float[] doses)
    {
        int num = doses.Length;
        Color32[] colors = new Color32[num];

        for (int i = 0; i < num; i++)
        {
            colors[i] = Calculate(doses[i]);
        }

        return colors;
    }
}
