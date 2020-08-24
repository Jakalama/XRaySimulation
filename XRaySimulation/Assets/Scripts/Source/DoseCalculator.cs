using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DoseCalculator
{
    private static float sourceVoltage = 120f;
    private static float electronCharge = 1.602e-19f;

    public static float Calculate(float distance)
    {
        if (Double.IsNaN(distance))
            return 0f;

        distance = Math.Abs(distance);

        float sourceEnergy = sourceVoltage * electronCharge;
        float intensity = sourceEnergy / (4f * (float)Math.PI * distance * distance);
        intensity /= 1e-20f;

        //Debug.Log(distance + ", " + intensity);

        intensity = (float)Math.Round(intensity, 3);

        return intensity;
    }

    public static float[] Calculate(float[] distances)
    {
        int num = distances.Length;
        float[] doses = new float[num];

        for (int i = 0; i < num; i++)
        {
            doses[i] = Calculate(distances[i]);
        }

        return doses;
    }
}
