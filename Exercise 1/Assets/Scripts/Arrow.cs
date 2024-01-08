using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
   
   /*

   I shot an arrow into the air, 
   It fell to earth, I knew not where; 
   For, so swiftly it flew, the sight 
   Could not follow it in its flight.

   from The Arrow and the Song
   by Henry Wadsworth Longfellow

   */

    private float v = 100; //magnitude of the initial velocity vector, i.e. initial speed
    private float theta = 45f; // initial direction of the velocity vector
    private const float initialHeight = 2f; //initial height
    private const float g = 9.8f; //this is in units of meters per second per second

    float tT;  //this will keep track of the total elapsed time

    Vector3 acc;
    Vector3 vel;
    Vector3 pos;

    //OldPos vector to help calculate accumulated flight distance
    Vector3 oldPos;

    //Distance private field
    private float distance;

    //Launched public field
    public bool launched;
   
    public bool released; //this is made public so as to be accessible to GameManager

    public GameObject plotRenderer;  //must be set in Inspector

    // Start is called before the first frame update
    void Start()
    {
        launched = false;

        oldPos = new Vector3(0f, initialHeight, 0f);

        distance = 0f;

        tT = 0f;

        released = false;
 
        acc = new Vector3(0f, -g, 0f); //this vector is constant in magnitude and direction (always pointing straight down)

        vel = v * (new Vector3(Mathf.Cos(theta * Mathf.Deg2Rad), Mathf.Sin(theta * Mathf.Deg2Rad), 0f));  //velocity, initialized at time 0 using initial speed v and direction theta

        pos = new Vector3(0, initialHeight, 0); //NOTE: transform.position.y cannot be set to the initial height, it is read-only

        transform.position = pos;

        transform.right = vel.normalized;  //NOTE:  if the arrow image was oriented "up", then the statement would need to be: transform.up = vel.normalized

        plotRenderer.GetComponent<Plotter>().PlotTrajectory(pos, vel);

        //for reference purposes only; this is the formula of the exact solution for the trajectory of a projectile, as used by Plotter
        // (x0, y0) is the initial position, v * < cos(theta), sin(theta) > is the initial velocity
        // (x(t), y(t)) is the position of the projectile at time t 
        /*
        x(t) = x0 + v * Mathf.Cos(theta) * (tT);
        y(t) = y0 + v * Mathf.Sin(theta) * (tT) - 0.5f * g * (tT) * (tT);
        */
    }

    // Update is called once per frame;
    void Update()
    {
        if (released)
        {
            tT += Time.deltaTime;  //total elapsed time

            //update vel and pos, computed on a (time)step-by-step basis using "Euler integration method", which is simply a linear approximation scheme

            vel = vel + Time.deltaTime * acc;  //since acc is constant, this produces an exact (linear) solution for vel
            pos = pos + Time.deltaTime * vel; //this is a good approximation to pos (a quadratic) since vel is linear

            //Updating the accumulated flight distance
            distance += Mathf.Sqrt(Mathf.Pow(pos.x - oldPos.x, 2) + Mathf.Pow(pos.y - oldPos.y, 2));
            oldPos = pos;

            //Update the value of theta
            theta = Mathf.Atan(vel.y / vel.x) * 180 / Mathf.PI;

            transform.position = pos;

            //update the orientation of the arrow so that it points in the direction of the velocity
            if (vel.sqrMagnitude > 0)
            {
                transform.right = vel.normalized; //NOTE: in 2D (in the x-y plane, with transform.forward fixed) the transform.right vector will rotate when transform.up does, and vice-versa
                //and for the sake of comparison, an alternative both correct and incorrect 
                //transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(vel.y, vel.x)));
                //transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(vel.y, vel.x)));  //NOTE:  despite its similarity to the above, THIS METHOD DOESN'T WORK!
            }
        }
    }

    void OnGUI()
    {
        int time, speed;
        GUI.color = Color.white;
        GUI.skin.box.fontSize = 15;
        GUI.skin.box.wordWrap = false;

        //note:  must use (int) or else the float digits flicker

        speed = (int)vel.magnitude;

        time = (int)tT;


        GUI.Box(new Rect(5, 5, 100, 30), "speed = " + speed);

        GUI.Box(new Rect(5, 35, 100, 30), "time = " + time);

        GUI.Box(new Rect(5, 65, 100, 30), "angle = " + Mathf.Round(theta));

        GUI.Box(new Rect(5, 95, 100, 30), "dist = " + Mathf.Round(distance));
    }



    /// ----- Input Methods -----

    //AdjustInitAngle Method
    public void AdjustInitAngle(float val)
    {
        //Keeping the initial angle within a range between 0 and 90
        if(theta + val < 91f && theta + val > -1f)
        {
            theta += val;
            vel = v * (new Vector3(Mathf.Cos(theta * Mathf.Deg2Rad), Mathf.Sin(theta * Mathf.Deg2Rad), 0f));
            transform.right = vel.normalized;
            plotRenderer.GetComponent<Plotter>().PlotTrajectory(pos, vel);
        }
    }

    //AdjustInitSpeed Method
    public void AdjustInitSpeed(float val)
    {
        //Keeping the initial speed greater than 0
        if(v + val > 0)
        {
            v += val;
            vel = v * (new Vector3(Mathf.Cos(theta * Mathf.Deg2Rad), Mathf.Sin(theta * Mathf.Deg2Rad), 0f));
            transform.right = vel.normalized;
            plotRenderer.GetComponent<Plotter>().PlotTrajectory(pos, vel);
        }
    }
}
