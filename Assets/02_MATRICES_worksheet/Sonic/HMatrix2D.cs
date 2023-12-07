using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HMatrix2D : MonoBehaviour
{
    //to encapsulate the matrix entries
    //entries is a propetry, get = returns the value, set = gives a value to entries
    //initialise a 3x3 matrix
    public float[,] Entries { get; set; } = new float[3, 3];

    public HMatrix2D()
    {
        //initialise the identity matrix (set the values of the entries array)
        SetIdentity();
    }

    //
    public HMatrix2D(float[,] multiArray)
    {
        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
            {
                //copy each entries into multiarray
                Entries[y, x] = multiArray[y, x];
            }         
    }

    public HMatrix2D(float m00, float m01, float m02,
             float m10, float m11, float m12,
             float m20, float m21, float m22)
    {
        // First row
        Entries[0, 0] = m00;

        // Second row
        Entries[0, 1] = m01;

        // Third row
        Entries[0, 2] = m02;

        Entries[1, 0] = m10;
        Entries[1, 1] = m11;
        Entries[1, 2] = m12;

        Entries[2, 0] = m20;
        Entries[2, 1] = m21;
        Entries[2, 2] = m22;

        //shld be 9 rows
    }

    public static HMatrix2D operator +(HMatrix2D left, HMatrix2D right)
    {
        HMatrix2D result = new HMatrix2D();

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                result.Entries[y, x] = left.Entries[y, x] + right.Entries[y, x];
            }
        }

        return result;
    }

    public static HMatrix2D operator -(HMatrix2D left, HMatrix2D right)
    {
        HMatrix2D result = new HMatrix2D();

        for (int y = 0; y< 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                result.Entries[y, x] = left.Entries[y, x] - right.Entries[y, x];
            }
        }

        return result;
    }

    public static HMatrix2D operator *(HMatrix2D left, float scalar)
    {
        HMatrix2D result = new HMatrix2D();

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                result.Entries[y, x] = left.Entries[y, x] * scalar;
            }
        }

        return result;
    }

    //Note that the second argument is a HVector2D object

    public static HVector2D operator *(HMatrix2D left, HVector2D right)
    {
        return new HVector2D
        (
            /* 
               00 01 02    x 
               10 11 12    y 
                           
               */
            left.Entries[0, 0] * right.x + left.Entries[0, 1] * right.y + left.Entries[0, 2],
            left.Entries[1, 0] * right.x + left.Entries[1, 1] * right.y + left.Entries[1, 2]
        );
    }

    // Note that the second argument is a HMatrix2D object

    public static HMatrix2D operator *(HMatrix2D left, HMatrix2D right)
    {
        return new HMatrix2D
        (
            /* 
                00 01 02    00 xx xx
                xx xx xx    10 xx xx
                xx xx xx    20 xx xx
                */
            left.Entries[0, 0] * right.Entries[0, 0] + left.Entries[0, 1] * right.Entries[1, 0] + left.Entries[0, 2] * right.Entries[2, 0],

            /* 
                00 01 02    xx 01 xx
                xx xx xx    xx 11 xx
                xx xx xx    xx 21 xx
                */
            left.Entries[0, 0] * right.Entries[0, 1] + left.Entries[0, 1] * right.Entries[1, 1] + left.Entries[0, 2] * right.Entries[2, 1],
            /* 
                00 01 02    xx xx 02
                xx xx xx    xx xx 12
                xx xx xx    xx xx 22
                */
            left.Entries[0, 0] * right.Entries[0, 2] + left.Entries[0, 1] * right.Entries[1, 2] + left.Entries[0, 2] * right.Entries[2, 2],
             /* 
                 xx xx xx    00 xx xx
                 10 11 12    10 xx xx
                 xx xx xx    20 xx xx
                 */
             left.Entries[1, 0] * right.Entries[0, 0] + left.Entries[1, 1] * right.Entries[1, 0] + left.Entries[1, 2] * right.Entries[2, 0],
             /* 
                 xx xx xx    xx 01 xx
                 10 11 12    xx 11 xx
                 xx xx xx    xx 21 xx
                 */
             left.Entries[1, 0] * right.Entries[0, 1] + left.Entries[1, 1] * right.Entries[1, 1] + left.Entries[1, 2] * right.Entries[2, 1],
             /* 
                 xx xx xx    xx xx 02
                 10 11 12    xx xx 12
                 xx xx xx    xx xx 22
                 */
             left.Entries[1, 0] * right.Entries[0, 2] + left.Entries[1, 1] * right.Entries[1, 2] + left.Entries[1, 2] * right.Entries[2, 2],
             /* 
                 xx xx xx    00 xx xx
                 xx xx xx    10 xx xx
                 20 21 22    20 xx xx
                 */
             left.Entries[2, 0] * right.Entries[0, 0] + left.Entries[2, 1] * right.Entries[1, 0] + left.Entries[2, 2] * right.Entries[2, 0],
             /* 
                 xx xx xx    xx 01 xx
                 xx xx xx    xx 11 xx
                 20 21 22    xx 21 xx
                 */
             left.Entries[2, 0] * right.Entries[0, 1] + left.Entries[2, 1] * right.Entries[1, 1] + left.Entries[2, 2] * right.Entries[2, 1],
             /* 
                 xx xx xx    xx xx 02
                 xx xx xx    xx xx 12
                 20 21 22    xx xx 22
                 */
             left.Entries[2, 0] * right.Entries[0, 2] + left.Entries[2, 1] * right.Entries[1, 2] + left.Entries[2, 2] * right.Entries[2, 2]
        // and so on for another 7 entries
    );
    }

    public static bool operator ==(HMatrix2D left, HMatrix2D right)
    {
        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
                if (left.Entries[y, x] != right.Entries[y, x])
                    return false;
        return true;
    }

    public static bool operator !=(HMatrix2D left, HMatrix2D right)
    {
        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
                if (left.Entries[y, x] == right.Entries[y, x])
                    return false;
        return true;
    }

    //public override bool Equals(object obj)
    //{
    //    // your code here
    //}

    //public override int GetHashCode()
    //{
    //    // your code here
    //}

    //public HMatrix2D transpose()
    //{
    //    return // your code here
    //}

    //public float getDeterminant()
    //{
    //    return // your code here
    //}

    //for resetting the matrix
    public void SetIdentity()
    {
        //COMMENTED OUT
        //matrix order = rows x columns
        //different rows are arranged vertically in a matrix, y to represent
        //loop 3 times => 3x3 matrix (no. of rows in the entries array is 3)
        //for(int y = 0; y < 3; y++)
        //{
              //different columns are arranged horizontally in a matrix, x to represent
              //no. of columns in the entries array is 3
        //    for(int x = 0; x < 3; x++)
        //    {
                  //checking whether the current x value is the same as the current y value
        //        if (x == y)
        //        {
                      //true => sets the value at that position (eg. [0,0]) to 1
                      //the main diagonal elements in a identity matrix is 1
        //            Entries[y,x]= 1;
        //        }
                  //not the same
        //        else
        //        {
                      //not needed since elements are initialised to 0 when declaring an array
                      //sets the value at that position to 0
        //            Entries[y,x] = 0;
        //        }
        //    }
        //}

        //ternary operator
        //for loops for both rows and columns
        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
                //TAKEN FROM TUTORIAL: [condition] ? [code to be run if condition is true] : [code to be run if condition is false];
                //if the value of x and y is same in the entries matrix, the entries matrix element = 1, if not would be 0
                Entries[y, x] = x == y ? 1 : 0;

    }

    public void setTranslationMat(float transX, float transY)
    {
        SetIdentity();
        Entries[0, 2] = transX;
        Entries[1, 2] = transY;
    }

    //rotation
    public void setRotationMat(float rotDeg)
    {
        //
        SetIdentity();
        //
        float rad = rotDeg * Mathf.Deg2Rad;
        /* 
          00 01
          10 11
          */
        Entries[0, 0] = Mathf.Cos(rad);
        Entries[0, 1] = -Mathf.Sin(rad);
        Entries[1, 0] = Mathf.Sin(rad);
        Entries[1, 1] = Mathf.Cos(rad);
    }

    public void setScalingMat(float scaleX, float scaleY)
    {
        // your code here
    }

    public void Print()
    {
        string result = "";
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                result += Entries[r, c] + "  ";
            }
            result += "\n";
        }
        Debug.Log(result);
    }
}

