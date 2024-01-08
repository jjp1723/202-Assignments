using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Helix : MonoBehaviour
{
  
    public int segments = 24;

    public float radius = 1.0f; //this can be changed by GameManager

    public float width = .01f;  //this can be changed by GameManager

    public Color color;  //this can be changed by GameManager

    public int numWinds; //this can be changed by GameManager

    public float height; //this can be changed by GameManager


    LineRenderer lineRenderer;  //this is a required Component of the GameObject to which this script is attached

    public void GenHelix()
    { 
        lineRenderer = gameObject.GetComponent<LineRenderer>(); 
        lineRenderer.positionCount = (segments + 1);
        lineRenderer.startWidth = width;
        lineRenderer.startColor = color;
        lineRenderer.endColor = lineRenderer.startColor;
        lineRenderer.useWorldSpace = false;
        CreateSpiralPoints();
    }

    void CreateSpiralPoints()
    {
        float x;
        float y;
        float z;

        float angle = 0f;
        float spiralSlope = height / segments;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            y = spiralSlope * i;

            lineRenderer.SetPosition(i, new Vector3(x, y, z));

            angle += numWinds*(360f / segments);
        }
    }
}
