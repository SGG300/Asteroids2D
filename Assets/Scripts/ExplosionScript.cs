using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//When it awakes, it plays the partycleSystem and it is destroyed at the end of the animation
		ParticleSystem exp = GetComponent<ParticleSystem>();
		exp.Play();
		Destroy(gameObject, exp.duration);
	}

}
