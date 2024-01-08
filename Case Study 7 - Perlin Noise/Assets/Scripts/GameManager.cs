using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Terrain terrain;
    public GameObject curveBall;
    float increment;

    // Start is called before the first frame update
    void Start()
    {
        //this method must be called after the TerrainGeneration's Start() method! Use Edit > Project Settings > Script Execution Order
        float x,y,z;
        x = 0;
        z = 0;
        y = terrain.SampleHeight(new Vector3(x, 123456, z)) + terrain.GetPosition().y;
        curveBall.transform.position = new Vector3(x, y, z);

        increment = .01f;
    }

    public void Update()
    {

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

    }

}