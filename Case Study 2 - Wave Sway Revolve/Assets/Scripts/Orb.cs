using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    private Vector3 pos;
    public float phaseAngle;  //set by WaveManager script
    public float orbitalRadius; //set by WaveManager script
    public Vector3 center;  //set by WaveManager script
    private const float halfPI = .5f * Mathf.PI;

    private float theta;  //time converted into angle of orbit

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(.4f, .4f, .4f);
        //phaseAngle = 0f; //in radians, set by WaveManager
        //orbitalRadius = 1f; // set by WaveManager
        theta = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        theta += Time.deltaTime;  
        pos = center + new Vector3(orbitalRadius * Mathf.Cos(halfPI*theta + phaseAngle), orbitalRadius * Mathf.Sin(halfPI * theta + phaseAngle), 0f);
        transform.position = pos;
    }
}