using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveController
{
    private Transform playerTransform;
    private CharacterController CharacterController;
    private float moveSpeed = 10f;

    public MoveController(Transform movedTransform)
    {
        this.playerTransform = movedTransform;
        CharacterController = this.playerTransform.GetComponent<CharacterController>();
    }

    public void Move(float x, float z, float time)
    {
        Vector3 moveValue = playerTransform.right * x + playerTransform.forward * z;
        moveValue *= time * moveSpeed;

        CharacterController.Move(moveValue);

    }
}
