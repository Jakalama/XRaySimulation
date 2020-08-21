using System;

public interface ICameraController
{
    void Rotate(float x, float y, float time);
    //void SetXRotation(float value);
    float GetYRotation();
}
