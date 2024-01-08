using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager : MonoBehaviour
{
    //Terrain reference
    public Terrain terrain;

    //Seaweed Fields
    GameObject[] helices;
    [SerializeField] GameObject helixPrefab; //ref to prefab in Assets > Models, set in Inspector
    private Helix helix;  //reference to the script component of the instantiated helixPrefab 
    private const int numHelices = 100;
    private bool randRadius; //true for random radius, false for random center

    //Rock Fields
    GameObject[] rocks;
    [SerializeField] GameObject rockObject;
    private GameObject rock;
    private const int numRocks = 20;

    //Coral Fields
    GameObject[] corals;
    private GameObject coral;
    private const int numCoral = 100;

    // Start is called before the first frame update
    void Start()
    {
        // ----- Seaweed Generation -----

        //This part illustrates uniform random choices for center, radius, color and band width
        helices = new GameObject[numHelices];
        for(int i = 0; i < helices.Length; i++)
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
            helix.transform.position = new Vector3(Gaussian(0f, 30f), 0f, Gaussian(0f, 30f));
            helix.transform.position = new Vector3(helix.transform.position.x, terrain.SampleHeight(helix.transform.position) + terrain.GetPosition().y, helix.transform.position.z);

            //Gausian Distribution for Height
            helix.height = Gaussian(50f, 20f);

            //Uniform Color
            helix.color = Color.green;

            helix.GenHelix();
        }

        randRadius = true;



        // ----- Rock Generation -----

        rocks = new GameObject[numRocks];
        for(int i = 0; i < rocks.Length; i++)
        {
            rocks[i] = Instantiate(rockObject, new Vector3(Random.Range(0f, 10f), 0f, Random.Range(0f, 10f)), Quaternion.identity);
            rock = rocks[i];

            //Non-Uniform Distribution for Size
            float rad = Random.Range(0f, 1f);
            if (rad < .6f)
            {
                rock.transform.localScale = new Vector3(12, 12, 12);
            }
            else if (rad < .9)
            {
                rock.transform.localScale = new Vector3(18, 18, 18);
            }
            else
            {
                rock.transform.localScale = new Vector3(32, 32, 32);
            }

            //Gausian Distribution for the Center
            rock.transform.position = new Vector3(Gaussian(0f, 30f), 0f, Gaussian(0f, 30f));
            rock.transform.position = new Vector3(rock.transform.position.x, terrain.SampleHeight(rock.transform.position) + terrain.GetPosition().y, rock.transform.position.z);

            //Uniform Color
            rock.GetComponent<Renderer>().material.color = Color.gray;
        }



        // ----- Coral Generation -----

        corals = new GameObject[numCoral];
        for (int i = 0; i < corals.Length; i++)
        {
            corals[i] = Instantiate(rockObject, new Vector3(Random.Range(0f, 10f), 0f, Random.Range(0f, 10f)), Quaternion.identity);
            coral = corals[i];

            //Non-Uniform Distribution for Size
            float rad = Random.Range(0f, 1f);
            if (rad < .6f)
            {
                coral.transform.localScale = new Vector3(6, 6, 6);
            }
            else if (rad < .9)
            {
                coral.transform.localScale = new Vector3(9, 9, 9);
            }
            else
            {
                coral.transform.localScale = new Vector3(2, 12, 12);
            }

            //Gausian Distribution for the Center
            coral.transform.position = new Vector3(Gaussian(0f, 40f), 0f, Gaussian(0f, 40f));
            coral.transform.position = new Vector3(coral.transform.position.x, terrain.SampleHeight(coral.transform.position) + terrain.GetPosition().y, coral.transform.position.z);

            //Uniform Distribution for Color
            coral.GetComponent<Renderer>().material.color = Random.ColorHSV();
        }
    }
    void Update()
    {

    }

    float Gaussian(float mean, float stdDev)
    {
        float val1 = Random.Range(0f, 1f);
        float val2 = Random.Range(0f, 1f);
        float gaussValue = Mathf.Sqrt(-2.0f * Mathf.Log(val1)) *
            Mathf.Sin(2.0f * Mathf.PI*val2);
        return mean + stdDev * gaussValue;
    }
}
