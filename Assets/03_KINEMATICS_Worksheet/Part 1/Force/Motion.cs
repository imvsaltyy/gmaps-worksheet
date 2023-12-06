using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    public Vector3 Velocity;

    //note: fixedupdate = called every fixed frame-rate frame (for physics update)
    void FixedUpdate()
    {
        //get the time of the last frame to current frame
        float dt = Time.deltaTime;

        //displacement of vector in each axis using the velocity and change in time
        float dx = Velocity.x * dt;
        float dy = Velocity.y * dt;
        float dz = Velocity.z * dt;

        //moves the position of gameobj to the new displaced position
        transform.Translate(new Vector3(dx, dy, dz));
    }
}
