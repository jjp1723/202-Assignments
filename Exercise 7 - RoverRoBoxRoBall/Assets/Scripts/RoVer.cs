using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class RoVer : MonoBehaviour
{
    Vector3 pos;
    Vector3 vel;
    Vector3 acc;

    float speed;
    float x, y, z;

    float g = 3.71f;  //as it would be on the planet mars
    Vector3 gravity;

    float thrust = 0.1f;  //this variable represents the magnitude of acceleration due to the self-propulsion;  it should be large enough to overcome gravity
    //the propulsive acceleration vector must be parallel to the forward vector, for instance: thrust*transform.forward can be consider 
    //to either be pushing it or pulling it

    Vector3 zero = new Vector3(0f, 0f, 0f);
    Vector3 origin = new Vector3(0f, 0f, 0f);

    Vector3 gradient;
    Vector3 normal;

    public Terrain terrain;

    float slope;

    float theta;

    // Start is called before the first frame update
    void Start()
    {

        //Our inclined plane will satisfy the explicit equation: y = slope*z, or the implicit equation 0*x+1*y-slope*z = 0 (note that there is no dependency on x)
        //which can be rewritten as 0*x + cos(theta)*y - sin(theta)*z = 0   since slope = tan(theta) = sin(theta)/cos(theta)
        //now recall the point-normal form of a plane equation: <nx, ny, nz> DOT (<x,y,z> - <px, py, pz>) = 0  for normal n to the plane through the point p

        theta = 15f;
        slope = Mathf.Tan(Mathf.Deg2Rad * theta); //tan(theta) = sin(theta)/cos(theta)

        //this vector is the "upward-pointing" normal to the plane: <0, cos, -sin> DOT <x, y, z> = 0  (note that the point p is the origin for our inclinded plane)
        normal = new Vector3(0f, Mathf.Cos(Mathf.Deg2Rad * theta), -Mathf.Sin(Mathf.Deg2Rad * theta));

        //this unit vector points in the direction of steepest descent; it is the opposite of the 
        //unit vector <0, sin(theta), cos(theta) that lies in the z-y plane and points upwards
        gradient = new Vector3(0f, -Mathf.Sin(Mathf.Deg2Rad * theta), -Mathf.Cos(Mathf.Deg2Rad * theta));  
       
        x = 0f;
        z = 0f;
        //Exercise 7:  replace the statement below using Terrain's SampleHeight() and GetPosition() 
        y = terrain.SampleHeight(zero) + terrain.GetPosition().y;
        pos = new Vector3(x, y, z);
        transform.position = pos;

      
        transform.rotation = Quaternion.LookRotation(gradient, normal);
        //NOTE: this does the same as "simultaneously" setting the following
        //transform.up = normal; 
        //transform.forward = gradient; 

        gravity = new Vector3(0, -g, 0);
        vel = zero;
        acc = zero;
    }

    // Update is called once per frame
    void Update()
    {
        acc = zero;  //reset, to start a new update cycle from scratch

        //the projection of gravity onto the forward (unit) vector
        //acc = Vector3.Dot(transform.forward, gravity) * transform.forward;
        acc = (-g * transform.forward.y ) * transform.forward; //equal to the calculation above, but takes advantage of the x and z components of gravity being 0
        vel = vel + Time.deltaTime * acc;

        //NOTE: pos not yet updated, since vel might still be further modified by WASD key presses

        if (Input.GetKey(KeyCode.A))
        {
            speed = vel.magnitude;  //save so that same speed can be maintained after turn is made
            //if (speed >= .02f)
            {
                transform.Rotate(0f, -1f, 0f, Space.Self); //this maintains transform.up, but changes transform.forward and transform.right
                vel = speed * transform.forward;  //keep velocity consistent with vehicle's forward vector
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            speed = vel.magnitude;  //save so that same speed can be maintained after turn is made
            //if (speed >= .02f)
            {
                transform.Rotate(0f, 1f, 0f, Space.Self); //this maintains transform.up, but changes transform.forward and transform.right
                vel = speed * transform.forward;  //keep velocity consistent with vehicle's forward vector
            }
        }


        //Exercise 7:  add propulsion (thrust)
        if (Input.GetKey(KeyCode.W))
        {
            speed += thrust;
            //if (speed >= .02f)
            {
                vel = speed * transform.forward;  //keep velocity consistent with vehicle's forward vector
            }
        }

        //Exercise 7:  add braking
        if (Input.GetKey(KeyCode.S))
        {
            speed += speed * -0.1f;
            if (speed <= .02f && speed >= -0.2f)
            {
                speed = 0f;
            }
            vel = speed * transform.forward;  //keep velocity consistent with vehicle's forward vector
        }

        x = x + Time.deltaTime * vel.x;
        z = z + Time.deltaTime * vel.z;
        //Exercise 7:  replace the statement below using Terrain's SampleHeight() and GetPosition() 
        y = terrain.SampleHeight(transform.position) + terrain.GetPosition().y;

        transform.position = new Vector3(x, y, z);
    }
}