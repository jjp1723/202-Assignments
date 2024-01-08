using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    //Fields
    bool floordesign;
    bool wavedesign;
    bool wait;

    public GameObject floor;
    public GameObject surface;
    public GameObject board;

    float increment;


    // Start is called before the first frame update
    void Start()
    {
        floordesign = true;
        wavedesign = false;
        wait = false;

        increment = .01f;

        surface.SetActive(false);
        board.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ----- Ocean Floor Design Mode -----
        if(floordesign)
        {
            //'Escape' key switches from floor design mode to surface design mode
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                floordesign = false;
                wavedesign = true;
                surface.SetActive(true);

                //'wait' ensures that the surface design isnt skipped while transitioning from the floor design
                wait = true;

                Debug.Log("Switching from floor design to surface design");
            }


            //Floor Design Actions
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                floor.GetComponent<FloorGeneration>().AdjustHeight(10);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                floor.GetComponent<FloorGeneration>().AdjustHeight(-10);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                floor.GetComponent<FloorGeneration>().ReRange(false);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                floor.GetComponent<FloorGeneration>().ReRange(true);
            }

            if (Input.GetKey(KeyCode.W))
            {
                floor.GetComponent<FloorGeneration>().ShiftOriginate(0, increment);
            }

            if (Input.GetKey(KeyCode.S))
            {
                floor.GetComponent<FloorGeneration>().ShiftOriginate(0, -increment);
            }

            if (Input.GetKey(KeyCode.A))
            {
                floor.GetComponent<FloorGeneration>().ShiftOriginate(-increment, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                floor.GetComponent<FloorGeneration>().ShiftOriginate(increment, 0);
            }
        }


        
        // ----- Ocean Surface Design Mode -----
        if(wavedesign)
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                wait = false;
            }

            //'Escape' key switches from surface design mode to simulation mode
            if (Input.GetKeyDown(KeyCode.Escape) && !wait)
            {
                wavedesign = false;
                surface.GetComponent<WaveGeneration>().design = false;
                board.SetActive(true);

                Debug.Log("Switching from surface design to simulation");
            }

            //Surface Design Actions
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                surface.GetComponent<WaveGeneration>().AdjustHeight(10);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                surface.GetComponent<WaveGeneration>().AdjustHeight(-10);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                surface.GetComponent<WaveGeneration>().ReRange(false);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                surface.GetComponent<WaveGeneration>().ReRange(true);
            }

            if (Input.GetKey(KeyCode.W))
            {
                surface.GetComponent<WaveGeneration>().ShiftOriginate(0, increment);
            }

            if (Input.GetKey(KeyCode.S))
            {
                surface.GetComponent<WaveGeneration>().ShiftOriginate(0, -increment);
            }

            if (Input.GetKey(KeyCode.A))
            {
                surface.GetComponent<WaveGeneration>().ShiftOriginate(-increment, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                surface.GetComponent<WaveGeneration>().ShiftOriginate(increment, 0);
            }
        }



        // ----- Simulation Mode -----
        if(!floordesign && !wavedesign)
        {

        }
    }
}
