using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class RoBox : MonoBehaviour
{

    //Exercise 7 requires that you program RoBox to continually tranverse a square-shaped circuit upon the surface and along the 4 edges of the sloping Terrain, 
    //in CCW order (so go up along the right edge, then left across the top edge, the down along the left edge, the right along the bottom edge, ... )
    //the time to complete traversal should match RoBall's orbital period
    //The path should start at one of the four corners: bottom left, bottom right, top right, top left

    //RoBox should have its position and orientation such that its bottom face is always just touching the surface.

    private Vector3 pos;

    public Terrain terrain;

    float boxMid;

    float offset;

    float maxX;
    float maxZ;
    float minZ;

    float theta;

    Vector3 gradient;
    Vector3 normal;

    float frames;

    void Start()
    {
        //Due to the slope of the terrain, slight adjustments need to be made to the position of the box so that when it rotates, it still lines up with the edges at the top and bottom
        //  otherwise it goes over the enge at the top of the terrain and comes short at the bottom of the terrain
        theta = 15f;
        boxMid = transform.localScale.y / 2;
        offset = Mathf.Abs(boxMid * Mathf.Tan(theta)) / 4f;

        //Debug.Log("Offset: " + offset);

        //Calculating the maximum and minimum x and z coordinates the box can have to get as close to the edge of the terrain as possible
        maxX = terrain.terrainData.size.x / 2 - boxMid;
        maxZ = terrain.terrainData.size.z / 2 - boxMid - offset;
        minZ = -1f * (terrain.terrainData.size.z / 2 - boxMid) - offset;

        //The box starts in the top right corner of the terrain
        pos = new Vector3(maxX, 0, maxZ);
        pos.y = terrain.SampleHeight(pos) + terrain.GetPosition().y + boxMid;
        transform.position = pos;

        //Rotating the box so the bottom is parallel to the slope of the terrain
        normal = new Vector3(0f, Mathf.Cos(Mathf.Deg2Rad * theta), -Mathf.Sin(Mathf.Deg2Rad * theta));
        gradient = new Vector3(0f, -Mathf.Sin(Mathf.Deg2Rad * theta), -Mathf.Cos(Mathf.Deg2Rad * theta));
        transform.rotation = Quaternion.LookRotation(gradient, normal);

        frames = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Moving the box along the top edge
        if(transform.position.z == maxZ && transform.position.x > -maxX)
        {
            pos.x -= 0.5f;
            pos.y = terrain.SampleHeight(pos) + terrain.GetPosition().y + boxMid;
            transform.position = pos;
        }

        //Moving the box along the left edge
        if(transform.position.x == -maxX && transform.position.z > minZ)
        {
            pos.z -= 0.5f;
            pos.y = terrain.SampleHeight(pos) + terrain.GetPosition().y + boxMid;
            transform.position = pos;
        }
        else if(transform.position.x == -maxX && transform.position.z < minZ)
        {
            pos.z = minZ;
            pos.y = terrain.SampleHeight(pos) + terrain.GetPosition().y + boxMid;
            transform.position = pos;
        }

        //Moving the box along the bottom edge
        if(transform.position.z == minZ && transform.position.x < maxX)
        {
            pos.x += 0.5f;
            pos.y = terrain.SampleHeight(pos) + terrain.GetPosition().y + boxMid;
            transform.position = pos;
        }

        //Moving the box along the right edge
        if (transform.position.x == maxX && transform.position.z < maxZ)
        {
            pos.z += 0.5f;
            pos.y = terrain.SampleHeight(pos) + terrain.GetPosition().y + boxMid;
            transform.position = pos;
        }

        //Used as reference for ball movement speed
        frames++;
        if(transform.position.x == maxX && transform.position.z == maxZ)
        {
            //Debug.Log("Frames for box to complete motion: " + frames);
        }
    }
}
