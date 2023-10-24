using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    public Transform planet;
    public float force = 5f;
    public float gravityStrength = 5f;

    private Vector3 gravityDir, gravityNorm;
    private Vector3 moveDir;
    private Rigidbody2D rb;

    void Start()
    {
        //reference to gameobject's rigidbody2d component
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //find gravity vec
        gravityDir = planet.position - transform.position;
        //achieved by rotating the gravity vector
        moveDir = new Vector3(gravityDir.y, -gravityDir.x);
        //normalize & flip move vec -> clockwise
        moveDir = moveDir.normalized * -1f;

        //add force to move dir
        rb.AddForce(moveDir * force);

        //normalize gravity vec to get mag = 1 (can set gravity's mag w gravityStrength)
        gravityNorm = gravityDir.normalized;
        //to get gravity
        rb.AddForce(gravityNorm * gravityStrength);

        //for astronaut sprite to rotate to look like it is standing on the surface of the moon.
        //signed angle(where astronaut is looking, the vector to reach, axis to rotate around(z-axis))
        float angle = Vector3.SignedAngle(Vector3.right,moveDir,Vector3.forward);

        //how much to rotate around the z-axis
        rb.MoveRotation(Quaternion.Euler(0, 0, angle));

        //arrows for gravity vector
        //start position(astronaut), direction pointing in
        DebugExtension.DebugArrow(transform.position,gravityDir, Color.red);
        //arrows for move direction vector
        DebugExtension.DebugArrow(transform.position,moveDir,Color.blue);
    }
}


