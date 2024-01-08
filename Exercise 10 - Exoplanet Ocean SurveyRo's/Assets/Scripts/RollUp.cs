using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RollUp : MonoBehaviour
{
    Vector3 pos;
    Vector3 vel;

    float speed;
    float x, y, z;



    Vector3 origin;

    Vector3 grad, normal;

    public Terrain surface;
    public Terrain floor;

    public bool onEdge;

    //Laser fields
    private int segments = 1;  //only drawing 1 line segment to go between the box and the ocean floor
    public float width;
    LineRenderer lineRenderer;



    // Start is called before the first frame update
    void Start()
    {
        x = transform.parent.position.x;
        z = transform.parent.position.z;
        y = floor.SampleHeight(new Vector3(x, 0f, z)) + floor.GetPosition().y;
        pos = new Vector3(x, y, z);

        vel = Vector3.zero;
        speed = 32f;

        onEdge = false;

        width = 1f;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = (segments + 1);
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.useWorldSpace = true;

        lineRenderer.SetPosition(0, transform.parent.position);
        lineRenderer.SetPosition(1, pos);
    }

    // Update is called once per frame
    void Update()
    {
        if (!onEdge)
        {
            normal = floor.GetComponent<TerrainCollider>().terrainData.GetInterpolatedNormal((x + 100f) / 200f, (z + 100f) / 200f);

            //grad = Vector3.ProjectOnPlane(normal, new Vector3(0f,1f,0f); //projection of the normal vector onto the x-z plane
            grad = new Vector3(normal.x, 0f, normal.z); //equivalent to the above, more efficient and precise

            //only one of the following four should be uncommented, based on the desired behavior (e.g. go higher, go lower, stay level to , go right & stay left)  

            //vel = speed * grad; //direction of steepest descent 
            vel = speed * (new Vector3(-grad.x, 0f, -grad.z));  //opposite to the vel in previous statement                                                                     
                                                                //vel = speed * (new Vector3(-grad.z, 0f, grad.x));  //perpendicular to both of the above
                                                                //vel = speed * (new Vector3(grad.z, 0f, -grad.x));  //opposite to the vel in previous statement

            x = x + Time.deltaTime * vel.x;
            z = z + Time.deltaTime * vel.z;

            y = floor.SampleHeight(new Vector3(x, 0f, z)) + floor.GetPosition().y;

            pos = new Vector3(x, y, z);

            lineRenderer.SetPosition(0, transform.parent.position);
            lineRenderer.SetPosition(1, pos);

            normal = floor.GetComponent<TerrainCollider>().terrainData.GetInterpolatedNormal((x + 100f) / 200f, (z + 100f) / 200f).normalized;

            //Stop searching after reaching the edge of the terrain
            if (x >= 99 || x <= -99 || z >= 99 || z <= -99)
            {
                onEdge = true;
                Debug.Log("Reached the edge");
            }
        }
        
        
        if (onEdge)
        {
            lineRenderer.SetPosition(0, transform.parent.position);
        }
    }



    //Deploy Method
    public void Deploy()
    {
        x = transform.parent.position.x;
        z = transform.parent.position.z;
        y = surface.SampleHeight(new Vector3(x, 0f, z)) + surface.GetPosition().y;
        pos = new Vector3(x, y, z);

        vel = Vector3.zero;
        speed = 32f;

        onEdge = false;

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = (segments + 1);
        lineRenderer.useWorldSpace = true;

        lineRenderer.SetPosition(0, transform.parent.position);
        lineRenderer.SetPosition(1, pos);
    }
}