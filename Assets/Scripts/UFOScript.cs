using UnityEngine;
using System.Collections;

public class UFOScript : MonoBehaviour {

	public GameObject target; //The target that it follows
	public float speed = 9.0f; //The speed it moves
	private Rigidbody2D rb;//The reference to his rigidBody

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	// Update is called once per frame
	void Update () {
		//It target isn't null it moves towards it, elsewhere it stops.
		if (target != null)
			rb.velocity = (target.transform.position - transform.position).normalized * speed;
		else
			rb.velocity = new Vector2 (0.0f, 0.0f);
	}
}
