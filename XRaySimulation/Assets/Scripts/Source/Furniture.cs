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

    [HideInInspector] public FurnitureController Controller;

    private SphereCollider trigger;

    private bool[] instructions;

    private void Awake()
    {
        trigger = this.gameObject.GetComponent<SphereCollider>();
        trigger.isTrigger = true;

        this.Controller = FurnitureControllerFactory.Create(Type, this.transform);
    }

    private void Update()
    {
        if (Controller.isTriggerd)
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

    // there should be a better way
    // is needed because GetKey will fire each frame
    // this leads to interaction with the furniture each frame
    // resulting in activating/deactivating it multiple times in a single press
    private bool GetInstructionBasedOnType(int index)
    {
        if (Type == FurnitureType.PatientTable)
            return UnityInput.Instance.GetKey(Info.KeyCodes[index]);
        else
            return UnityInput.Instance.GetKeyDown(Info.KeyCodes[index]);
    }

    private void OnTriggerEnter(Collider other)
    {
        Controller.Activate(Type, Info);
    }

    private void OnTriggerExit(Collider other)
    {
        Controller.Deactivate();
    }
}
