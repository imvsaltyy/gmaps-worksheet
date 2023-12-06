using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMatrix : MonoBehaviour
{
    private HMatrix2D mat = new HMatrix2D();

    // Start is called before the first frame update
    void Start()
    {
        mat.SetIdentity();
        mat.Print();
        Question2();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    void Question2()
    {
        HMatrix2D mat1 = new HMatrix2D(1,2,1,0,1,0,2,3,4);
        HMatrix2D mat2 = new HMatrix2D(2,5,1,6,7,1,1,8,1);
        HMatrix2D resultMat = mat1 * mat2;
        resultMat.Print();
        //HVector2D vec1 = new HVector2D(2,5);
        //HMatrix2D resultMat1 = mat1 * mat2;
    }
}
