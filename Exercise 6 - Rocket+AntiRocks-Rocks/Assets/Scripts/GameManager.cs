using UnityEngine;
using UnityEngine.UI; // Note this new line is needed for UI
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public Text scoreText;
	public Text gameOverText;
    CollisionDetector collisionDetector;
    public List<GameObject> rocks;
    public List<GameObject> antirocks;
    public GameObject rocket;
    GameObject antirock;
    GameObject rock;

	int playerScore = 0;

    bool rockOut;

    //Bool to determine which collision method to use
    bool circleCollision;

    public void Start()
    {
        collisionDetector = gameObject.GetComponent<CollisionDetector>();
        rocks = new List<GameObject>();
        antirocks = new List<GameObject>();
        circleCollision = false;
    }


    public void Update()
    {
        //Using the Alpha0 key to toggle collision methods
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            SwitchCollision();
        }

        rockOut = false;

        //check to see whether any rock has rolled onto the rocket
        foreach (GameObject rck in rocks)
        {
            if (collisionDetector.AABBTest(rocket, rck) && !circleCollision)
            {
                PlayerDied();
            }

            //for Exercise 6, you will implement the option to select the use of this
            if (collisionDetector.BoundingCircleTest(rocket, rck) && circleCollision)
            {
                PlayerDied();
            }
        }

        foreach (GameObject antirck in antirocks )
        {
            foreach (GameObject rck in rocks)
            {
                if (collisionDetector.AABBTest(antirck, rck) && !circleCollision)
                {
                    //Debug.Log("antirock hit rock!");
                    rockOut = true;
                    antirock = antirck;
                    rock = rck;
                    break;
                }

                //for Exercise 6, you will implement the option to select the use of this
                if (collisionDetector.BoundingCircleTest(antirck, rck) && circleCollision)
                {
                    //Debug.Log("antirock hit rock!");
                    rockOut = true;
                    antirock = antirck;
                    rock = rck;
                    break;
                }

                //Detecting if a rock is out of range
                if (rck.transform.position.y < -6.25f)
                {
                    Debug.Log("Rock detected out of range");
                    rockOut = true;
                    rock = rck;
                }
            }

            //Detecting if an antirock is out of range
            if (antirck.transform.position.y > 6.25f)
            {
                Debug.Log("Antirock detected out of range");
                rockOut = true;
                antirock = antirck;
            }

            if (rockOut)
                break;
        }

        if (rockOut)
        {
            RemoveRockFromList(rock);
            RemoveAntiRockFromList(antirock);
            Destroy(rock);
            Destroy(antirock);
            AddScore();
        }

    }

    public void AddRockToList(GameObject rock)
    {
        rocks.Add(rock);
    }

    public bool RemoveRockFromList(GameObject rock)
    {
        return rocks.Remove(rock);
    }

    public void AddAntiRockToList(GameObject antirock)
    {
        antirocks.Add(antirock);
    }

    public bool RemoveAntiRockFromList(GameObject antirock)
    {
        return antirocks.Remove(antirock);
    }


    public void AddScore()
	{
		playerScore++;
		//This converts the score (a number) into a string
		scoreText.text = playerScore.ToString();
	}

	public void PlayerDied()
	{
		gameOverText.enabled = true;

		// This freezes the game
		Time.timeScale = 0;				
	}

    //SwitchCOllision method switches between the two collision methods
    public void SwitchCollision()
    {
        if(circleCollision)
        {
            circleCollision = false;
            Debug.Log("Switching to box collision");
        }
        else
        {
            circleCollision = true;
            Debug.Log("Switching to circle collision");
        }
    }
}
