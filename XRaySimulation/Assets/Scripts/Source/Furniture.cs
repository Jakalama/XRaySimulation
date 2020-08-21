using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Furniture : MonoBehaviour
{
    public bool isTriggerd;
    protected SphereCollider trigger;

    private void Awake()
    {
        trigger = this.gameObject.GetComponent<SphereCollider>();
        trigger.isTrigger = true;
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        isTriggerd = true;
    }
}
