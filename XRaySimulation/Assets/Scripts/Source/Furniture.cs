using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

[RequireComponent(typeof(SphereCollider))]
public class Furniture : MonoBehaviour
{
    [SerializeField] public FurnitureType Type;
    [SerializeField] public KeyCode[] KeyCodes;
    [HideInInspector] public bool isTriggerd;

    private SphereCollider trigger;
    private FurnitureController Controller;

    private bool[] instructions;

    private void Awake()
    {
        trigger = this.gameObject.GetComponent<SphereCollider>();
        trigger.isTrigger = true;

        this.Controller = FurnitureControllerFactory.Create(Type);
        this.Controller.SetTransform(this.transform);
    }

    private void Update()
    {
        if (isTriggerd)
        {
            GetInstructions();
            Controller.Interact(instructions, Time.deltaTime);
        }
    }

    private void GetInstructions()
    {
        instructions = new bool[KeyCodes.Length];

        for (int i = 0; i < KeyCodes.Length; i++)
        {
            instructions[i] = GetInstructionBasedOnType(i);
        }
    }

    private bool GetInstructionBasedOnType(int index)
    {
        if (Type == FurnitureType.PatientTable)
            return Input.GetKey(KeyCodes[index]);
        else
            return Input.GetKeyDown(KeyCodes[index]);
    }

    private void OnTriggerEnter(Collider other)
    {
        isTriggerd = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggerd = false;
    }
}
