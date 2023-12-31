using JetBrains.Annotations;
using UnityEngine;

public class VectorExercises : MonoBehaviour
{
    [SerializeField] LineFactory lineFactory;
    [SerializeField] bool Q2a, Q2b, Q2d, Q2e;
    [SerializeField] bool Q3a, Q3b, Q3c, projection;

    private Line drawnLine;

    private Vector2 startPt;
    private Vector2 endPt;

    public float GameWidth, GameHeight;
    private float minX, minY, maxX, maxY;
    
    private void Start()
    {
        if (Q2a)
            Question2a();
        if (Q2b)
            CalculateGameDimensions();
            Question2b(20);
        if (Q2d)
            CalculateGameDimensions();
            Question2d();
        if (Q2e)
            CalculateGameDimensions();
            Question2e(20);
        if (Q3a)
            Question3a();
        if (Q3b)
            Question3b();
        if (Q3c)
            Question3c();
        if (projection)
            Projection();
    }

    public void CalculateGameDimensions()
    {
        GameHeight = Camera.main.orthographicSize * 2f;
        GameWidth = Camera.main.aspect * GameHeight;

        maxX = GameWidth / 2;
        maxY = GameHeight / 2;
        minX = -maxX;
        minY = -maxY;
    }

    void Question2a()
    {
        
        startPt = new Vector2(0, 0);
        endPt = new Vector2(2, 3);

        drawnLine = lineFactory.GetLine(startPt, endPt, 0.02f, Color.black);

        drawnLine.EnableDrawing(true);

        Vector2 vec2 = endPt - startPt;
        Debug.Log("Magnitude = " + vec2.magnitude);
    }

    void Question2b(int n)
    {
        for (int i = 0; i < n; i++)
        {
            startPt = new Vector2(
                Random.Range(-5, 5),
                Random.Range(-5, 5));

            endPt = new Vector2(
                Random.Range(-5, 5),
                Random.Range(-5, 5));

            drawnLine = lineFactory.GetLine(startPt, endPt, 0.02f, Color.black);

            drawnLine.EnableDrawing(true);

        }
    }

    void Question2d()
    {
        DebugExtension.DebugArrow(
            new Vector3(0, 0, 0),
            new Vector3(5, 5, 0),
            Color.red,
            60f);
    }

    void Question2e(int n)
    {
        for (int i = 0; i < n; i++)
        {
            DebugExtension.DebugArrow(
                new Vector3(0, 0, 0),
                new Vector3(
                Random.Range(-maxX, maxX),
                Random.Range(-maxY, maxY),
                Random.Range(-minY, minY)),
                Color.white,
                60f);

        }

    }


    public void Question3a()
    {
        HVector2D a = new HVector2D(3, 5);
        HVector2D b = new HVector2D(-4, 2);
        HVector2D c = a + b;
 
        //first code
        //DebugExtension.DebugArrow(Vector3.zero, a.ToUnityVector3(), Color.red, 60f);
        //DebugExtension.DebugArrow(Vector3.zero, b.ToUnityVector3(), Color.green, 60f);
        //DebugExtension.DebugArrow(Vector3.zero, c.ToUnityVector3(), Color.white, 60f);
        //DebugExtension.DebugArrow(a.ToUnityVector3(),b.ToUnityVector3(), Color.green, 60f);

        float magA = a.Magnitude();
        Debug.Log("Magnitude of a = " + magA.ToString("F2"));
        float magB = b.Magnitude();
        Debug.Log("Magnitude of b = " + magB.ToString("F2"));
        float magC = c.Magnitude();
        Debug.Log("Magnitude of c = " + magC.ToString("F2"));

        //modified code
        DebugExtension.DebugArrow(Vector3.zero, a.ToUnityVector3(), Color.red, 60f);
        DebugExtension.DebugArrow(Vector3.zero, b.ToUnityVector3(), Color.green, 60f);
        DebugExtension.DebugArrow(Vector3.zero, (a-b).ToUnityVector3(), Color.white, 60f);
        DebugExtension.DebugArrow(a.ToUnityVector3(), -b.ToUnityVector3(), Color.green, 60f);
    }


    public void Question3b()
    {
        HVector2D a = new HVector2D(3, 5);
        HVector2D b = a * 2;
        Vector3 offset = new Vector3(1, 0, 0);

        //first code
        //DebugExtension.DebugArrow(Vector3.zero, a.ToUnityVector3(), Color.red, 60f);
        //DebugExtension.DebugArrow(offset, b.ToUnityVector3(), Color.green, 60f);

        //modified code
        DebugExtension.DebugArrow(Vector3.zero, a.ToUnityVector3(), Color.red, 60f);
        DebugExtension.DebugArrow(offset, (a/2).ToUnityVector3(), Color.green, 60f);
    }

    public void Question3c()
    {
        HVector2D a = new HVector2D(3, 5);
        DebugExtension.DebugArrow(Vector3.zero, a.ToUnityVector3(), Color.red, 60f);

        Vector3 offset = new Vector3(1, 0, 0);
        a.Normalize();
        Debug.Log("Magnitude of a = " + a.Magnitude().ToString("F2"));
        DebugExtension.DebugArrow(offset, a.ToUnityVector3(), Color.green, 60f);

    }

    public void Projection()
    {
        HVector2D a = new HVector2D(0, 0);
        HVector2D b = new HVector2D(6, 0);
        HVector2D c = new HVector2D(2, 2);

        HVector2D v1 = b - a;
        HVector2D v2 = c - a;

        v1.DotProduct(v2);

        HVector2D proj = v2.Projection(v1);

        DebugExtension.DebugArrow(a.ToUnityVector3(), b.ToUnityVector3(), Color.red, 60f);
        DebugExtension.DebugArrow(a.ToUnityVector3(), c.ToUnityVector3(), Color.yellow, 60f);
        DebugExtension.DebugArrow(a.ToUnityVector3(), proj.ToUnityVector3(), Color.white, 60f);
    }
}
