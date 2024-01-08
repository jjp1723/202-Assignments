using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FameManager : MonoBehaviour
{

    private GameObject localAxes;  //reference to the prefab instance in the hierarchy 
 
    public GameObject modelAxes;  //reference to the prefab in Assets/Prefabs folder, for dynamic instantiation use

    private const int numLocalAxes = 6;
    private GameObject[] localAxesArray;

    private bool posDir;
   
    private Vector3 position, velocity;
    private Vector3 i, j, k; //unit vectors in, respectively, the x, y, z global directions
    float speed = .2f;
    float stepSize = .005f;

    /*
    // Start is called before the first frame update
    void Start()
    {
    
        localAxes = Instantiate(modelAxes, Vector3.zero, Quaternion.identity); 

        posDir = true;

        i = new Vector3(1, 0, 0);  //Vector3.right
        j = new Vector3(0, 1, 0);  //Vector3.up
        k = new Vector3(0, 0, 1);  //Vector3.forward

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))  //R is for resetting the position back to the origin 
        {
            localAxes.transform.position = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            localAxes.transform.Translate(stepSize * (posDir ? +1 : -1) * i);
        }

        else if (Input.GetKey(KeyCode.X))
        {
            velocity = (posDir ? +1 : -1) * speed * i;
            localAxes.transform.position += velocity * Time.deltaTime;

            //equivalently, but presumably less efficient, is the following statement:
            //localAxes.GetComponent<LocalAxes>().transform.position += velocity * Time.deltaTime;  
            //note that it isn't necessary to access the transform's position property this way, via the Script's "built-in" variable for the transform component,
            //but it isn't wrong to do so either because a Script component of a GameObject does has a "built-in" reference to that GameObject's Transform component
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            localAxes.transform.Translate(stepSize * (posDir ? +1 : -1) * j);
        }

        else if (Input.GetKey(KeyCode.Y))
        {
            velocity = (posDir ? +1 : -1) * speed * j;
            localAxes.transform.position += velocity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            localAxes.transform.transform.Translate(stepSize * (posDir ? +1 : -1) * k);
        }

        else if (Input.GetKey(KeyCode.Z))
        {
            velocity = (posDir ? +1 : -1) * speed * k;
            localAxes.transform.position += velocity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.P))  //reverse polarity (i.e. change from + to -, or from - to +)
        {
            posDir = !posDir;
        }
        */
       

          // the code below is used to provide dynamic instantiation of a troupe of six localAxes, but is otherwise the same as the solo Axis act
          // it is what will appear in episode 2 of this Case Study: "CoordinMates"
       
        void Start()
        {
            posDir = true;
                       
            localAxesArray = new GameObject[numLocalAxes];

            for (int m = 0; m < numLocalAxes; m++)
            {
                localAxes = Instantiate(modelAxes, Vector3.zero, Quaternion.identity);
                localAxes.transform.position =  new Vector3(Mathf.Cos(m * (360 / numLocalAxes) * Mathf.Deg2Rad), 0, Mathf.Sin(m * (360 / numLocalAxes) * Mathf.Deg2Rad));  //makes a circle of localAxes in the x-z plane
                localAxesArray[m] = localAxes;
            }
      
            i = new Vector3(1, 0, 0);  //Vector3.right
            j = new Vector3(0, 1, 0);  //Vector3.up
            k = new Vector3(0, 0, 1);  //Vector3.forward

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))  //R is for reset 
            {
               
              for (int m = 0; m < numLocalAxes; m++)
              {
                localAxesArray[m].transform.position = Mathf.Cos(m * (360 / numLocalAxes) * Mathf.Deg2Rad) * i + 0 * j + Mathf.Sin(m * (360 / numLocalAxes) * Mathf.Deg2Rad) * k;  //makes a circle of localAxes in the x-z plane                                                                                                                                                                         
              }

            }


            if (Input.GetKeyDown(KeyCode.X))
            {
               
           
                for (int n = 0; n < numLocalAxes; n++)
                {
                    localAxesArray[n].transform.Translate(stepSize * (posDir ? +1 : -1) * i);
                }
               
            }

            else if (Input.GetKey(KeyCode.X))
            {
               
                for (int m = 0; m < numLocalAxes; m++)
                {
                    velocity = (posDir ? +1 : -1) * speed * i;
                    localAxesArray[m].transform.position += velocity * Time.deltaTime;
                }

            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
              
                for (int n = 0; n < numLocalAxes; n++)
                {
                    localAxesArray[n].transform.Translate(stepSize * (posDir ? +1 : -1) * j);
                }

            }

            else if (Input.GetKey(KeyCode.Y))
            {
                
                for (int m = 0; m < numLocalAxes; m++)
                {
                    velocity = (posDir ? +1 : -1) * speed * j;
                    localAxesArray[m].transform.position += velocity * Time.deltaTime;
                }

            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
             
                for (int n = 0; n < numLocalAxes; n++)
                {
                    localAxesArray[n].transform.Translate(stepSize * (posDir ? +1 : -1) * k);
                }

            }

            else if (Input.GetKey(KeyCode.Z))
            {
               
                for (int m = 0; m < numLocalAxes; m++)
                {
                    velocity = (posDir ? +1 : -1) * speed * k;
                    localAxesArray[m].transform.position += velocity * Time.deltaTime;
                }
               
            }

            if (Input.GetKeyDown(KeyCode.P))  //reverse polarity (i.e. change from + to -, or from - to +)
            {
                posDir = !posDir;
            }



            //NOTE:  these routines involve a group shuffle to produce a wavelike effect as they move up and down out of phase; 
            //       the six axes should each have a uniquely assigned y coordinate 

            /*
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //Debug.Log("in Update() handling UpArrow keypress event");

                float y0 = localAxesArray[0].transform.position.y; //save first y value for the last

                float ym;

                Vector3 position;

                for (int m = 0; m < numLocalAxes - 1; m++)
                {
                    position = localAxesArray[m].transform.position; //save x and z values at i 

                    ym = localAxesArray[m + 1].transform.position.y; //use y value at i+1 to replace y value at i

                    localAxesArray[m].transform.position = new Vector3(position.x, ym, position.z);
                }

                position = localAxesArray[numLocalAxes - 1].transform.position; //save x and z values at last

                localAxesArray[numLocalAxes - 1].transform.position = new Vector3(position.x, y0, position.z);

            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

                float y0 = localAxesArray[numLocalAxes - 1].transform.position.y; //save last y value for the first

                float ym;

                Vector3 position;

                for (int m = numLocalAxes - 1; m > 0; m--)
                {
                    position = localAxesArray[m].transform.position; //save x and z values at i 

                    ym = localAxesArray[m - 1].transform.position.y; //use y value at i-1 to replace y value at i

                    localAxesArray[m].transform.position = new Vector3(position.x, ym, position.z);
                }

                position = localAxesArray[0].transform.position; //save x and z values at first

                localAxesArray[0].transform.position = new Vector3(position.x, y0, position.z);

            }
            */
    }
}