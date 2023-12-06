using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PoolCue : MonoBehaviour
{
    public LineFactory lineFactory;
    public GameObject ballObject;

    private Line drawnLine;
    private Ball2D ball;
    

    private void Start()
    {
        //reference to the ball2D component
        ball = ballObject.GetComponent<Ball2D>();
    }

    void Update()
    {
        //checking the click of the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            //the mouse position is transform to world space
            var startLinePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Start line drawing
            //checking there is a ball and the ball is colliding with the mouse position
            if (ball != null && ball.IsCollidingWith(startLinePos.x,startLinePos.y))
            {
                //creates a line from the mouse position to the ball position
                drawnLine = lineFactory.GetLine(startLinePos, new Vector2(ball.Position.x, ball.Position.y), 1f, Color.black);
                //to draw the line from the above code
                drawnLine.EnableDrawing(true);
            }
        }
        //checking the release of left mouse button and there is a line drawn
        else if (Input.GetMouseButtonUp(0) && drawnLine != null)
        {
            //stops drawing
            drawnLine.EnableDrawing(false);

            //update the velocity of the ball with a vector from the mouse up postion to the ball position
            //note: magnitude of the vector = velocity
            HVector2D v = new HVector2D(ball.Position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x, ball.Position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            ball.Velocity = v;

            drawnLine = null; // End line drawing            
        }

        //line is drawn
        if (drawnLine != null)
        {
            //the line is started at the mouse position
            drawnLine.start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //the line is ended at the ball position
            drawnLine.end = new Vector2 (ball.Position.x,ball.Position.y); // Update line end
        }
    }

    /// <summary>
    /// Get a list of active lines and deactivates them.
    /// </summary>
    public void Clear()
    {
        var activeLines = lineFactory.GetActive();

        foreach (var line in activeLines)
        {
            line.gameObject.SetActive(false);
        }
    }
}
