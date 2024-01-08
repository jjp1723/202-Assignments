using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TerrainGeneration : MonoBehaviour 
{

	private TerrainData myTerrainData;
    public int xSize, ySize, zSize;
    public Vector3 worldSize;
	public int resolution;  //number of vertices in the mesh along X and Z axes, resolution = 1 + 2^p (must equal 1 plus a power of 2) 
	float[,] heightArray;
    Vector3 originate; 
    private float range;

    void Start () 
	{
        myTerrainData = gameObject.GetComponent<TerrainCollider>().terrainData;

        ySize = 50;
        xSize = 200;
        zSize = 200;
		worldSize = new Vector3 (xSize, ySize, zSize);
		myTerrainData.size = worldSize;

        resolution = 257;
		heightArray = new float[resolution, resolution];
        myTerrainData.heightmapResolution = resolution;

        originate = new Vector3(65.78028f, 0, 28.30647f);  //a particular value that will produce a closed-loop path that cyli and CAPS will traverse
        //originate = new Vector3(Random.Range(50.0f, 100.0f), 0, Random.Range(50.0f, 100.0f)) ; // start sampling from a random location in PerlinNoiseLand

        range = 1.28f;

        Perlin(originate, resolution, range);

		// Assign values from heightArray into the terrain object's heightmap
		myTerrainData.SetHeights(0, 0, heightArray);

        transform.position = new Vector3(-xSize / 2, -ySize / 2, -zSize / 2);  //this centers the Terrain around the origin
    }

    void Perlin(Vector3 originate, int resolution, float range)
    {
        //range indicates how far to go in horizontal and vertical directions to collect samples, within the space we'll call "PerlinNoiseLand", 
        float xIndex = originate.x;
        float zIndex = originate.z;

        float stepSize = range / (resolution - 1); //separation between the sampling spots/locations

        for (int k = 0; k < resolution; k++)
        {
            for (int i = 0; i < resolution; i++)
            {
                xIndex += stepSize;
                heightArray[k, i] = Mathf.PerlinNoise(xIndex, zIndex);
                //comment the above, then uncomment below, and observe what happens when the WASD keys are pressed to translate the originate point
                //heightArray[i, k] = Mathf.PerlinNoise(xIndex, zIndex);
            }

            xIndex = originate.x;  //reset to first column, so as to sweep out along the beginning of the next row

            zIndex += stepSize;  //step forward to the next row
        }
    }

    public void ShiftOriginate(float xIncrement, float zIncrement)
    {
        //if (xIncrement != 0f)
        originate.x += xIncrement;
        //if (zIncrement != 0f)
        originate.z += zIncrement;

        Perlin(originate, resolution, range);
        myTerrainData.SetHeights(0, 0, heightArray);
    }

    public void AdjustHeight(int deltaHeight)
    {
        ySize += deltaHeight;
        if (ySize < 0) ySize = 0;
        worldSize.y = ySize;
        myTerrainData.size = worldSize;
        transform.position = new Vector3(-xSize / 2, -ySize / 2, -zSize / 2);  //this re-centers the Terrain around the origin
    }

    public void ReRange(bool stretch)
    {
        if(stretch)
            range = range * 1.10f; //10% increase in range
        else
            range = range * .90f; //10% decrease in range
        Perlin(originate, resolution, range);
        myTerrainData.SetHeights(0, 0, heightArray);
    }
}