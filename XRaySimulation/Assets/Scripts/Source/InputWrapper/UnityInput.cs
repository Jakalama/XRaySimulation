using System;
using UnityEngine;

public class UnityInput : IInputWrapper
{
    private static IInputWrapper input;
    public static IInputWrapper Instance
    {
        get
        {
            if (input == null)
                input = new UnityInput();

            return input;
        }
    }

    public bool GetKey(KeyCode code)
    {
        return Input.GetKey(code);
    }

    public bool GetKeyDown(KeyCode code)
    {
        return Input.GetKeyDown(code);
    }

    public float GetAxis(string axis)
    {
        return Input.GetAxis(axis);
    }
}
