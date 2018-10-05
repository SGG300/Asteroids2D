using UnityEngine;
using System.Collections.Generic;
using System;

public class AsteroidsGenerator : MonoBehaviour
{
    public GameObject asteroid; //The prefab we invoke
    public float radius; //The radius where we invoke
    public float AsteroidsCooldown; //The cooldown from invoke to invoke

    private List<GameObject> asteroids = new List<GameObject>();
    private readonly int maxLevel = 2;

    private void OnEnable()
    {
        CreateAsteroid();
    }

    public void CreateAsteroid()
    {
        //We calculate with an random number, where it is invoked and where it goes in the surface of the circle
        float angle = UnityEngine.Random.Range(0.0f, 360.0f);
        float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector2 newPos = new Vector2(transform.position.x + x, transform.position.y + y);
        angle = UnityEngine.Random.Range(0.0f, 360.0f);
        x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector3 posDest = new Vector3(transform.position.x + x, transform.position.y + y, 0.0f);
        InstantiateAsteroid(newPos, posDest, maxLevel);

        Invoke("CreateAsteroid", AsteroidsCooldown);
    }

    public void CreateSubAsteroid(Vector3 pos, int level)
    {
        float angle = UnityEngine.Random.Range(0.0f, 360.0f);
        float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        angle = UnityEngine.Random.Range(0.0f, 360.0f);
        x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector3 posDest = new Vector3(transform.position.x + x, transform.position.y + y, 0.0f);
        InstantiateAsteroid(pos, posDest, level);
    }

    private void InstantiateAsteroid(Vector3 newPos, Vector3 posDest, int level)
    {
        bool usedPool = false;
        foreach (GameObject asteroid in asteroids)
        {
            if (asteroid.activeSelf == false)
            {
                asteroid.transform.position = newPos;
                asteroid.GetComponent<AsteroidScript>().dest = posDest;
                asteroid.GetComponent<AsteroidScript>().radius = radius;
                asteroid.GetComponent<AsteroidScript>().level = level;
                asteroid.SetActive(true);
                asteroid.GetComponent<AsteroidScript>().Init();
                usedPool = true;
                break;
            }
        }
        if (!usedPool)
        {
            GameObject asteroid = Instantiate(this.asteroid, newPos, transform.rotation, this.transform) as GameObject;
            asteroids.Add(asteroid);
            asteroid.GetComponent<AsteroidScript>().dest = posDest;
            asteroid.GetComponent<AsteroidScript>().radius = radius;
            asteroid.GetComponent<AsteroidScript>().level = level;
            asteroid.GetComponent<AsteroidScript>().Init();
        }
    }
}
