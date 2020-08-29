using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private MoveController MoveController;

    private void Start()
    {
        MoveController = new MoveController(this.transform);
    }

    private void FixedUpdate()
    {
        float x = UnityInput.Instance.GetAxis("Horizontal Movement");
        float z = UnityInput.Instance.GetAxis("Vertical Movement");

        MoveController.Move(x, z, Time.fixedDeltaTime);
    }
}
