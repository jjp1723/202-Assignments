using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinAir : MonoBehaviour
{
    //Properties
    private float altitude;
    private Vector3 pos;
    private Vector3 vel;


    //Start is called before the first frame update
    void Start()
    {
        //Instantiating Properties
        altitude = 5.5f;
        pos = new Vector3(0, altitude, 0);
        vel = Vector3.zero;
        transform.position = pos;
    }

    //Update is called once per frame
    void Update()
    {
        //Using the '1' and '0' keys to increase and decrease altitude
        if(Input.GetKey(KeyCode.Alpha1))
        {
            altitude += 0.1f;
            pos = new Vector3(transform.position.x, altitude, transform.position.z);
            //Debug.Log("Position = " + pos);
        }
        if (Input.GetKey(KeyCode.Alpha0))
        {
            altitude -= 0.1f;
            pos = new Vector3(transform.position.x, altitude, transform.position.z);
            //Debug.Log("Position = " + pos);
        }

        //Updating position using velocity and deltaTime
        pos += vel * Time.deltaTime;
        transform.position = pos;
    }

    //WindVelocity Method
    public void WindVelocity(float windSpeed, float windDirection)
    {
        vel = windSpeed * (new Vector3(Mathf.Sin(windDirection), 0, Mathf.Cos(windDirection)));

        //Debug.Log("Windvelocity method called successfully.\nSpeed = " + windSpeed + "\tDirection = " + windDirection);
    }
}
