using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockNumbers : MonoBehaviour
{
    //Reference variable to the ClockNumber GameObject
    public GameObject clockNumber;

    //Creating an array to hold 12 dynamically instantiated ClockNumber GameObjects
    private GameObject[] numbers;

    // Start is called before the first frame update
    void Start()
    {
        numbers = new GameObject[12];

        for(int num = 1; num < 13; num++)
        {
            //Calculating the corresponding radian ammount to where the current number would be placed on a clock
            float radian = 3 * Mathf.PI / 6 - Mathf.PI * num / 6;

            //Calculating the position of each number based on the calculated radian amount
            Vector3 pos = new Vector3(Mathf.Cos(radian) * 3.3f, Mathf.Sin(radian) * 3.3f, 0f);

            //Dynamically instantiating ClockNumbers with the calculated position
            numbers[num - 1] = Instantiate(clockNumber, pos, Quaternion.identity);

            //Debug.Log("Created the number " + num);
        }

        //Changing the TextMesh of each number to its corresponding number
        for (int num = 0; num < 12; num++)
        {
            TextMesh text = numbers[num].GetComponentInChildren<TextMesh>();
            text.text = (num + 1).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
