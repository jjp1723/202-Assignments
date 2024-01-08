using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject earth;

    public GameObject calipso; //set in the Inspector

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

        //exercise 3 will require the implementation of keyboard event-driven method calls to change +/- the orbital radius in CalipsoControl, 
        //and the timescale in both CalipsoControl and Octahedron Sphere 

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            calipso.GetComponent<CalipsoControl>().radius += 20f;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            calipso.GetComponent<CalipsoControl>().radius -= 20f;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            calipso.GetComponent<CalipsoControl>().timeScale *= 0.5f;
            earth.GetComponent<OctahedronSphere>().timeScale *= 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            calipso.GetComponent<CalipsoControl>().timeScale *= 2f;
            earth.GetComponent<OctahedronSphere>().timeScale *= 2f;
        }
    }

    void OnGUI()
    {
     
        GUI.color = Color.white;
        GUI.skin.box.fontSize = 20;
        GUI.skin.box.wordWrap = false;

        //note:  must use (int) or else the float digits flicker

        //exercise 3 will require that latitude and longitude be computed and displayed

        float phi = Mathf.Asin(calipso.transform.position.y / calipso.transform.position.magnitude) * Mathf.Rad2Deg;

        GUI.Box(new Rect(10, 10, 300, 60), "Elevation Angle: " + (int)phi);

        if (phi > -1f)
        {
            GUI.Box(new Rect(10, 60, 300, 60), "Latitude: " + (int)(phi) + " North");
        }
        else
        {
            GUI.Box(new Rect(10, 60, 300, 60), "Latitude: " + (int)(-1f * phi) + " South");
        }

        int theta = (int)earth.transform.rotation.eulerAngles.y;

        //NOTE:  can't we use rotation.y, it isn't an angle

        //can't use this, because the satellite isn't moving - it is the earth beneath it!

        //theta = Mathf.Rad2Deg * Mathf.Atan2(calipso.transform.position.z, calipso.transform.position.x);

        GUI.Box(new Rect(10, 110, 300, 60), "Azimuthal Angle: " + (theta > 180 ? theta - 360 : theta) );  // -180 < theta <= 180 

        if (theta < 180)
        {
            GUI.Box(new Rect(10, 160, 300, 60), "Longitude: " + theta + " East");
        }
        else
        {
            GUI.Box(new Rect(10, 160, 300, 60), "Longitude: " + (360 - theta) + " West");
        } 

    }
}