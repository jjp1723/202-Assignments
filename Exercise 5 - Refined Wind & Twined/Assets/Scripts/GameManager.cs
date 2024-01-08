using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //ThinAirBalloon Object Prefab
    public GameObject thinAirBalloon;

    //balloon reference gameObject
    private GameObject balloon;

    //Balloon camera view references
    public GameObject north;
    public GameObject east;
    public GameObject south;
    public GameObject west;
    public GameObject bottom;

    //Terrain reference
    public Terrain terrain;

    //Audio reference
    public GameObject audio;


    public GameObject windVane; //note that this references the prefab WindVane, which is not a GameObject in the Hierarchy, it is in the Assets folder 

    public GameObject[,] windVaneList;  //[,0] holds wind vanes on outer ring, [,1] holds wind vanes on inner ring

    private const int numWindVanes = 24; //actually there are twice this number of them, since there are two rings 

    private float windSpeed;  
    private float windDirection; //this is the angle that the wind velocity vector makes with the z axis, which is 0 initially

    // Start is called before the first frame update
    void Start()
    {
        windSpeed = 0;
        windDirection = 0;

        windVaneList = new GameObject[numWindVanes,2];

        for (int i = 0; i < numWindVanes; i++)
        {
            windVaneList[i,0] = Instantiate(windVane, new Vector3(47 * Mathf.Sin(360f / numWindVanes * i * Mathf.Deg2Rad), 5, 47 * Mathf.Cos(360f / numWindVanes * i * Mathf.Deg2Rad)), Quaternion.identity);
            windVaneList[i,0].transform.localScale = new Vector3(5, 5, 5);
            windVaneList[i, 0].transform.position = new Vector3(windVaneList[i, 0].transform.position.x, terrain.SampleHeight(windVaneList[i, 0].transform.position) + terrain.GetPosition().y + 5f, windVaneList[i, 0].transform.position.z);
            windVaneList[i,1] = Instantiate(windVane, new Vector3(37 * Mathf.Sin(360f / numWindVanes * i * Mathf.Deg2Rad), 5, 33 * Mathf.Cos(360f / numWindVanes * i * Mathf.Deg2Rad)), Quaternion.identity);
            windVaneList[i,1].transform.localScale = new Vector3(5, 5, 5);
            windVaneList[i, 1].transform.position = new Vector3(windVaneList[i, 1].transform.position.x, terrain.SampleHeight(windVaneList[i, 1].transform.position) + terrain.GetPosition().y + 5f, windVaneList[i, 1].transform.position.z);
        }

        //Instantiating and initializing the balloon
        balloon = Instantiate(thinAirBalloon, Vector3.zero, Quaternion.identity);

        //Making the ballon cameras children of the ballon

        north.transform.parent = balloon.transform;
        east.transform.parent = balloon.transform;
        south.transform.parent = balloon.transform;
        west.transform.parent = balloon.transform;
        bottom.transform.parent = balloon.transform;

        //PLay audio
        audio.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            windSpeed += .1f;  //currently does nothing

            //Calling ThinAir's WindVelocity Method
            balloon.GetComponent<ThinAir>().WindVelocity(windSpeed, windDirection);

            Debug.Log("windSpeed has increased to: " + windSpeed);
        }
      
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            windSpeed -= .1f;  //currently does nothing

            //Calling ThinAir's WindVelocity Method
            balloon.GetComponent<ThinAir>().WindVelocity(windSpeed, windDirection);

            Debug.Log("windSpeed has decreased to: " + windSpeed);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            windDirection -= .1f; //note that this represents an angle in radians, not degrees

            //Calling ThinAir's WindVelocity Method
            balloon.GetComponent<ThinAir>().WindVelocity(windSpeed, windDirection);

            for (int i = 0; i < numWindVanes; i++)
            {
                windVaneList[i,0].transform.rotation = Quaternion.Euler(new Vector3(0, windDirection * Mathf.Rad2Deg, 0));
                windVaneList[i,1].transform.rotation = Quaternion.Euler(new Vector3(0, windDirection * Mathf.Rad2Deg, 0));
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        { 

             windDirection += .1f;

            //Calling ThinAir's WindVelocity Method
            balloon.GetComponent<ThinAir>().WindVelocity(windSpeed, windDirection);

            for (int i = 0; i < numWindVanes; i++)
            { 
                windVaneList[i,0].transform.rotation = Quaternion.Euler(new Vector3(0, windDirection * Mathf.Rad2Deg, 0));
                windVaneList[i,1].transform.rotation = Quaternion.Euler(new Vector3(0, windDirection * Mathf.Rad2Deg, 0));
            }
        }
    }


    void OnGUI()
    {

        GUI.color = Color.white;    //text color

        GUI.skin.box.fontSize = 20; //font size

        //Displaying Altitude, Wind Speed, and Direction
        GUI.Box(new Rect(10, 350, 200, 50), "Balloon Altitude: " + (balloon.transform.position.y - 5.5f));

        GUI.Box(new Rect(10, 410, 200, 50), "Wind Speed: " + windSpeed);

        GUI.Box(new Rect(10, 470, 200, 50), "Wind Direction: " + windDirection);

        GUI.skin.box.wordWrap = true;  //wrap the text into multiples lines
    }
}
