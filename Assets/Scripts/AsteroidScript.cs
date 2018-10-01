using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {

	private Rigidbody2D rBody; //Reference to its rigidbody
	public Vector3 dest; //The point of destination
	private Vector3 dist; //The distance of origin and destination
	public float radius;// the radius where it can exist

	// Use this for initialization
	void Start () {
		//We obtain the distance and normalize it
		dist = dest - transform.position;
		dist.Normalize ();
		//reference to its rigidbody
		rBody = GetComponent<Rigidbody2D>();
	}

	void Update (){
		//If the distance from the center is greater than radius, it is destroyed
		Vector3 distFromCenter = transform.position;
        if (distFromCenter.magnitude > radius)
            gameObject.SetActive(false);
	}
	// Update is called once per frame
	void FixedUpdate () {
		//We move the asteroids
		rBody.MovePosition (transform.position + dist * Time.deltaTime);
	}


}
