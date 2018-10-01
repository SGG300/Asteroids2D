using UnityEngine;
using System.Collections.Generic;

public class UFOGenerator : MonoBehaviour
{

    public GameObject ufoPrefab; //The prefab we invoke
    public float radius; //The radius of the circle where we invoke the enemy
    public float cooldown;//The time it last to invoke another
    private float counterTime;//The counter untils the cooldown finishes
    private GameObject target;//The target we follow AKA: The spaceship

    private List<GameObject> ufos = new List<GameObject>();

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("SpaceShip"); //We Search the spaceship and reference it
        CreateUfo();
    }

    // Update is called once per frame
    public void CreateUfo()
    {
        float angle = Random.Range(0.0f, 360.0f);
        float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector2 newPos = new Vector2(transform.position.x + x, transform.position.y + y);
        bool usedPool = false;
        foreach (GameObject ufo in ufos)
        {
            if (ufo.activeSelf == false)
            {
                ufo.transform.position = newPos;
                ufo.SetActive(true);
                usedPool = true;
                break;
            }
        }
        if (!usedPool)
        {
            GameObject ufoAux = Instantiate(ufoPrefab, newPos, transform.rotation, this.transform) as GameObject;
            ufoAux.GetComponent<UFOScript>().target = target;
            ufos.Add(ufoAux);
        }

        
        Invoke("CreateUfo", cooldown);
    }
}
