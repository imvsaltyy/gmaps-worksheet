using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransformMesh : MonoBehaviour
{
    [HideInInspector]
    public Vector3[] vertices { get; private set; }

    private HMatrix2D transformMatrix = new HMatrix2D();
    HMatrix2D toOriginMatrix = new HMatrix2D();
    HMatrix2D fromOriginMatrix = new HMatrix2D();
    HMatrix2D rotateMatrix = new HMatrix2D();

    private MeshManager meshManager;
    HVector2D pos = new HVector2D();

    void Start()
    {
        meshManager = GetComponent<MeshManager>();
        //current position of sprite (sonic)
        pos = new HVector2D(gameObject.transform.position.x, gameObject.transform.position.y);
        //translate by 1
        Translate(1,1);
        //rotate by -45
        Rotate(-45);

    }

    void Translate(float x, float y)
    {
        //reset the transformation of the matrix
        transformMatrix.SetIdentity();
        //translate the matrix by the specified x,y coords
        transformMatrix.setTranslationMat(x, y);
        
        Transform();

        //update the position of the sprite 
        pos = transformMatrix * pos;
        
    }

    void Rotate(float angle)
    {
        //negative coords values -> bring back to origin
        toOriginMatrix.setTranslationMat(-pos.x,-pos.y);
        //translate back to original position (1f,1f)
        fromOriginMatrix.setTranslationMat(pos.x, pos.y);
        //to rotate the matrix
        rotateMatrix.setRotationMat(angle);
        //reset the transformation
        transformMatrix.SetIdentity();
        //right to left: move to origin, then rotate, then move to original position
        transformMatrix = fromOriginMatrix * rotateMatrix * toOriginMatrix;
        
        Transform();
    }

    private void Transform()
    {
        //get the verticles
        vertices = meshManager.clonedMesh.vertices;

        //loop through each vertices (the sonic sprite has 4)
        for (int i = 0; i < vertices.Length; i++)
        {
            //declare a vector for the vert
            HVector2D vert = new HVector2D(vertices[i].x, vertices[i].y);
            //transform each vert
            vert = transformMatrix * vert;
            //update the vert coords with the transformed verticles
            vertices[i].x = vert.x;
            vertices[i].y = vert.y;
        }
        //update the transformed verticles
        meshManager.clonedMesh.vertices = vertices;
    }
}
