using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager : MonoBehaviour
{
    //Terrain reference
    public Terrain terrain;

    GameObject[] helices;
    [SerializeField] GameObject helixPrefab; //ref to prefab in Assets > Models, set in Inspector
    private Helix helix;  //reference to the script component of the instantiated helixPrefab 
    private const int numHelices = 100;
    private bool randRadius; //true for random radius, false for random center 

    // Start is called before the first frame update
    void Start()
    {
        
        // this part illustrates uniform random choices for center, radius, color and band width
                
        helices = new GameObject[numHelices];
        for(int i=0; i < helices.Length; i++)
        {
            helices[i] = Instantiate(helixPrefab, new Vector3(Random.Range(0f, 10f), 0f, Random.Range(0f, 10f)), Quaternion.identity);
            helix = helices[i].GetComponent<Helix>();

            //Uniform Distrubution for NumWinds
            helix.numWinds = Random.Range(1, 5);

            //Uniform Distribution for Direction
            helix.numWinds *= Random.Range(0, 2) * 2 - 1;

            //Non-Uniform Distribution for Radius
            float rad = Random.Range(0f, 1f);
            if (rad < .6f)
            {
                helix.radius = Random.Range(1f, 4f);
            }
            else if (rad < .9)
            {
                helix.radius = Random.Range(4f, 8f);
            }
            else
            {
                helix.radius = Random.Range(8f, 10f);
            }

            //Non-Uniform Distribution for Width
            float wide = Random.Range(0f, 1f);
            if (wide <.4f)
            {
                helix.width = Random.Range(.01f, .1f);
            }
            else if (wide < 0.6f)
            {
                helix.width = Random.Range(0.5f, 1.5f);
            }
            else
            {
                helix.width = Random.Range(0.1f, 0.5f);
            }

            //Gausian Distribution for the Center
            helix.transform.position = new Vector3(Gaussian(-20f, 20f), 0f, Gaussian(-20f, 20f));
            helix.transform.position = new Vector3(helix.transform.position.x, terrain.SampleHeight(helix.transform.position) + terrain.GetPosition().y, helix.transform.position.z);

            //Gausian Distribution for Height
            helix.height = Gaussian(100f, 20f);

            //Uniform Distribution for Color
            helix.color = Random.ColorHSV();

            helix.GenHelix();
        }

        randRadius = true; 
    }


    //this code compares the use of random number generation with uniform, non-uniform, and Gaussian distributions to determine either the radius of a helix, or the x-coordinate 
    //location of its center.  When the radius is randomly generated, all of the helices' centers are at (0,0,0)
    //When the x-coordinate of the center is randomly generated, all of helices' radii are fixed at 5 and the z-coordinate is 0

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) //P will select the "position" of helix's center to be randomly generated
            randRadius = false;
        if (Input.GetKeyDown(KeyCode.R))  //R will select the "radius" of helix to be randomly generated
            randRadius = true;

        if (randRadius)  //randRadius is true, so choose a random radius and use a constant center position  
        {
            if (Input.GetKeyDown(KeyCode.G)) // use Gaussian distribution for radius
            {

                for (int i = 0; i < helices.Length; i++)
                {
                    helix = helices[i].GetComponent<Helix>();
                    helix.radius = Gaussian(5f, 2f);
                    helix.color = Color.gray;
                    helix.width = .1f;
                    helix.height = Random.Range(1f, 150f);
                    helix.numWinds = Random.Range(1, 5);
                    helix.transform.position = Vector3.zero;
                    helix.GenHelix();
                }
            }

            if (Input.GetKeyDown(KeyCode.N)) // use non-uniform distribution for radius 
            {
                for (int i = 0; i < helices.Length; i++)
                {
                    helix = helices[i].GetComponent<Helix>();
                    helix.color = Color.gray;
                    helix.width = .1f;
                    helix.height = Random.Range(1f, 150f);
                    helix.numWinds = Random.Range(1, 5);

                    float pick = Random.Range(0f, 1f);
                    if (pick < .6f)
                    {
                        helix.radius = Random.Range(1f, 4f);
                    }
                    else if (pick < .9) //and pick >= .6
                    {
                        helix.radius = Random.Range(4f, 8f);
                    }
                    else //pick >= .9
                    {
                        helix.radius = Random.Range(8f, 10f);
                    }

                    helix.transform.position = Vector3.zero;

                    helix.GenHelix();
                }
            }

            if (Input.GetKeyDown(KeyCode.U)) //use uniform distribution for radius
            {
                for (int i = 0; i < helices.Length; i++)
                {
                    helix = helices[i].GetComponent<Helix>();
                    helix.radius = Random.Range(1f, 10f);
                    helix.color = Color.gray;
                    helix.width = .1f;
                    helix.height = Random.Range(1f, 150f);
                    helix.numWinds = Random.Range(1, 5);
                    helix.transform.position = Vector3.zero;
                    helix.GenHelix();
                }
            }
        }
        else //randRadius is false, so use a constant radius value and choose a random center position instead 
        {
            if (Input.GetKeyDown(KeyCode.G)) // use Gaussian distribution for center along the x-axis
            {
                for (int i = 0; i < helices.Length; i++)
                {
                    helix = helices[i].GetComponent<Helix>();
                    helix.radius = 5;
                    helix.color = Color.gray;
                    helix.width = .1f;
                    helix.height = Random.Range(1f, 150f);
                    helix.numWinds = Random.Range(1, 5);
                    helix.transform.position = new Vector3(Gaussian(0f, 5f), 0f, 0);
                    helix.GenHelix();
                }
            }

            if (Input.GetKeyDown(KeyCode.N)) // use non-uniform distribution for center along the x-axis
            {
                for (int i = 0; i < helices.Length; i++)
                {
                    helix = helices[i].GetComponent<Helix>();
                    helix.radius = 5;
                    helix.color = Color.gray;
                    helix.width = .1f;
                    helix.height = Random.Range(1f, 150f);
                    helix.numWinds = Random.Range(1, 5);

                    float pick = Random.Range(0f, 1f);
                    if (pick < .6f)
                    {
                        helices[i].transform.position = new Vector3(Random.Range(-10,-3f), 0f, 0);
                    }
                    else if (pick < .9) //and pick >= .6
                    {
                        helices[i].transform.position = new Vector3(Random.Range(-3f, 4f), 0f, 0);
                    }
                    else //pick >= .9
                    {
                        helices[i].transform.position = new Vector3(Random.Range(4f, 10f), 0f, 0);
                    }
                    helix.GenHelix();
                }
            }

            if (Input.GetKeyDown(KeyCode.U)) //use uniform distribution for center along the x-axis
            {
                for (int i = 0; i < helices.Length; i++)
                {
                    helix = helices[i].GetComponent<Helix>();
                    helix.radius = 5;
                    helix.color = Color.gray;
                    helix.width = .1f;
                    helix.height = Random.Range(1f, 150f);
                    helix.numWinds = Random.Range(1, 5);
                    helices[i].transform.position = new Vector3(Random.Range(-10f, 10f), 0f, 0);
                    helix.GenHelix();
                }
            }
        }
    }

    float Gaussian(float mean, float stdDev)
    {
        float val1 = Random.Range(0f, 1f);
        float val2 = Random.Range(0f, 1f);
        float gaussValue = Mathf.Sqrt(-2.0f * Mathf.Log(val1)) *
            Mathf.Sin(2.0f * Mathf.PI*val2);
        return mean + stdDev * gaussValue;
     }

    void OnGUI()
    {
      
        GUI.color = Color.white;    //text color

        GUI.skin.box.fontSize = 20; //font size

        GUI.Box(new Rect(10, 130, 200, 100), "Press R or P for Random Radius or Random Position");
       
        GUI.Box(new Rect(10, 240, 200, 100), "Then Press U, N, or G for Uniform, Non-Uniform, or Gaussian Distribution");
       
        GUI.skin.box.wordWrap = true;  //wrap the text into multiples lines
    }
}
