using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoBall : MonoBehaviour
{
    //Exercise 7 requires that you program RoBall to traverse a circular path in the CW direction, centered at the origin with radius one-half 
    //the total width (length) of the inclined plane
    //the orbital period should be the same time as it takes RoBox to complete an entire circuit
    //the path can start anywhere on the circle you pick.

    //RoBall should have its position such that its lowest point just touches the surface

    private Vector3 pos;

    public Terrain terrain;

    float ballRadius;
    float maxRadius;

    float theta;

    Vector3 gradient;
    Vector3 normal;

    float time;
    float speed;

    float frames;

    // Start is called before the first frame update
    void Start()
    {
        //Calculating the radius of the ball's orbit
        ballRadius = transform.localScale.y / 2;
        maxRadius = terrain.terrainData.size.x / 2 - ballRadius;

        //Setting the starting position of the ball at the top edge of the terrain
        pos = new Vector3(0f, 0f, maxRadius);
        pos.y = terrain.SampleHeight(pos) + terrain.GetPosition().y + ballRadius;
        transform.position = pos;

        //Rotating the ball so the bottom is tangent to the slope
        theta = 15f;
        normal = new Vector3(0f, Mathf.Cos(Mathf.Deg2Rad * theta), -Mathf.Sin(Mathf.Deg2Rad * theta));
        gradient = new Vector3(0f, -Mathf.Sin(Mathf.Deg2Rad * theta), -Mathf.Cos(Mathf.Deg2Rad * theta));
        transform.rotation = Quaternion.LookRotation(gradient, normal);

        //Setting the time and speed that will be used to move the ball
        time = 0f;
        //Most accurate value I could implement for the speed without it being rounded down by unity to an unusable value
        speed = 0.89f;

        frames = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Using increments in time to move the ball at a set speedS
        time += Time.deltaTime * speed;
        pos.x = Mathf.Sin(time) * maxRadius;
        pos.z = Mathf.Cos(time) * maxRadius;
        pos.y = terrain.SampleHeight(pos) + terrain.GetPosition().y + ballRadius;
        transform.position = pos;

        //Used as reference to get the orbital period of the ball as close as possible to the movement period of the box
        frames++;
        if(transform.position.z >= maxRadius - 0.01f)
        {
            //Debug.Log("Frames for ball to complete motion: " + frames);
        }
    }
}
