using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
  
    public int segments = 50;

    public float radius = 1.0f;

    LineRenderer lineRenderer;



    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = (segments + 1);
        lineRenderer.startWidth = 0.03f;
        lineRenderer.useWorldSpace = false;  //points will be relative to the revolving Orb's orbital center
        CreatePoints();
    }

    void CreatePoints()
    {
        float x;
        float y;
        //float z;

        float angle = 0f;

        for (int i = 0; i < (segments + 1); i++)
        { 
            //this draws the circle in CW order starting at (0,radius)
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }
}