using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Board : MonoBehaviour
{
    Vector3 pos;
    Vector3 vel;
    Vector3 acc;
    float speed;

    float x, y, z;

    Vector3 gradient, normal;

    public Terrain oceanSurface;

    //New fields
    float thrust = 4f;

    // Start is called before the first frame update
    void Start()
    {
        acc = Vector3.zero;
        vel = Vector3.zero;

        x = 0f;
        z = 0f;
        y = oceanSurface.SampleHeight(new Vector3(x, 100f, z)) + oceanSurface.GetPosition().y;
        pos = new Vector3(x, y, z);
        transform.position = pos;

        normal = oceanSurface.GetComponent<TerrainCollider>().terrainData.GetInterpolatedNormal((x + 100f) / 200f, (z + 100f) / 200f).normalized;

        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, normal).normalized, normal);

        transform.Translate(0, .5f, 0, Space.Self);
    }


    // Update is called once per frame
    void Update()
    {
        vel = vel + Time.deltaTime * acc;

        x = x + Time.deltaTime * vel.x;
        z = z + Time.deltaTime * vel.z;

        //Note that x and z don't change in this script, but the height and normal do

        y = oceanSurface.SampleHeight(new Vector3(x, 0, z)) + oceanSurface.GetPosition().y;

        transform.position = new Vector3(x, y, z);

        normal = oceanSurface.GetComponent<TerrainCollider>().terrainData.GetInterpolatedNormal((x + 100f) / 200f, (z + 100f) / 200f).normalized;

        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, normal).normalized, normal);

        transform.Translate(0, .5f, 0, Space.Self);

    }



    // ----- New Methods -----

    public void Thrust()
    {
        acc = thrust * transform.forward;
        vel = vel + Time.deltaTime * acc;

        //Debug.Log("Thrust Velocity: " + vel);
    }

    public void Left()
    {
        speed = vel.magnitude;
        if (speed >= .02f)
        {
            transform.Rotate(0f, -1f, 0f, Space.Self); //this maintains transform.up, but changes transform.forward and transform.right
            vel = speed * transform.forward;
        }
    }

    public void Right()
    {
        speed = vel.magnitude;
        if (speed >= .02f)
        {
            transform.Rotate(0f, 1f, 0f, Space.Self); //this maintains transform.up, but changes transform.forward and transform.right
            vel = speed * transform.forward;
        }
    }

    public void Brake()
    {
        acc = -thrust * vel.normalized;
        if (vel.magnitude <= thrust * Time.deltaTime)  //to avoid having vehicle move backwards, just stop entirely
        {
            acc = Vector3.zero;
            vel = Vector3.zero;  //but be careful not to set forward vector to zero!
        }
        else
        {
            vel = vel + Time.deltaTime * acc;
        }

        //Debug.Log("Brake Velocity: " + vel);
    }
}
