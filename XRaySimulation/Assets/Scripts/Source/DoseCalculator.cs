using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DoseCalculator
{
    private static float SourceEnergy = 10f;

    public static float Calculate(float distance)
    {
        if (Double.IsNaN(distance))
            return 0f;

        distance = Math.Abs(distance);

        float dose = SourceEnergy / (4f * (float)Math.PI * distance * distance);

        //Debug.Log(distance + ", " + dose);

        dose = (float)Math.Round(dose, 3);


        return dose;
    }
}
