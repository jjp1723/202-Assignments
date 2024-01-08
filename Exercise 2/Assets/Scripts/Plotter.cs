using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plotter : MonoBehaviour
{
    public int segments; 

    public float width;

    public LineRenderer lineRenderer;

    void Start()
    {
        width = 1f;

        segments = 120;  //it should be possible to compute this based on timeStep and when arrow will hit ground

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = (segments + 1);
        lineRenderer.startWidth = width;
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = lineRenderer.startColor;
        lineRenderer.useWorldSpace = true;
    }
   
    //NOTE:  this method now implemented in Arrow1 
    /*
    public void PlotTrajectory(Vector3 initPos, Vector3 initVel)  
    {
        tT = 0f; //need to reset in case this method called again
        float x;
        float y;
        float z = 0f;

        lineRenderer.SetPosition(0, initPos);

        for (int i = 1; i < (segments + 1); i++)
        {
            tT += timeStep;  //total elapsed time; note that timeStep is in virtual time, not real time
            x = initPos.x + initVel.x * (tT);  //horizontal velocity is not affected by gravity
            y = initPos.y + initVel.y * (tT) - 0.5f * g * (tT) * (tT);
            lineRenderer.SetPosition(i, new Vector3(x, y, z));
        }
    }
    */
}