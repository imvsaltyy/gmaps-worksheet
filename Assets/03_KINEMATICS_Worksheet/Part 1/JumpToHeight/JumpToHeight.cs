using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpToHeight : MonoBehaviour
{
    public float Height = 1f;
    Rigidbody rb;

    private void Start()
    {
        //reference to the rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Jump()
    {
        //SUVAT kinematic equation: v = final velocity, u = initial velocity, a = acceleration, s = displacement
        // v*v = u*u + 2as
        // u*u = v*v - 2as
        // u = sqrt(v*v - 2as) -> finding initial velocity
        // v = 0, u = ?, a = Physics.gravity, s = Height

        // -2 -> sqrt(0 - 2as)
        float u = Mathf.Sqrt(-2 * Physics2D.gravity.y * Height);
        //The velocity when jumping up (y-axis)
        rb.velocity = new Vector3(0,u,0);

        //float jumpForce = Mathf.Sqrt(-2 * Physics2D.gravity.y * Height);
        //rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }

    private void Update()
    {
        //when spacebar is pressed, rb will jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
}

