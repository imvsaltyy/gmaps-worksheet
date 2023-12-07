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

    //constructor 1 -> identity matrix 
    public HMatrix2D()
    {
        //initialise the identity matrix (set the values of the entries array)
        SetIdentity();
    }

    //constructor 2 -> uses a provided multiarray
    public HMatrix2D(float[,] multiArray)
    {
        //for loops for each rows and columns
        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
            {
                //copy each entries into multiarray
                Entries[y, x] = multiArray[y, x];
            }         
    }

    //constructor -> for individual elements in the 3x3 matrix
    public HMatrix2D(float m00, float m01, float m02,
                    float m10, float m11, float m12,
                    float m20, float m21, float m22)
    {
        /*
            00 01 02
            10 11 12
            20 21 22
        */
        //setting each variable to the corresponding element in the matrix
        Entries[0, 0] = m00;
        Entries[0, 1] = m01;
        Entries[0, 2] = m02;

        Entries[1, 0] = m10;
        Entries[1, 1] = m11;
        Entries[1, 2] = m12;

        Entries[2, 0] = m20;
        Entries[2, 1] = m21;
        Entries[2, 2] = m22;

        //shld be 9 entries for a 3x3 matrix
    }

    //addition for matrix, parameters are the left matrix and right matrix
    public static HMatrix2D operator +(HMatrix2D left, HMatrix2D right)
    {
        //new instance to store results of addition
        HMatrix2D result = new HMatrix2D();

        //for loops for each rows and columns
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                //each result element is the addition of left and right element
                result.Entries[y, x] = left.Entries[y, x] + right.Entries[y, x];
            }
        }

        return result;
    }

    //subtraction for matrix
    public static HMatrix2D operator -(HMatrix2D left, HMatrix2D right)
    {
        //new instance to store results of substraction
        HMatrix2D result = new HMatrix2D();

        //for loops for each rows and columns
        for (int y = 0; y< 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                //each result element is the substraction of left and right element
                result.Entries[y, x] = left.Entries[y, x] - right.Entries[y, x];
            }
        }

        return result;
    }

    //multiplication with a scalar value (eg. 2)
    public static HMatrix2D operator *(HMatrix2D left, float scalar)
    {
        //new instance to store results of scalar multiplication
        HMatrix2D result = new HMatrix2D();

        //for loops for each rows and columns
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                //each left element is multiplied by the scalar for each result element
                result.Entries[y, x] = left.Entries[y, x] * scalar;
            }
        }

        return result;
    }

    //Note that the second argument is a HVector2D object
    //multiplication with a vector (x,y)
    public static HVector2D operator *(HMatrix2D left, HVector2D right)
    {

        return new HVector2D
        (
            /* 
               00 01 02    x 
               10 11 12    y 
                           
               */
            //since it's rows x columns, 00 will multiply with x, 01 will multiply with y, 02 will multiply with 1
            //multiplied by 1 => homogenous coordinate to ensure matrix multiplication
            left.Entries[0, 0] * right.x + left.Entries[0, 1] * right.y + left.Entries[0, 2],
            left.Entries[1, 0] * right.x + left.Entries[1, 1] * right.y + left.Entries[1, 2]
        );
    }

    // Note that the second argument is a HMatrix2D object
    //multiplication for matrices
    public static HMatrix2D operator *(HMatrix2D left, HMatrix2D right)
    {
        return new HMatrix2D
        (
            /* 
                00 01 02    00 xx xx
                xx xx xx    10 xx xx
                xx xx xx    20 xx xx
                */
            //since it's rows x columns, the 1st row of the left entries will multiply with the 1st column of the right entries,
            //where entries are multiplied from left to right, up to down
            left.Entries[0, 0] * right.Entries[0, 0] + left.Entries[0, 1] * right.Entries[1, 0] + left.Entries[0, 2] * right.Entries[2, 0],

            /* 
                00 01 02    xx 01 xx
                xx xx xx    xx 11 xx
                xx xx xx    xx 21 xx
                */
            //1st row is then multiplied with the 2nd column
            left.Entries[0, 0] * right.Entries[0, 1] + left.Entries[0, 1] * right.Entries[1, 1] + left.Entries[0, 2] * right.Entries[2, 1],
            /* 
                00 01 02    xx xx 02
                xx xx xx    xx xx 12
                xx xx xx    xx xx 22
                */
            //1st row is then multiplied with the 3rd column
            left.Entries[0, 0] * right.Entries[0, 2] + left.Entries[0, 1] * right.Entries[1, 2] + left.Entries[0, 2] * right.Entries[2, 2],
             /* 
                 xx xx xx    00 xx xx
                 10 11 12    10 xx xx
                 xx xx xx    20 xx xx
                 */
             //2nd row is then multiplied with the 1st column
             left.Entries[1, 0] * right.Entries[0, 0] + left.Entries[1, 1] * right.Entries[1, 0] + left.Entries[1, 2] * right.Entries[2, 0],
             /* 
                 xx xx xx    xx 01 xx
                 10 11 12    xx 11 xx
                 xx xx xx    xx 21 xx
                 */
             //2nd row is then multiplied with the 2nd column
             left.Entries[1, 0] * right.Entries[0, 1] + left.Entries[1, 1] * right.Entries[1, 1] + left.Entries[1, 2] * right.Entries[2, 1],
             /* 
                 xx xx xx    xx xx 02
                 10 11 12    xx xx 12
                 xx xx xx    xx xx 22
                 */
             //2nd row is then multiplied with the 3rd column
             left.Entries[1, 0] * right.Entries[0, 2] + left.Entries[1, 1] * right.Entries[1, 2] + left.Entries[1, 2] * right.Entries[2, 2],
             /* 
                 xx xx xx    00 xx xx
                 xx xx xx    10 xx xx
                 20 21 22    20 xx xx
                 */
             //3rd row is then multiplied with the 1st column
             left.Entries[2, 0] * right.Entries[0, 0] + left.Entries[2, 1] * right.Entries[1, 0] + left.Entries[2, 2] * right.Entries[2, 0],
             /* 
                 xx xx xx    xx 01 xx
                 xx xx xx    xx 11 xx
                 20 21 22    xx 21 xx
                 */
             //3rd row is then multiplied with the 2nd column
             left.Entries[2, 0] * right.Entries[0, 1] + left.Entries[2, 1] * right.Entries[1, 1] + left.Entries[2, 2] * right.Entries[2, 1],
             /* 
                 xx xx xx    xx xx 02
                 xx xx xx    xx xx 12
                 20 21 22    xx xx 22
                 */
             //3rd row is then multiplied with the 3rd column
             left.Entries[2, 0] * right.Entries[0, 2] + left.Entries[2, 1] * right.Entries[1, 2] + left.Entries[2, 2] * right.Entries[2, 2]
        
    );
    }

    //checking for equality
    public static bool operator ==(HMatrix2D left, HMatrix2D right)
    {
        //for loops for each rows and columns
        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
                //checking: left entries element not equal to the right entries element
                if (left.Entries[y, x] != right.Entries[y, x])
                    return false;
        return true;
    }

    //checking for inequality
    public static bool operator !=(HMatrix2D left, HMatrix2D right)
    {
        //for loops for each rows and columns
        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
                //checking: left entries element equal to the right entries element
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

    //translation
    public void setTranslationMat(float transX, float transY)
    {
        //to reset the matrix to identity
        SetIdentity();
        /* 
          1 0 x
          0 1 y
          0 0 1
          */
        //2D translate transformation
        Entries[0, 2] = transX;
        Entries[1, 2] = transY;
    }

    //rotation
    public void setRotationMat(float rotDeg)
    {
        //to reset the matrix to identity
        SetIdentity();
        //change degrees to radians
        //Unity expects the input for Mathf to be in radians
        float rad = rotDeg * Mathf.Deg2Rad;
        /* 
          00 01
          10 11
          */
        //2D rotation transformation
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

