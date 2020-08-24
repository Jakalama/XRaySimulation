using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

[RequireComponent(typeof(SphereCollider))]
public class Furniture : MonoBehaviour
{
    [SerializeField] public FurnitureType Type;
    [SerializeField] public FurnitureInfo Info;
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
        instructions = new bool[Info.KeyCodes.Length];

        for (int i = 0; i < Info.KeyCodes.Length; i++)
        {
            instructions[i] = GetInstructionBasedOnType(i);
        }
    }

    private bool GetInstructionBasedOnType(int index)
    {
        if (Type == FurnitureType.PatientTable)
            return Input.GetKey(Info.KeyCodes[index]);
        else
            return Input.GetKeyDown(Info.KeyCodes[index]);
    }

    private void OnTriggerEnter(Collider other)
    {
        Activate();
    }

    private void OnTriggerExit(Collider other)
    {
        Deactivate();
    }

    private void Activate()
    {
        if (FurnitureTriggerInfo.Type == FurnitureType.None)
        {
            isTriggerd = true;
            FurnitureTriggerInfo.SetActiveFurniture(Type, Info);
        }
    }

    private void Deactivate()
    {
        if (isTriggerd)
        {
            isTriggerd = false;
            FurnitureTriggerInfo.DeactivateFurniture();
        }
    }
}
