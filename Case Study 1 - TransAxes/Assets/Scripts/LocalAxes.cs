using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalAxes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))  //reset to default orientation
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            //this sets the rotation property to a particular value, independent of the current orientation (rotate value)
            //alternatively, can use transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));

        //Rotate uses Euler angles, but applies a rotation transformation (specified using Euler angles) to the current orientation  

        if (Input.GetKeyDown(KeyCode.Alpha1))
            transform.Rotate(0.0f, 10.0f, 0.0f, Space.Self); //Yaw - rotation about the local y-axis  

        if (Input.GetKeyDown(KeyCode.Alpha2))
            transform.Rotate(5.0f, 0.0f, 0.0f, Space.Self); //Pitch - rotation about the local x-axis

        if (Input.GetKeyDown(KeyCode.Alpha3))
            transform.Rotate(0.0f, 0.0f, 15.0f, Space.Self); //Roll - rotation about the local z-axis

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            transform.Rotate(0.0f, 10.0f, 0.0f, Space.Self); //Yaw - rotation about the local y-axis
            transform.Rotate(5.0f, 0.0f, 0.0f, Space.Self); //Pitch - rotation about the local x-axis
            transform.Rotate(0.0f, 0.0f, 15.0f, Space.Self); //Roll - rotation about the local z-axis
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
            transform.Rotate(5.0f, 10.0f, 15.0f, Space.Self); //same result as case 4  [Y] -> [X] -> [Z]  for local

        if (Input.GetKeyDown(KeyCode.Alpha6))  //case 6 issues rotations in the reverse order from case 4, showing that order makes a difference; 
        {
            transform.Rotate(0.0f, 0.0f, 15.0f, Space.Self); //Roll - rotation about the local z-axis
            transform.Rotate(5.0f, 0.0f, 0.0f, Space.Self); //Pitch - rotation about the local x-axis
            transform.Rotate(0.0f, 10.0f, 0.0f, Space.Self); //Yaw - rotation about the local y-axis 
        }

        if (Input.GetKeyDown(KeyCode.Alpha7)) //case 7 issues rotations in the same order as 6, but with global instead of local as a reference coordinate system
        {
            transform.Rotate(0.0f, 0.0f, 15.0f, Space.World); //Roll - rotation about the global z-axis
            transform.Rotate(5.0f, 0.0f, 0.0f, Space.World); //Pitch - rotation about the global x-axis
            transform.Rotate(0.0f, 10.0f, 0.0f, Space.World); //Yaw - rotation about the global y-axis 
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
            transform.Rotate(5.0f, 10.0f, 15.0f, Space.World); //same result as case 7   [Y] <- [X] <- [Z]  for global


        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            transform.Rotate(0.0f, 0.0f, -15.0f, Space.Self);
            transform.Rotate(-5.0f, 0.0f, 0.0f, Space.Self);
            transform.Rotate(0.0f, -10.0f, 0.0f, Space.Self); //this should put the cube back to default orientation, following cases 1+2+3, 4, 6, 7, or 8
        }

    }
}
