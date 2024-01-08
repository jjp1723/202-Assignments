using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject localAxes;  //reference to the prefab instance in the hierarchy 

    private bool posDir;
   
    private Vector3 position, velocity;
    private Vector3 i, j, k; //unit vectors in, respectively, the x, y, z global directions
    float speed = .2f;
    float stepSize = .005f;


    // Start is called before the first frame update
    void Start()
    {
        posDir = true;

        i = new Vector3(1, 0, 0);  //Vector3.right
        j = new Vector3(0, 1, 0);  //Vector3.up
        k = new Vector3(0, 0, 1);  //Vector3.forward

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))  //R is for resetting the position back to the origin 
        {
            localAxes.transform.position = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            localAxes.transform.Translate(stepSize * (posDir ? +1 : -1) * i);
        }

        else if (Input.GetKey(KeyCode.X))
        {
            velocity = (posDir ? +1 : -1) * speed * i;
            localAxes.transform.position += velocity * Time.deltaTime;

            //the following statement will also work, because every script Component has a "built-in" variable
            //that refers to the transform Component of the GameObject to which it is attached;

            //localAxes.GetComponent<LocalAxes>().transform.position += velocity * Time.deltaTime;  
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            localAxes.transform.Translate(stepSize * (posDir ? +1 : -1) * j);
        }

        else if (Input.GetKey(KeyCode.Y))
        {
            velocity = (posDir ? +1 : -1) * speed * j;
            localAxes.transform.position += velocity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            localAxes.transform.transform.Translate(stepSize * (posDir ? +1 : -1) * k);
        }

        else if (Input.GetKey(KeyCode.Z))
        {
            velocity = (posDir ? +1 : -1) * speed * k;
            localAxes.transform.position += velocity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.P))  //reverse polarity (i.e. change from + to -, or from - to +)
        {
            posDir = !posDir;
        }

    }
}