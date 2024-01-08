using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject arrow;  //reference set in Inspector to Arrow GameObject in Hierarchy

    //NOTE: if a Component will be used often in Update(), then it is best to have a variable assigned to it since GetComponent<> is relatively inefficient

    private Arrow arrow_script; //for the script componenent of the arrow GameObject

    // Start is called before the first frame update
    void Start()
    {
        arrow_script = arrow.GetComponent<Arrow>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            arrow_script.launched = true;
            arrow_script.released = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            arrow_script.released = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && arrow_script.launched == false)
        {
            arrow_script.AdjustInitAngle(1f);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && arrow_script.launched == false)
        {
            arrow_script.AdjustInitAngle(-1f);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && arrow_script.launched == false)
        {
            arrow_script.AdjustInitSpeed(1f);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && arrow_script.launched == false)
        {
            arrow_script.AdjustInitSpeed(-1f);
        }

        //NOTE:  there are no controls to change the initial velocity;  both the initial speed and the initial direction are set as const in Arrow.cs to 100 m/s and 45 degrees, respectively
    }
}