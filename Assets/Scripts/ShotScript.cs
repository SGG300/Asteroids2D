using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour
{

    public float speed; //The speed at the shot moves
    public Rigidbody2D rBody;//The rigidbody of the shot
    public GameObject explosion;//The prefab of the particles

    public float durationTime = 10.0f;//How much it lasts before it is destroyed
    private float actualTime = 0.0f;//The actual time
                                    // Use this for initialization
    void Start()
    {
        
    }

    public void OnEnable()
    {
        actualTime = 0.0f;
        rBody.velocity = transform.up * speed;
    }

    // We destroy the shot when the counter time surpasses the duration time
    void Update()
    {
        actualTime += Time.deltaTime;
        if (actualTime > durationTime)
            gameObject.SetActive(false);
    }

    //When it collides with an asteroid or a ufo it destroys its and adds score
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Asteroid")
        {
            GameManagerScript.instance.AddScore(1);
            Instantiate(explosion, transform.position, transform.rotation);
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        else if (other.tag == "UFO")
        {
            GameManagerScript.instance.AddScore(5);
            Instantiate(explosion, transform.position, transform.rotation);
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }


    }
}
