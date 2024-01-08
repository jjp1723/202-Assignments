using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoVer : MonoBehaviour
{
    Vector3 pos;
    Vector3 vel;
    Vector3 acc;

    float speed;
    float x, y, z;

    float g = 3.71f;  //gravitational acceleration on Mars 
    Vector3 gravity;

    float thrust = 4f;  //this variable added to give vehicle self-propulsion, must be large enough to overcome gravity

    Vector3 origin;

    Vector3 gradient, parametric, normal;

    public Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        x = 0f;
        z = 0f;
        y = terrain.SampleHeight(new Vector3(x, 1000f, z)) + terrain.GetPosition().y; 
        pos = new Vector3(x, y, z);
        transform.position = pos;

        vel = Vector3.zero;
        acc = Vector3.zero;
        gravity = new Vector3(0, -g, 0);
    }

    // Update is called once per frame
    void Update()
    {
        acc = Vector3.zero;  //reset, to start a new update cycle from scratch

        acc = Vector3.Dot(transform.forward, gravity) * transform.forward; 
        vel = vel + Time.deltaTime * acc;

        //this part added to provide control over ERV/C using WASD keys
        /*
        if (Input.GetKey(KeyCode.W))
        {
            acc = thrust * transform.forward;
            vel = vel + Time.deltaTime * acc;
        }

        if (Input.GetKey(KeyCode.A))
        {
            speed = vel.magnitude;
            if (speed >= .02f)
            {
                transform.Rotate(0f, -1f, 0f, Space.Self); //this maintains transform.up, but changes transform.forward and transform.right
                vel = speed * transform.forward;

            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            speed = vel.magnitude;
            if (speed >= .02f)
            {
                transform.Rotate(0f, 1f, 0f, Space.Self); //this maintains transform.up, but changes transform.forward and transform.right
                vel = speed * transform.forward;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            acc = -thrust * vel.normalized;
            if (vel.magnitude <= thrust * Time.deltaTime)  //to avoid having vehicle move backwards, just stop entirely
            {
                acc = Vector3.zero;
                vel = Vector3.zero;  //but be careful not to set forward vector to zero!
            }
            else
                vel = vel + Time.deltaTime * acc;
        }
        */
        //end of section to provide WASD control code

        x = x + Time.deltaTime * vel.x;
        z = z + Time.deltaTime * vel.z;
        y = terrain.SampleHeight(new Vector3(x,1000f, z)) + terrain.GetPosition().y; 

        transform.position = new Vector3(x, y, z);

        normal = terrain.GetComponent<TerrainCollider>().terrainData.GetInterpolatedNormal((x + 100f) / 200f, (z + 100f) / 200f).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, normal).normalized, normal);
    }



    // ----- Methods -----

    //Methods used below use functionality commented-out of movement code in Update()

    public void Thrust()
    {
        acc = thrust * transform.forward;
        vel = vel + Time.deltaTime * acc;
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
            vel = vel + Time.deltaTime * acc;
    }
}