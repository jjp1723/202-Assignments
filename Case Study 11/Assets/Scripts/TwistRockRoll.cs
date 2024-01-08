using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistRockRoll : MonoBehaviour
{
    float yaw;

    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(0, .5f, 0); //to stand a little ways above _Board

        //Declaring new fields
        yaw = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //Twist aka Yaw
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate(0f, 15f, 0f, Space.Self);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            transform.Rotate(0f, -15f, 0f, Space.Self);
        }

        //Rock aka Pitch
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Rotate(15f, 0f, 0f, Space.Self);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Rotate(-15f, 0f, 0f, Space.Self);
        }

        //Roll
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(0f, 0f, 15f, Space.Self);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(0f, 0f, -15f, Space.Self);
        }
        */
    }



    // ----- Methods -----

    public void Twist(float deg)
    {
        yaw = deg;
        transform.localRotation = Quaternion.Euler(0f, yaw, 0f);
    }

    public void Rock(float deg)
    {
        if(yaw == 0f)
        {
            transform.localRotation = Quaternion.Euler(deg, yaw, 0f);
        }
        if(yaw == 180f)
        {
            transform.localRotation = Quaternion.Euler(-deg, yaw, 0f);
        }
        if(yaw == 90f)
        {
            transform.localRotation = Quaternion.Euler(0f, yaw, deg);
        }
        if(yaw == 270f)
        {
            transform.localRotation = Quaternion.Euler(0f, yaw, -deg);
        }
    }

    public void Roll(float deg)
    {
        if (yaw == 0f)
        {
            transform.localRotation = Quaternion.Euler(0f, yaw, deg);
        }
        if (yaw == 180f)
        {
            transform.localRotation = Quaternion.Euler(0f, yaw, -deg);
        }
        if (yaw == 90f)
        {
            transform.localRotation = Quaternion.Euler(-deg, yaw, 0f);
        }
        if (yaw == 270f)
        {
            transform.localRotation = Quaternion.Euler(deg, yaw, 0f);
        }
    }
}
