using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHand : MonoBehaviour
{
    //Rotate boolean variable
    public bool rotate;

    // Start is called before the first frame update
    void Start()
    {
        //Instatiating rotate as false
        rotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Ensuring that the RotateMouse method is only called when 'rotate' is true
        if(rotate)
        {
            //Calling the RotateToMouse Method
            RotateToMouse();
        }
    }

    //RotateToMouse Method
    public void RotateToMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(mouseWorldPos.y, mouseWorldPos.x) * 180 / Mathf.PI);
    }
}
