using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveVaneManager : MonoBehaviour
{
    public Terrain terrain;

    public GameObject[,] waveVaneList;

    public GameObject waveVane;

    private const int numwaveVanes = 17;

    private float x, y, z;

    private Vector3 gradient, normal;

    Vector3 gravity;

    const float g = 3.71f;


    // Start is called before the first frame update
    void Start()
    {
        gravity = new Vector3(0, -g, 0);

        waveVaneList = new GameObject[numwaveVanes, numwaveVanes];

        for (int k = 0; k < numwaveVanes; k++)
        {
            z = -100f + k * 200f / (numwaveVanes - 1);
            for (int i = 0; i < numwaveVanes; i++)
            {
                x = -100f + i * 200f / (numwaveVanes - 1);
                y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.GetPosition().y;
                waveVaneList[k, i] = Instantiate(waveVane, new Vector3(x, y, z), Quaternion.identity);
                waveVaneList[k, i].transform.localScale = new Vector3(5, 5, 5);
                normal = terrain.GetComponent<TerrainCollider>().terrainData.GetInterpolatedNormal((x + 100f) / 200f, (z + 100f) / 200f).normalized;
                gradient = Vector3.ProjectOnPlane(gravity, normal).normalized;
                if(gradient.sqrMagnitude > .01) //must avoid passing a Zero vector to LookRotation
                    waveVaneList[k, i].transform.rotation = Quaternion.LookRotation(gradient, normal);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
      for (int k = 0; k < numwaveVanes; k++)
      {
          z = -100f + k * 200f / (numwaveVanes - 1);
          for (int i = 0; i < numwaveVanes; i++)
          {
              x = -100f + i * 200f / (numwaveVanes - 1);
              y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.GetPosition().y;
              
              waveVaneList[k, i].transform.position = new Vector3(x, y, z);

              normal = terrain.GetComponent<TerrainCollider>().terrainData.GetInterpolatedNormal((x + 100f) / 200f, (z + 100f) / 200f).normalized;
    
              gradient = Vector3.ProjectOnPlane(gravity, normal).normalized;
              if(gradient.sqrMagnitude > .01) //must avoid passing a Zero vector to LookRotation
                    waveVaneList[k, i].transform.rotation = Quaternion.LookRotation(gradient, normal);
            }
       }
    }



    // ----- ToggleActive Method -----
    public void ToggleActive()
    {
        for (int k = 0; k < numwaveVanes; k++)
        {
            for (int i = 0; i < numwaveVanes; i++)
            {
                waveVaneList[k, i].SetActive(!waveVaneList[k, i].activeSelf);
            }
        }
    }
}
