using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AWWW YISSSSS
using UnityEngine.AI;

public class Influence : MonoBehaviour
{
    //    NavMeshSurface navMeshSurface;

    //public GridBall gridobject;

    GameObject[,] gridpoints;

    // Start is called before the first frame update
    void Start()
    {

        gridpoints = new GameObject[50,50];
        for(int i = 0; i < 50; i++)
        {
            for(int j = 0; j < 50; j++)
            {
                //gridpoints[i,j] = new GameObject(); 
                gridpoints[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                gridpoints[i,j].transform.position = this.transform.position + (i * Vector3.right) + (j * Vector3.forward);
                gridpoints[i,j].transform.parent = this.transform;
                gridpoints[i, j].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                
            }
            
        }
        
        /*
        Vector3 verts;
        int ids;
        NavMeshTriangulation navTri = NavMesh.CalculateTriangulation();
        //foreach (int i in navTri.areas)
        foreach (int i in navTri.indices)
        {
            Debug.Log(navTri.vertices[navTri.indices[i]]);
            
        }
        */

    }

    // Update is called once per frame
    void Update()
    {

    }
}
