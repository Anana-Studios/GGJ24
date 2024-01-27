using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObj : MonoBehaviour
{
    public bool isGrabbed = false;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrabZone"))
        {
            other.GetComponentInParent<PlayerController>()._objInReach = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GrabZone"))
        {
            other.GetComponentInParent<PlayerController>()._objInReach = null;
        }
    }
    public void ResetObj()
    {
        isGrabbed = false;
        rb.useGravity = true;   
        transform.parent = null;
        rb.constraints = RigidbodyConstraints.None;
    }
}
