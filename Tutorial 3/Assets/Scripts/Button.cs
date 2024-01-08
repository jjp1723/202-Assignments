using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    //Creating a reference to the ClockHand GameObject
    public GameObject clockHand;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        //Debug.Log("Click Clock");

        //Setting the 'rotate' variable in RotateHand to the opposite value that it is now
        RotateHand script = clockHand.GetComponent<RotateHand>();
        script.rotate ^= true;
    }
}
