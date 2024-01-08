using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plotter : MonoBehaviour
{
    public int segments; 

    public float width;

    LineRenderer lineRenderer;

    void Start()
    {  
        width = 1f;

        segments = 150;  //just found by trial and arrow/error, since we don't know analytically when it will hit the ground (unless the ground happens to be flat) 

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = (segments + 1);
        lineRenderer.startWidth = width;
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = lineRenderer.startColor;
        lineRenderer.useWorldSpace = true;
    }

    public void PlotTrajectory(Vector3 initPos, Vector3 initVel)
    {
        const float g = 9.8f;
        float timeStep = .2f;
        float tT = 0f;  //elaplse time of the virtual flight
        float x;
        float y;
        float z = 0f;  //since we're in 2D

        lineRenderer.SetPosition(0, initPos);

        for (int i = 1; i < (segments + 1); i++)
        {
            tT += timeStep;  //total elapsed time
            x = initPos.x + initVel.x * (tT);  //note that horizontal velocity component is not affected by gravity
            y = initPos.y + initVel.y * (tT) - 0.5f * g * (tT) * (tT);

            lineRenderer.SetPosition(i, new Vector3(x, y, z));
        }
    }
}