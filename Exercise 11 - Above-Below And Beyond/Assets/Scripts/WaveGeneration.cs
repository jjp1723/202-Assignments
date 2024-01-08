using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveGeneration : MonoBehaviour 
{
	private TerrainData myTerrainData;
    public int xSize, ySize, zSize;
    public Vector3 worldSize;
	public int resolution = 257;			// number of vertices along X and Z axes
	float[,] heightArray;
    Vector3 originate; 
    float deltaZ;
   
    float range = 1.28f;

    //New Fields
    public bool design;

    void Start () 
	{
        myTerrainData = gameObject.GetComponent<TerrainCollider>().terrainData;
        xSize = 200;
        zSize = 200;
        ySize = 50;
        worldSize = new Vector3 (xSize, ySize, zSize);
		myTerrainData.size = worldSize;
		myTerrainData.heightmapResolution = resolution;
		heightArray = new float[resolution, resolution];
        deltaZ = .002f;

        originate = new Vector3(Random.Range(0.0f, 100.0f), 0, Random.Range(0.0f, 100.0f)) ; // start sampling from a random location in the "Sea of PerlinNoise"

        Perlin(originate, resolution, range);

		// Assign values from heightArray into the terrain object's heightmap
		myTerrainData.SetHeights(0, 0, heightArray);
        transform.Translate(-xSize/2, -ySize/2, -zSize/2); //center the Terrain about the origin

        design = true;
    }

    public void Update()
    {
        if(!design)
        {
            originate.z += deltaZ;
            Perlin(originate, resolution, range);
            myTerrainData.SetHeights(0, 0, heightArray);
        }
    }



    // ----- Methods -----
    
    //Perlin Methods
    void Perlin(Vector3 originate, int resolution, float range)
    {

        float xIndex = originate.x;
        float zIndex = originate.z;
        // range = 1.28f; //how far to go out into the "Sea of PerlinNoise" to collect samples
        float stepSize = range / (resolution - 1); //separation between sample spots


        for (int k = 0; k < resolution; k++)
        {
            for (int i = 0; i < resolution; i++)
            {
                xIndex += stepSize;
                heightArray[k, i] = Mathf.PerlinNoise(xIndex, zIndex);
            }

            xIndex = originate.x;  //reset to sweep out along the beginning of the next row

            zIndex += stepSize;  //step forward to the next row
        }
    }

    //AdjustHeight Method
    public void AdjustHeight(int deltaHeight)
    {
        ySize += deltaHeight;
        if (ySize < 0) ySize = 0;
        worldSize.y = ySize;
        myTerrainData.size = worldSize;
        transform.position = new Vector3(-xSize / 2, -ySize / 2, -zSize / 2);  //this re-centers the Terrain around the origin
    }

    //ReRange Method
    public void ReRange(bool stretch)
    {
        if (stretch)
            range = range * 1.10f; //10% increase in range
        else
            range = range * .90f; //10% decrease in range
        Perlin(originate, resolution, range);
        myTerrainData.SetHeights(0, 0, heightArray);
    }

    //ShiftOriginate Method
    public void ShiftOriginate(float xIncrement, float zIncrement)
    {
        //if (xIncrement != 0f)
        originate.x += xIncrement;
        //if (zIncrement != 0f)
        deltaZ += zIncrement;

        Perlin(originate, resolution, range);
        myTerrainData.SetHeights(0, 0, heightArray);
    }
}