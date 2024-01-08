using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoVerBoard : MonoBehaviour
{
    Vector3 pos;
    Vector3 vel;

    float speed;
    float x, y, z;

    Vector3 normal;

    public Terrain oceanSurface;
    public Terrain oceanFloor;
  
    float[] turnAngles = {-.5f, -.4f, -.3f, -.2f, -.1f, 0f, .1f, .2f, .3f, .4f, .5f};
    float[] speeds = { 0f, 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 9f, 10f };
    int turnIndex = 5; //straight
    int speedsIndex = 0;

    //Reference to each of the foursome
    public GameObject ball;
    public GameObject box;
    public GameObject cyli;
    public GameObject caps;

    //Laser fields
    private int segments = 1;  //only drawing 1 line segment to go between the box and the ocean floor
    public float width;
    LineRenderer lineRenderer;
    float xOffset, zOffset;
    float floorY;
    Vector3 floorPos;

    //Laser Trail fields
    public GameObject trail;

    //Audio Field
    public AudioSource audio;

    //Coordinate and Heading Fields
    private string lat;
    private string lon;
    private string heading;

    // Start is called before the first frame update
    void Start()
    {
        x = 0f;
        z = 0f;
        y = oceanSurface.SampleHeight(new Vector3(x, 100f, z)) + oceanSurface.GetPosition().y + .5f; //RoVer HoVers a little bit above the surface
        pos = new Vector3(x, y, z);
        transform.position = pos;

        floorY = oceanFloor.SampleHeight(new Vector3(x, 100f, z)) + oceanFloor.GetPosition().y;
        floorPos = new Vector3(x + xOffset, floorY, z + zOffset);

        vel = Vector3.zero;
       
        turnIndex = 5;

        xOffset = 0f;
        zOffset = 0f;
        width = 2f;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = (segments + 1);
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.useWorldSpace = true;

        lat = "N";
        lon = "E";
        heading = "North";

        trail.SetActive(false);
        trail.transform.position = floorPos;
    }


    // Update is called once per frame
    void Update()
    {
        vel = vel.magnitude * transform.forward;

        if (Input.GetKeyDown(KeyCode.W))
        {
            //Retrieves the foursome if they are deployed
            if(speedsIndex == 0)
            {
                ball.SetActive(false);
                box.SetActive(false);
                cyli.SetActive(false);
                caps.SetActive(false);

                //Starts drawing the trail
                trail.SetActive(true);

                audio.Play();
            }
            speedsIndex++;
            if (speedsIndex > speeds.Length - 1) speedsIndex = speeds.Length - 1;
            vel = speeds[speedsIndex] * transform.forward;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
             turnIndex--;
                if (turnIndex < 0) turnIndex = 0;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
              turnIndex++;
              if (turnIndex > 10) turnIndex = 10;
        }
          
        if (speedsIndex > 0) //this keeps RoVerBoard in a turn even when A or D is not being pressed
        {
            transform.Rotate(0f, turnAngles[turnIndex], 0f, Space.Self); //this maintains transform.up, but changes transform.forward and transform.right
            vel = speeds[speedsIndex] * transform.forward;

            //Rendering the laser while moving
            floorY = oceanFloor.SampleHeight(new Vector3(x + xOffset, 100f, z + zOffset)) + oceanFloor.GetPosition().y;
            floorPos = new Vector3(x + xOffset, floorY, z + zOffset);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, floorPos);

            //Rendering the Trail
            trail.transform.position = floorPos;

            // ----- Key inputs to move the laser's position on the ocean floor -----
            if(Input.GetKey(KeyCode.UpArrow))
            {
                zOffset++;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                zOffset--;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                xOffset++;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                xOffset--;
            }
        }

        //Stops rendering the Lazer and the Trail when the board isn't moving
        if(speedsIndex == 0)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);

            trail.SetActive(false);
        }
 
        if (Input.GetKeyDown(KeyCode.S))
        {
            speedsIndex--;
            if (speedsIndex < 0) speedsIndex = 0;
            vel = speeds[speedsIndex] * transform.forward;

            //Deploys the Foursome if the board is stopping
            if (speedsIndex == 0)
            {
                ball.SetActive(true);
                box.SetActive(true);
                cyli.SetActive(true);
                caps.SetActive(true);

                ball.GetComponent<RollDown>().Deploy();
                box.GetComponent<RollUp>().Deploy();
                cyli.GetComponent<CyliLevel>().Deploy();
                caps.GetComponent<CAPSLevel>().Deploy();
            }
        }

        x = x + Time.deltaTime * vel.x;
        z = z + Time.deltaTime * vel.z;

        y = oceanSurface.SampleHeight(new Vector3(x, 100f, z)) + oceanSurface.GetPosition().y + .5f; //RoVer HoVers a little bit above the surface
        transform.position = new Vector3(x, y, z);

        floorY = oceanFloor.SampleHeight(new Vector3(x, 100f, z)) + oceanFloor.GetPosition().y;
        floorPos = new Vector3(x + xOffset, floorY, z + zOffset);

        normal = oceanSurface.GetComponent<TerrainCollider>().terrainData.GetInterpolatedNormal((x + 100f) / 200f, (z + 100f) / 200f).normalized;

        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, normal).normalized, normal);
    }



    void OnGUI()
    {
        GUI.color = Color.white;    //text color

        GUI.skin.box.fontSize = 20; //font size


        //Display the current Speed
        GUI.Box(new Rect(10, 10, 320, 30), "Speed: " + speeds[speedsIndex] + " Knots");

        //Displaying the Heading
        if(transform.eulerAngles.y > 350 || transform.eulerAngles.y < 10)
        {
            heading = "North";
        }
        else if(transform.eulerAngles.y >= 10 && transform.eulerAngles.y <= 80)
        {
            heading = "Northeast";
        }
        else if(transform.eulerAngles.y > 80 && transform.eulerAngles.y < 100)
        {
            heading = "East";
        }
        else if(transform.eulerAngles.y >= 100 && transform.eulerAngles.y <= 170)
        {
            heading = "Southeast";
        }
        else if(transform.eulerAngles.y > 170 && transform.eulerAngles.y < 190)
        {
            heading = "South";
        }
        else if(transform.eulerAngles.y >= 190 && transform.eulerAngles.y <= 260)
        {
            heading = "Southwest";
        }
        else if(transform.eulerAngles.y > 260 && transform.eulerAngles.y < 280)
        {
            heading = "West";
        }
        else if(transform.eulerAngles.y >= 280 && transform.eulerAngles.y <= 350)
        {
            heading = "Northwest";
        }
        GUI.Box(new Rect(590, 10, 320, 30), "Heading: " + heading);

        //Displaying the Turning Angle
        GUI.Box(new Rect(590, 50, 320, 30), "Turning Angle: " + turnAngles[turnIndex] + " Degrees");

        //Display the Depth
        GUI.Box(new Rect(10, 50, 320, 30), "Depth: " + Mathf.Round((transform.position.y - floorPos.y)) + " Meters");

        //Display the Coordinates
        if(transform.position.z >= 0)
        {
            lat = "N";
        }
        else
        {
            lat = "S";
        }
        if(transform.position.x >= 0)
        {
            lon = "E";
        }
        else
        {
            lon = "W";
        }
        GUI.Box(new Rect(10, 90, 320, 30), "Latitude: " + Mathf.Abs((Mathf.Round(transform.position.z * 10) / 10)) + lat);
        GUI.Box(new Rect(590, 90, 320, 30), "Longitude: " + Mathf.Abs((Mathf.Round(transform.position.x * 10) / 10)) + lon);

        GUI.skin.box.wordWrap = true;  //wrap the text into multiples lines
    }
}
