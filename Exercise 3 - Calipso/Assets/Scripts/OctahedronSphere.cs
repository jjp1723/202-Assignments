	using UnityEngine;

	[RequireComponent(typeof(MeshFilter))]
	public class OctahedronSphere : MonoBehaviour {

		public int subdivisions;
		
		public float radius;

        private float tT;

        public float timeScale; //exercise 3 requires that this quantity be controlled -/+ by left/right arrow keys (handled in GameManager script) 

    private void Awake () {
			GetComponent<MeshFilter>().mesh = OctahedronSphereCreator.Create(subdivisions, radius);
		}

    private void Start()
    {
        timeScale = 1/16f;

        radius = 1f;  //note that this gets scaled by a factor of 50 in the Inspector

        subdivisions = 4;

    }

    void Update()
    {
        tT += Time.deltaTime * timeScale;
        transform.rotation = Quaternion.Euler(0, -(tT * Mathf.Rad2Deg), 0);  //one complete rotation every 2*Pi/timeScale seconds
        //the angle here is negative, because we want the earth spinning CCW, when viewed from above the north pole looking down
        //IMPORTANT:  Unity uses a "CW positive" convention to go along with the left-handed coordinate system.
    }
}