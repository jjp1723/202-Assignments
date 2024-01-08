using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalipsoControl : MonoBehaviour
{
    private float tT;
    
    public float radius;  //exercise 3 requires writing code to control this using up/down arrow key to +/- orbital radius
    private float orbitsPerUnitTime; 
    public float timeScale;  //exercise 3 requires writing code to control this using left/right arrow keys to -/+ timeScale
    private Vector3 acc, vel, pos;


    //this version starts at the north pole

    void Start()
    {
        tT = 0;
        radius = 100f;
        orbitsPerUnitTime = 16;
        timeScale = 1/16f;
        //vel = radius * (orbitsPerUnitTime) * Vector3.forward;  
        pos = radius * Vector3.up;
        transform.position = pos * -1;
        transform.Rotate(new Vector3(180, 0, 0));  //must adjust the model's initial orientation since we start up above the north pole
    }

    // Update is called once per frame
    void Update()
    {
        tT += Time.deltaTime * timeScale; //note:  it is better to apply timeScale to Time.deltaTime rather than tT, to make vel = d(pos)/dt and acc = d(vel)/dt simpler to compute


        //for reference, this is a unit vector in spherical coordinates that makes an angle of phi with the horizontal x-z plane, and an angle theta with the x axis
        //new Vector3(Mathf.Cos(phi) * Mathf.Cos(theta), Mathf.Sin(phi), Mathf.Cos(phi) * Mathf.Sin(theta)) 


        //NOTE:  DISCUSSION OF THIS CODE WILL BE CONTINUED IN CASE STUDY 12
        /*
        //by properly commenting and uncommenting some of the following statments, there are three different options:

        // using exact acc, then approx vel from acc, and then approx pos from vel
        //acc = radius * (orbitsPerUnitTime) * (orbitsPerUnitTime) * (new Vector3(0, -Mathf.Cos(orbitsPerUnitTime * tT), -Mathf.Sin(orbitsPerUnitTime * tT)));
        //vel += acc * Time.deltaTime * timeScale;
        //pos += vel * Time.deltaTime * timeScale;

        // using exact vel, then approx pos from vel (acc is not computed)
        //vel = radius * (orbitsPerUnitTime) * (new Vector3(0, -Mathf.Sin(orbitsPerUnitTime * tT), Mathf.Cos(orbitsPerUnitTime * tT)));
        //pos += vel * Time.deltaTime * timeScale;
       
        // using exact pos (vel, acc are not computed) 
        pos = radius * (new Vector3(0, Mathf.Cos(orbitsPerUnitTime * tT), Mathf.Sin(orbitsPerUnitTime * tT)));

        //the exact position is based on uniform circular motion: the derivative of pos is vel (always points in the direction of motion, tangent to the orbital circle), 
        //and the derivative of vel is acc (always points towards the center of the earth, aligned with the gravitational force and parallel but opposite in direction to the position vector

        //alternatively, knowing the exact acc (due to gravity alone) points towards the earth's center (origin) the integration w.r.t. time yields vel 
        //after which another integration w.r.t. time yields pos

        */

        pos = radius * (new Vector3(0, Mathf.Cos(orbitsPerUnitTime * tT), Mathf.Sin(orbitsPerUnitTime * tT))); //this parametric curve makes CALIPSO revolve in a circular orbit
        transform.position = pos * -1;

        //these are both equivalent ways to keep the "laser-emitting side" of CALIPSO facing towards the earth's surface
        //we'll discuss this further in a subsequent Case Study . . . 

        //transform.rotation = Quaternion.Euler(orbitsPerUnitTime * (timeScale * Time.deltaTime) * Mathf.Rad2Deg, 0, 0) * transform.rotation;

       transform.up = -transform.position.normalized;//- sign is there because the .fbx 3D model from NASA has the "laser-emitting side" with outward normal in the +y coordinate direction

        
    }
   
}
