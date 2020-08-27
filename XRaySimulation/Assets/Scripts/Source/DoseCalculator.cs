using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DoseCalculator
{
    private const float WEIGHT = 75f;

    public static float Calculate(float distance, float watt, float time)
    {
        if (Double.IsNaN(distance))
            return 0f;

        distance = Mathf.Abs(distance);

        float sourceEnergy = watt * time;
        float energyDose = sourceEnergy / WEIGHT;
        energyDose = energyDose / (distance * distance);

        energyDose = (float) Math.Round(energyDose, 3);

        return energyDose;
    }

    public static float[] Calculate(float[] distances, float sourceEnegery, float time)
    {
        int num = distances.Length;
        float[] doses = new float[num];

        for (int i = 0; i < num; i++)
        {
            doses[i] = Calculate(distances[i], sourceEnegery, time);
        }

        return doses;
    }
}

