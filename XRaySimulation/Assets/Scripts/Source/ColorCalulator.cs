using System;
using System.Collections.Generic;
using UnityEngine;

public static class ColorCalculator
{
    private static readonly HSVColor startColor = new HSVColor(new Color(0f, 1f, 0f));
    private static float maxDose = 200000f;

    public static Color32 Calculate(float dose)
    {
        float baseH = startColor.h;

        //Debug.Log("BaseH: " + baseH);

        float stepSize = maxDose / 360f;
        float newH = (dose / maxDose) * stepSize + baseH;

        if (newH > 1f)
            newH -= 1f;

        //Debug.Log("stepSize: " + stepSize + ", newH" + newH);

        HSVColor hsvColor = startColor.Set(newH);

        //Debug.Log(hsvColor.h + " " + hsvColor.s + " " + hsvColor.v);

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
