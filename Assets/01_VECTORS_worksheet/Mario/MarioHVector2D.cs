using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Mathematics.math;

public class MarioHVector2D : MonoBehaviour
{
    public Transform planet;
    public float force = 5f;
    public float gravityStrength = 5f;

    private HVector2D gravityDir, gravityNorm;
    private HVector2D moveDir;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        gravityDir = new HVector2D(planet.position - transform.position);
        gravityNorm = new HVector2D(planet.position - transform.position);
        moveDir = new HVector2D(gravityDir.y, -gravityDir.x);

        //normalize & flip move vec -> clockwise
        moveDir.Normalize();
        moveDir = moveDir * -1f;

        //add force to move dir
        rb.AddForce(moveDir.ToUnityVector3() * force);

        //normalize gravity vec to get mag = 1 (can set gravity's mag w gravityStrength)
        gravityNorm.Normalize();
        //to get gravity
        rb.AddForce(gravityNorm.ToUnityVector3() * gravityStrength);

        //for astronaut sprite to rotate to look like it is standing on the surface of the moon.
        //new HVector2D(1, 0) = Vector3.right
        //moveDir is in HVector2D so don't need to change.

        float angle = new HVector2D(1, 0).FindAngle(moveDir);
        Debug.Log("gravityDir.x: " + gravityDir.x);
        Debug.Log("angle before adjustment: " + angle);
        
        //gravityDir.x = horizontal distance, which is the direction of the vector
        //checks when gravityDir.x is -ve = direction is facing towards the negative x-axis(left side of the astronaut)
        if (gravityDir.x < 0)
        {
            //flip the angle to -ve
            //note: this is to ensure that the astronaut rotates in the direction that aligns with the vector's direction
            //basically, angle and vector are in the same quadrant
            angle *= -1f;
        }

        Debug.Log("adjusted angle: " + angle);

        //radians to degrees
        angle *= Mathf.Rad2Deg;

        //how much to rotate around the z-axis
        rb.MoveRotation(Quaternion.Euler(0, 0, angle));

        //arrows for gravity vector
        //start position(astronaut), direction pointing in
        DebugExtension.DebugArrow(transform.position, gravityDir.ToUnityVector3(), Color.red);
        //arrows for move direction vector
        DebugExtension.DebugArrow(transform.position, moveDir.ToUnityVector3(), Color.blue);
    }
}
