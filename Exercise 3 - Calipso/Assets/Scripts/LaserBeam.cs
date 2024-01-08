using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private int segments = 1;  //only drawing 1 line segment to go between the origin and the position point of satellite

    public float width;

    public Color color;

    LineRenderer lineRenderer;

    public GameObject calipso; //set in Inspector 

    private Vector3 groundPosition; //where the laser from satellite strikes earth surface

    public float earthRadius; //must be set in Start() 

    void Start()
    {
        color = Color.red;
        width = .08f;
        earthRadius = 50f;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = (segments + 1);
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.startColor = color;  
        lineRenderer.endColor = lineRenderer.startColor;
        lineRenderer.useWorldSpace = true;

        groundPosition = calipso.transform.position * 0.5f;

        lineRenderer.SetPosition(0, groundPosition);
        lineRenderer.SetPosition(1, calipso.transform.position);
    }


    void Update()
    {
        //exercise 3 will require this to be set to the surface off the earth, instead of going all the way down to zero
        //HINT:  consider a unit vector from origin to the satellite, scaled accordingly so it just breaks the earth's surface . . .

        groundPosition = calipso.transform.position * (50f / (calipso.transform.position.magnitude));

        //Debug.Log("Orbit magnitude: " + (-1 + (60f / (calipso.transform.position.magnitude))));

        lineRenderer.SetPosition(0, groundPosition);

        lineRenderer.SetPosition(1, calipso.transform.position);

        //Debug.Log("satellite position is " + calipso.transform.position);
        //Debug.Log("ground position is " + groundPosition);
    }
}
