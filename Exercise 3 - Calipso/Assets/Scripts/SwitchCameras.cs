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
    }

    // Display instructions to the user:
    /*
    void OnGUI()
    {
        //text color
        GUI.color = Color.white;
        //font size
        GUI.skin.box.fontSize = 20;
        GUI.Box(new Rect(10, 10, 200, 100), "Press the 'c' key to switch camera views.");
        //another box for easy readability
        GUI.Box(new Rect(10, 120, 200, 100), "Current camera view: " + cameras[currentCameraIndex].name);
        //wrap the text into multiples lines
        GUI.skin.box.wordWrap = true;
    }
    */
}
