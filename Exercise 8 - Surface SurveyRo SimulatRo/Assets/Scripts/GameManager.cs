using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Terrain terrain;
    float increment;
  
    bool surveyingStarted;

    public GameObject RoVer, RoBox, RoBall, cyli, CAPS;

    //camera array that holds a reference to every camera in the scene
    public Camera[] cameras;

    //current camera
    private int currentCameraIndex;

    // Start is called before the first frame update
    void Start()
    { 
        increment = .01f;

        surveyingStarted = false;
        RoVer.SetActive(false);
        RoBox.SetActive(false);
        RoBall.SetActive(false);
        cyli.SetActive(false);
        CAPS.SetActive(false);

        currentCameraIndex = 0;

        //Turn of all cameras, except the first default one
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        //If any cameras were added to the controller, enable the first one
        if (cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
        }
    }

    public void Update()
    {
        // ----- Camera Controls -----

        //Press number keys 1-5 to directly switch cameras
        if (Input.GetKeyDown(KeyCode.Alpha1))
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

        //Starting the survey; allowing all objects to be activated individually
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            surveyingStarted = true;
        }

        if(surveyingStarted)
        {
            // ----- Activating Objects Individually -----

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                RoVer.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                RoBox.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                RoBall.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                cyli.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                CAPS.SetActive(true);
            }


            // ----- Rover Movement -----

            //Inputs call methods from RoVer Script

            if (Input.GetKey(KeyCode.W))
            {
                RoVer.GetComponent<RoVer>().Thrust();
            }

            if (Input.GetKey(KeyCode.A))
            {
                RoVer.GetComponent<RoVer>().Left();
            }

            if (Input.GetKey(KeyCode.D))
            {
                RoVer.GetComponent<RoVer>().Right();
            }

            if (Input.GetKey(KeyCode.S))
            {
                RoVer.GetComponent<RoVer>().Brake();
            }
        }

        if(!surveyingStarted)
        { 
           if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                terrain.GetComponent<TerrainGeneration>().AdjustHeight(10);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                terrain.GetComponent<TerrainGeneration>().AdjustHeight(-10);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                terrain.GetComponent<TerrainGeneration>().ReRange(false);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                terrain.GetComponent<TerrainGeneration>().ReRange(true);
            } 
      
            if (Input.GetKey(KeyCode.W))
            {
                terrain.GetComponent<TerrainGeneration>().ShiftOriginate(0, increment);
            }

            if (Input.GetKey(KeyCode.S))
            {
                terrain.GetComponent<TerrainGeneration>().ShiftOriginate(0, -increment);
            }

            if (Input.GetKey(KeyCode.A))
            {
                terrain.GetComponent<TerrainGeneration>().ShiftOriginate(-increment, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                terrain.GetComponent<TerrainGeneration>().ShiftOriginate(increment, 0);
            }
        }
    }

    void OnGUI()
    {

        GUI.color = Color.white;    //text color

        GUI.skin.box.fontSize = 20; //font size


        //Displaying the status of the foursome

        GUI.Box(new Rect(10, 10, 320, 50), "RoBox Status: " + (RoBox.activeSelf ? "Deployed" : "Awaiting Command") + "\nPosition: " + RoBox.transform.position.x.ToString("#.00") + " " + RoBox.transform.position.y.ToString("#.00") + " " + RoBox.transform.position.z.ToString("#.00"));

        GUI.Box(new Rect(590, 10, 320, 50), "RoBall Status: " + (RoBall.activeSelf ? "Deployed" : "Awaiting Command") + "\nPosition: " + RoBall.transform.position.x.ToString("#.00") + " " + RoBall.transform.position.y.ToString("#.00") + " " + RoBall.transform.position.z.ToString("#.00"));

        GUI.Box(new Rect(10, 340, 320, 50), "Cyli Status: " + (cyli.activeSelf ? "Deployed" : "Awaiting Command") + "\nPosition: " + cyli.transform.position.x.ToString("#.00") + " " + cyli.transform.position.y.ToString("#.00") + " " + cyli.transform.position.z.ToString("#.00"));

        GUI.Box(new Rect(590, 340, 320, 50), "CAPS Status: " + (CAPS.activeSelf ? "Deployed" : "Awaiting Command") + "\nPosition: " + CAPS.transform.position.x.ToString("#.00") + " " + CAPS.transform.position.y.ToString("#.00") + " " + CAPS.transform.position.z.ToString("#.00"));

        GUI.skin.box.wordWrap = true;  //wrap the text into multiples lines
    }
}