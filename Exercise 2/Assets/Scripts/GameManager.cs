using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject arrow1;  //set in Inspector
    private Arrow1 arrow1_script;

    // Start is called before the first frame update
    void Start()
    {
        arrow1_script = arrow1.GetComponent<Arrow1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            arrow1_script.LiftOff();

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            arrow1_script.CeaseThrust();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            arrow1_script.Halt();
        }


        //Input for AdjustThrustMagnitude
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            arrow1_script.AdjustThrustMagnitude(1.1f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            arrow1_script.AdjustThrustMagnitude(0.9f);
        }

        //Input for AdjustThrustAngle
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            arrow1_script.AdjustThrustAngle(1f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            arrow1_script.AdjustThrustAngle(-1f);
        }
    }
}
