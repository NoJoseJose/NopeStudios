using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

public class ShadowMath : MonoBehaviour
{
    List<Vector3> vertices = new List<Vector3>();
    List<Vector3> normals = new List<Vector3>();
    //List<int> triangles = new List<int>();
    //hashset?
    GameObject[] tagged;
    //public float floatVal = 0;

    public GameObject secondary;

    void Start()
    {
        
        tagged = GameObject.FindGameObjectsWithTag("Wall");

        foreach (GameObject tag in tagged)
        {
            //tag.GetComponent<MeshFilter>().mesh.normals;
            Vector3[] vertii = tag.GetComponent<MeshFilter>().mesh.vertices;
        //    int[] triangles = tag.GetComponent<MeshFilter>().mesh.triangles;
            Vector3[] normii = tag.GetComponent<MeshFilter>().mesh.normals;


            if (vertii != null)
            {


                
                //single vertex
                for(int i = 0; i < vertii.Length; i++)
                {
                    //vertices.Add(tag.transform.TransformDirection(vertex));
                    vertices.Add(tag.transform.TransformPoint(vertii[i]));
                    normals.Add(tag.transform.TransformDirection(normii[i]));
                }
                
            }
        }
        
        
    }

    void Update()
    {
    
    }
    void OnDrawGizmos()
    {
        
        if (vertices != null)
        {
            //Tri-vertex style


            
            //single vertex style
            for (int i = 0; i < vertices.Count; i++ )
            {
                if(Vector3.Dot(normals[i], vertices[i] - transform.position) < 0.5f) //let's just deal with viewed faces
                {
                    //if we're here, there's at least one face, uh, facing the observer
                    //for something to be obscured, it's got to be on the 'other side' of a face AND reside in the 'shadow' cast by it
                    if (secondary != null)
                    {
                        if (Vector3.Dot(normals[i], vertices[i] - secondary.transform.position) > 0.5f)
                                
                          //      Quaternion.Angle(Quaternion.LookRotation(transform.position - vertices[i]), Quaternion.LookRotation(normals[i]))
                          //      - Quaternion.Angle(Quaternion.LookRotation(transform.position - secondary.transform.position), Quaternion.LookRotation(normals[i]))
                          //      > 0)
                                
                        {
                            Gizmos.color = Color.white;
                            Gizmos.DrawRay(vertices[i], ((vertices[i]) - this.transform.position));
                            Gizmos.color = Color.blue;
                            Gizmos.DrawRay(vertices[i], normals[i]);
                            
                            //Debug.Log("Vertex Dot: " + Vector3.Dot((transform.position - vertices[i]).normalized, normals[i]));
                            //Debug.Log("Second Dot: " + Vector3.Dot((vertices[i] - secondary.transform.position).normalized, normals[i]));
                            
                            //if (Vector3.Dot(vertices[i] - transform.position, transform.position - secondary.transform.position) < 0)
                            if (Vector3.Dot((transform.position - vertices[i]).normalized , normals[i]) < Vector3.Dot((vertices[i] - secondary.transform.position).normalized, normals[i]))
                            {
                                
                                Gizmos.color = Color.green;
                                Gizmos.DrawSphere(secondary.transform.position, 0.2f);
                            }
                            Gizmos.DrawLine(transform.position, secondary.transform.position);
                            Gizmos.DrawLine(vertices[i], transform.position);
                        }
                    }
                }
                
            }
            

        }
    }
}
