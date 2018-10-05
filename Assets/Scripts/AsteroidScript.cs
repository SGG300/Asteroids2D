using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour
{

    public Vector3 dest; //The point of destination
    public float radius;// the radius where it can exist
    public int level = 2;
    private Rigidbody2D rBody; //Reference to its rigidbody
    private Vector3 dist; //The distance of origin and destination
    private Vector3 initScale;

    public void Awake()
    {
        initScale = transform.localScale;
    }


    // Use this for initialization
    public void Init()
    {
        //We obtain the distance and normalize it
        dist = dest - transform.position;
        dist.Normalize();
        //reference to its rigidbody
        rBody = GetComponent<Rigidbody2D>();
        switch (level)
        {
            case 2:
                transform.localScale = initScale;
                break;
            case 1:
                transform.localScale = initScale / 2;
                break;
            case 0:
                transform.localScale = initScale / 3;
                break;

        }

    }

    void Update()
    {
        //If the distance from the center is greater than radius, it is destroyed
        Vector3 distFromCenter = transform.position;
        if (distFromCenter.magnitude > radius)
            gameObject.SetActive(false);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //We move the asteroids
        rBody.MovePosition(transform.position + dist * Time.deltaTime);
    }

    public void DestroyAsteroid()
    {
        if (level > 0)
        {
            GameManagerScript.instance.asteroidsGenerator.CreateSubAsteroid(transform.position, level - 1);
            GameManagerScript.instance.asteroidsGenerator.CreateSubAsteroid(transform.position, level - 1);
        }
        gameObject.SetActive(false);
    }

}
