using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball2D : MonoBehaviour
{
    //vectors to use for the position and velocity of the ball
    public HVector2D Position = new HVector2D(0, 0);
    public HVector2D Velocity = new HVector2D(0, 0);

    [HideInInspector]
    public float Radius;

    private void Start()
    {
        //set the current position of the ball
        Position.x = transform.position.x;
        Position.y = transform.position.y;

        //get the radius of the ball using the sprite
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Vector2 sprite_size = sprite.rect.size;
        //change the size to fix the local space
        Vector2 local_sprite_size = sprite_size / sprite.pixelsPerUnit;
        Radius = local_sprite_size.x / 2f;
    }

    public bool IsCollidingWith(float x, float y)
    {
        //the distance of the ball to the other position(mouse)
        float distance = Util.FindDistance(Position, new HVector2D(x,y));
        //true if the mouse distance is smaller or equal to the radius of the ball (the mouse is colliding with the ball)
        return distance <= Radius;
    }

    public bool IsCollidingWith(Ball2D other)
    {
        float distance = Util.FindDistance(Position, other.Position);
        return distance <= Radius + other.Radius;
    }

    public void FixedUpdate()
    {
        //update the position of the ball with the changes in velocity and time
        UpdateBall2DPhysics(Time.deltaTime);
    }

    private void UpdateBall2DPhysics(float deltaTime)
    {
        //displacement of vector in each axis using the current velocity and time
        float displacementX = Velocity.x * deltaTime;
        float displacementY = Velocity.y * deltaTime;

        //add the displacement to the current position in each axis
        Position.x += displacementX;
        Position.y += displacementY;

        //change the position of the ball
        transform.position = new Vector2(Position.x, Position.y);
    }
}

