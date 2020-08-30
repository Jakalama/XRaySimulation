using System;
using System.Collections.Generic;
using UnityEngine;

public static class ColorCalculator
{
    public static Color BASE_COLOR = new Color(1f, 1f, 0f);
    private static readonly HSVColor startColor = new HSVColor(BASE_COLOR);
    private static float maxDose = 200000f;

    /// <summary>
    /// Calculates a Color32 based on the given dose.
    /// </summary>
    public static Color32 Calculate(float dose)
    {
        float baseH = startColor.h;

        float stepSize = maxDose / 360f;
        float newH = (dose / maxDose) * stepSize + baseH;
        newH = Mathf.Clamp(newH, 0f, 1f);

        HSVColor hsvColor = startColor.GetNew(newH);

        return (Color32)hsvColor.ToRGB();
    }

    /// <summary>
    /// Calculates a Color32 array based on the given doses.
    /// </summary>
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
