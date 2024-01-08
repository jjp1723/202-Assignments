using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Collisions.
/// Component of the Game Manager game object.
/// Determines collisions between 2 game objects.
/// </summary>
public class CollisionDetector: MonoBehaviour 
{

    //For Exercise 6, rewrite this method to one that is its "logical equivalent", which uses the four tests shown in the PPT slides 
    //of the form: if(  Min < Max  ) that must each be true whenever two AABB's are colliding

    public bool AABBTest(GameObject a, GameObject b)
	{

        // get horizontal extents of sprite A
        float aMinX = a.transform.position.x + a.GetComponent<SpriteInfo>().lowLeft.x;
        float aMaxX = a.transform.position.x + a.GetComponent<SpriteInfo>().upRight.x;
       
        // get vertical extents of sprite A
        float aMinY = a.transform.position.y + a.GetComponent<SpriteInfo>().lowLeft.y;
        float aMaxY = a.transform.position.y + a.GetComponent<SpriteInfo>().upRight.y;

        // get horizontal extents of sprite B
        float bMinX = b.transform.position.x + b.GetComponent<SpriteInfo>().lowLeft.x;
        float bMaxX = b.transform.position.x + b.GetComponent<SpriteInfo>().upRight.x;

        // get vertical extents of sprite B
        float bMinY = b.transform.position.y + b.GetComponent<SpriteInfo>().lowLeft.y;
        float bMaxY = b.transform.position.y + b.GetComponent<SpriteInfo>().upRight.y;

        // Check for a collision using the concept of a separating plane, which if it exists between sprite A and sprite B, means they are not colliding
        if (aMaxX > bMinX && bMaxX > aMinX && aMaxY > bMinY && bMaxY > aMinY) //After negating the conditions, they all must be correct to return true
            return true;
        return false; //If they aren't all true, false is returned

	}
      
	public bool BoundingCircleTest(GameObject a, GameObject b)
	{
        //test whether the the square of (the distance between two circle centers) is greater than the square of (the sum of the radii of the two circles)
        float distance = Mathf.Pow((a.transform.position.x - b.transform.position.x), 2f) + Mathf.Pow((a.transform.position.y - b.transform.position.y), 2f);

        float sum =Mathf.Pow((a.GetComponent<SpriteInfo>().radius + b.GetComponent<SpriteInfo>().radius), 2f);

        if(distance > sum)
        {
            return false;
        }

        return true;

       //NOTE: 
        
       //the above test is more efficient than testing whether (the distance between two circle centers) is greater than (the sum of the radii of the two circles)
       //because of the square root calculation in computing distance.  
       //the sqrMagnitude property of Vector3 can be used to compute the square of the length of a vector (the vector can represent the directed distance between two points), 
       //instead of inefficiently computing the square of a square root  
      
    }
}