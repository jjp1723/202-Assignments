using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopingPlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //transform.rotation = Quaternion.AngleAxis(-15f, new Vector3(1f, 0f, 0f));
        transform.Rotate(15f, 180f, 0, Space.Self); //180 rotation around y-axis necessary to match the way the texture maps to the Terrain
    }

   
}
