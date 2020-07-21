using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DocMovement : MonoBehaviour
{
    private MoveController MoveController;

    private void Start()
    {
        MoveController = new MoveController(this.transform);
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal Movement");
        float z = Input.GetAxis("Vertical Movement");

        MoveController.Move(x, z, Time.fixedDeltaTime);
    }
}
