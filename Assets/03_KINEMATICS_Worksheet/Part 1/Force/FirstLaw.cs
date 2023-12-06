using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLaw : MonoBehaviour
{
    public Vector3 force;
    Rigidbody rb;

    void Start()
    {
        //reference to the rigidbody component
        rb = GetComponent<Rigidbody>();
        //add impulse force to the rb
        //forcemode.impulse = instantaneous change
        rb.AddForce(force,ForceMode.Impulse);
     }

    void FixedUpdate()
    {
        //debug the current position of the rb
        Debug.Log(transform.position);
    }
}

