using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Arrow1 : MonoBehaviour
{

    //NOTE: this version belongs with AeroNautics, it is not the solution to Exercise 3

    //Magnitude field used to calculate thrust
    private float magnitude;

    private float lambda = 70;  //lambda is fixed at 84 degrees to make an efficient trek up to the thermosphere, and back without collisions with meteors or airplanes
    const float g = 9.8f;
    private float x, y, z;
    private Vector3 thrust;
    private Vector3 gravity = new Vector3(0f, -g, 0f); //constant in magnitude and direction
    private Vector3 acc;
    private Vector3 vel;
    private Vector3 pos;
    private bool moving;
    private GameObject trail;
    public LineRenderer plotRenderer;  //fill in the Inspector
    private Plotter plotter; //the script component of lineRenderer

    private float speed;
    private int angle;
    private float distance;


    // Start is called before the first frame update
    void Start()
    {
        magnitude = 2;
        gravity = new Vector3(0, -g, 0);
        thrust = magnitude * g * new Vector3(Mathf.Cos(lambda * Mathf.Deg2Rad), Mathf.Sin(lambda * Mathf.Deg2Rad), 0f);
   
        plotter = plotRenderer.GetComponent<Plotter>();

        moving = false;
        acc = Vector3.zero;
        vel = Vector3.zero; //velocity, initialized at time 0 using speed v and direction lambda

        x = 0f;  //initial positions
        y = 0f;
        z = 0f;
        pos = new Vector3(x, y, z);
        transform.position = pos;

        trail = transform.GetChild(0).gameObject;  //note how the transform component manages parent/child relationships

        speed = 0;
        angle = 0;
        distance = 0f;
    }

    public void LiftOff()
    {
        acc = gravity + thrust;
        trail.GetComponent<TrailRenderer>().emitting = true; //the rocket's engines may be re-engaged
        moving = true;
    }

    public void CeaseThrust()
    {
        acc = gravity; //note that thrust no longer is being added to gravity
        trail.GetComponent<TrailRenderer>().emitting = false;  //the trek became too exhausting
        PlotTrajectory(pos, vel);
    }

    public void Halt()
    {
        moving = false;
        acc = Vector3.zero;
        vel = Vector3.zero; 
    }

    //AdjustThrustMagnitude Method
    public void AdjustThrustMagnitude(float val)
    {
        magnitude *= val;
        thrust = magnitude * g * new Vector3(Mathf.Cos(lambda * Mathf.Deg2Rad), Mathf.Sin(lambda * Mathf.Deg2Rad), 0f);
        acc = gravity + thrust;
    }

    //AdjustThrustAngle Method
    public void AdjustThrustAngle(float val)
    {
        if(lambda + val > 44f && lambda + val < 91f)
        {
            lambda += val;
            thrust = magnitude * g * new Vector3(Mathf.Cos(lambda * Mathf.Deg2Rad), Mathf.Sin(lambda * Mathf.Deg2Rad), 0f);
            acc = gravity + thrust;
        }
    }

    // Update is called once per frame;
    void Update()
    {
        if (moving)
        {
            //step-by-step euler integration method, based on a linear approximation
            vel = vel + Time.deltaTime * acc;
            pos = pos + Time.deltaTime * vel;
            transform.position = pos;

            if (vel.sqrMagnitude > 0)
            {
                //use this, or the alternatives shown in P_arrow_bolical Plot
                transform.up = vel.normalized;  //NOTE:  only want to do this if vel is not zero!
            }

            speed = vel.magnitude;
            distance += speed * Time.deltaTime; //an estimate of how far rocket travels

            //Halting the rocket upon landing/splashdown
            if (pos.y < 0)
            {
                Halt();
                pos.y = 0;
            }
        }
    }

    public void PlotTrajectory(Vector3 initPos, Vector3 initVel)
    {
        float tT = 0f;
        float timeStep = .2f;
        float x;
        float y;
        float z = 0f;

        plotter.lineRenderer.SetPosition(0, initPos);

        for (int i = 1; i < (plotter.segments + 1); i++)
        {
            tT += timeStep;  //total elapsed time; note that timeStep is in virtual time, not real time
            x = initPos.x + initVel.x * (tT);  //horizontal velocity is not affected by gravity
            y = initPos.y + initVel.y * (tT) - 0.5f * g * (tT) * (tT);
            plotter.lineRenderer.SetPosition(i, new Vector3(x, y, z));
        }
    }

    void OnGUI()
    {

        GUI.color = Color.white;
        GUI.skin.box.fontSize = 20;
        GUI.skin.box.wordWrap = false;

        //note:  must use (int) or else the float digits flicker

        GUI.Box(new Rect(10, 140, 240, 40), "Velocity Speed: " + (int)speed + " m/s");

        angle = (int)(Mathf.Rad2Deg * Mathf.Atan2(vel.y, vel.x));

        GUI.Box(new Rect(10, 180, 240, 40), "Velocity Angle: " + angle + " deg");

        GUI.Box(new Rect(10, 220, 240, 40), "Distance Traveled: " + (int)distance + " m");

        //Displays thrust magnitude and angle
        GUI.Box(new Rect(10, 260, 240, 40), "Thrust Magnitude: " + Math.Round(magnitude, 1) + " g");
        GUI.Box(new Rect(10, 300, 240, 40), "Thrust Angle: " + lambda + " deg");
    }
}
