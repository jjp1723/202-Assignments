using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameras : MonoBehaviour
   
{
    //camera array that holds a reference to every camera in the scene
    public Camera[] cameras;

    //current camera
    private int currentCameraIndex;

    //Use this for initialization
    void Start()
    {
        currentCameraIndex = 0;

        //Turn of all cameras, except the first default one
        for(int i=1; i<cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        //if any cameras were added to the controller, enable the first one
        if(cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
        }
    }

    void Update()
    {
        /*
        //press the 'C' key to cycle through cameras in the array
        if(Input.GetKeyDown(KeyCode.C))
        {
            //cycle to the next camera
            currentCameraIndex++;
            
            //if cameraIndex is in bounds, set this camera active and the last one inactive
            if(currentCameraIndex < cameras.Length)
            {
                cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                cameras[currentCameraIndex].gameObject.SetActive(true);
            }
            //if last camera, cycle back to the first camera
            else
            {
                cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                currentCameraIndex = 0;
                cameras[currentCameraIndex].gameObject.SetActive(true);
            }
        }
        */
        
        //Press number keys 1-7 to directly switch cameras
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            cameras[currentCameraIndex].gameObject.SetActive(false);
            cameras[0].gameObject.SetActive(true);
            currentCameraIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cameras[currentCameraIndex].gameObject.SetActive(false);
            cameras[1].gameObject.SetActive(true);
            currentCameraIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cameras[currentCameraIndex].gameObject.SetActive(false);
            cameras[2].gameObject.SetActive(true);
            currentCameraIndex = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            cameras[currentCameraIndex].gameObject.SetActive(false);
            cameras[3].gameObject.SetActive(true);
            currentCameraIndex = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            cameras[currentCameraIndex].gameObject.SetActive(false);
            cameras[4].gameObject.SetActive(true);
            currentCameraIndex = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            cameras[currentCameraIndex].gameObject.SetActive(false);
            cameras[5].gameObject.SetActive(true);
            currentCameraIndex = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            cameras[currentCameraIndex].gameObject.SetActive(false);
            cameras[6].gameObject.SetActive(true);
            currentCameraIndex = 6;
        }
    }

    // Display instructions to the user:

    void OnGUI()
    {
        //text color
        GUI.color = Color.white;
        //font size
        GUI.skin.box.fontSize = 20;
        GUI.Box(new Rect(10, 10, 200, 50), "Press the 'C' key to switch camera views.");
        //another box for easy readability
        GUI.Box(new Rect(10, 70, 200, 50), "Current camera view: " + cameras[currentCameraIndex].name);
        //wrap the text into multiples lines
        GUI.skin.box.wordWrap = true;
    }
}
