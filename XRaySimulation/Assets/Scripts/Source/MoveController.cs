using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveController
{
    private Transform movedTransform;
    private float moveSpeed = 1f;

    public MoveController(Transform movedTransform)
    {
        this.movedTransform = movedTransform;
    }

    public void Move(float x, float z, float time)
    {
        //Maybe change to: Vector3.Right * x + Vector.Forward * z
        //then the rotation will be irreleveant to the movement
        Vector3 move = movedTransform.right * x + movedTransform.forward * z;
        move *= time * moveSpeed;

        movedTransform.Translate(move);
    }
}
