using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAnalyzer : MonoBehaviour
{
    int numberRows = 10;
    int numberColumns = 12;

    int distance = 2; //distance from one ball's center to a neighboring ball's center, the same here in both horizontal and vertical directions 
    public Vector3 center;  

    private GameObject[,] circles;
    public GameObject circle; //reference to the prefab in Project > Assets > Models, set in the Inspector

    private GameObject[,,] lineSegments;  // [,,0] for horizontal, [,,1] for vertical
    public GameObject lineSegment; //reference to the prefab in Project > Assets > Models, set in the Inspector


    bool drawOrbits = false;
    bool drawHorizontalSegments = false;
    bool drawVerticalSegments = false;

    public GameObject[,] orbs;  //set in WaveGenerator after it initializes array

    // Start is called before the first frame update
    void Start()
    {

        circles = new GameObject[numberRows,numberColumns];
        lineSegments = new GameObject[numberRows, numberColumns, 2];

        for (int m = 0; m < numberRows; m++)
        {

            for (int n = 0; n < numberColumns; n++)
            {
                center = new Vector3(distance * n, distance * m, 0);
                circles[m, n] = Instantiate(circle, center, Quaternion.identity);
                circles[m, n].GetComponent<Circle>().radius = .1f * (m + 1); //chosen so that the radii changes linearly with depth; .1 is just an arbitrary pick
                circles[m, n].SetActive(false);

                for (int k = 0; k < 2; k++)  //k = 0 horizontal, 1 vertical
                {
                    lineSegments[m, n, k] = Instantiate(lineSegment, Vector3.zero, Quaternion.identity);

                    lineSegments[m, n, k].SetActive(false);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        int m, n;
        Vector3 beginPoint, endPoint;

        //NOTE:  the circles don't change, but the line segments do

        if (drawHorizontalSegments)
        {
            for (m = 0; m < numberRows; m++)
            {
                for (n = 1; n < numberColumns; n++)  //skip n = 0 case for horizontal line segment
                {
                    beginPoint = orbs[m, n - 1].transform.position;
                    endPoint = orbs[m, n].transform.position;
                    lineSegments[m, n, 0].GetComponent<LineSegment>().SetPoints(beginPoint, endPoint);
                }
            }
        }

        if (drawVerticalSegments)
        {
          
            for (m = 1; m < numberRows; m++) //skip m = 0 case for vertical line segment
            {
                for (n = 0; n < numberColumns; n++)
                {
                    beginPoint = orbs[m - 1, n].transform.position;
                    endPoint = orbs[m, n].transform.position;
                    lineSegments[m, n, 1].GetComponent<LineSegment>().SetPoints(beginPoint, endPoint);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (drawOrbits)
            {
                drawOrbits = false;
                for (m = 0; m < numberRows; m++)
                {
                    for (n = 0; n < numberColumns; n++)
                    {
                        circles[m, n].SetActive(false);
                    }
                }
            }
            else //drawOrbits is false
            {
                drawOrbits = true;
                for (m = 0; m < numberRows; m++)
                {
                    for (n = 0; n < numberColumns; n++)
                    {
                        circles[m, n].SetActive(true);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (drawHorizontalSegments)
            {
                drawHorizontalSegments = false;
                for (m = 0; m < numberRows; m++)
                {
                    for (n = 0; n < numberColumns; n++)
                    {
                        lineSegments[m, n, 0].SetActive(false);
                    }
                }
            }

            else //drawHorizontalSegments is false
            {
                drawHorizontalSegments = true;
                for (m = 0; m < numberRows; m++)
                {
                    for (n = 0; n < numberColumns; n++)
                    {
                        lineSegments[m, n, 0].SetActive(true);
                    }
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (drawVerticalSegments)
            {
                drawVerticalSegments = false;
                for (m = 0; m < numberRows; m++)
                {
                    for (n = 0; n < numberColumns; n++)
                    {
                        lineSegments[m, n, 1].SetActive(false);
                    }
                }
            }

            else //drawVerticalSegments is false
            {
                drawVerticalSegments = true;
                for (m = 0; m < numberRows; m++)
                {
                    for (n = 0; n < numberColumns; n++)
                    { 
                        lineSegments[m, n, 1].SetActive(true);
                    }
                }
            }
        }
    }  
}