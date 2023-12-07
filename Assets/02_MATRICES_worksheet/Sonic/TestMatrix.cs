using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMatrix : MonoBehaviour
{
    private HMatrix2D mat = new HMatrix2D();

    // Start is called before the first frame update
    void Start()
    {
        //set the mat variable to the identity matrix
        mat.SetIdentity();
        //print out the matrix (should look like this:)
        /* 
          1 0 0
          0 1 0
          0 0 1
        */
        mat.Print();

        Question2();
    }

    void Question2()
    {
        HMatrix2D mat1 = new HMatrix2D(1,2,1,0,1,0,2,3,4);
        HMatrix2D mat2 = new HMatrix2D(2,5,1,6,7,1,1,8,1);
        HMatrix2D resultMat1 = mat1 * mat2;
        resultMat1.Print();

        HMatrix2D mat3 = new HMatrix2D
            (2,0,0,
            6,0,0,
            1,0,0);
        HMatrix2D resultMat2 = mat1 * mat3;
        resultMat2.Print();

        HVector2D vec1 = new HVector2D(2,5);
        HVector2D resultVec = mat1 * vec1;
        resultVec.Print();
    }
}
