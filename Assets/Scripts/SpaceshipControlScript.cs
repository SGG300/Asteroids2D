using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class SpaceshipControlScript : MonoBehaviour
{

    private Rigidbody2D rBody; //reference to its rigibody
    public Boundary boundary;//The space it can travel before reaching the end of the screen
    private bool shooting = false;//Boolean to trigger shooting
    public GameObject explosion;//prefab ot the partycles of explosion
    public GameObject shotPrefab;//The projectiles it shots
    public Transform shotsParent;
    public float shotCooldown;//The cooldown between shots
    public float speedAngular;//the speed it rotates
    public float speedForward;//the speed it moves forwards and backwards
    public int modeShot = 1;//How it shots
    public AudioSource crash;//The sound it makes when it crash

    private List<GameObject> shots = new List<GameObject>();
    private Animator anim;//its animation
    private float counterTimeShot;//A countertime

    //We initializs
    void Start()
    {
        counterTimeShot = 0.0f;
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        counterTimeShot -= Time.deltaTime;
        //Everytime we press space, is time to shot (Except when the cooldown is still on)
        if (Input.GetKeyDown("space"))
            shooting = true;
        if (shooting == true && counterTimeShot < 0.0f)
        {
            switch (modeShot)
            {
                case 1:
                    //It fires one single shot
                    CreateShot(this.transform.rotation);
                    counterTimeShot = shotCooldown;
                    break;
                case 2:
                    //Two shots fired in angle
                    Quaternion rot1 = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + 0.2f, transform.rotation.w);
                    Quaternion rot2 = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z - 0.2f, transform.rotation.w);
                    CreateShot(rot1);
                    CreateShot(rot2);
                    counterTimeShot = shotCooldown;
                    break;
                case 3:
                    //A combination of the previous two modes
                    CreateShot(this.transform.rotation);
                    Quaternion rot3 = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + 0.2f, transform.rotation.w);
                    Quaternion rot4 = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z - 0.2f, transform.rotation.w);
                    CreateShot(rot3);
                    CreateShot(rot4);
                    counterTimeShot = shotCooldown;
                    break;
                default:
                    break;
            }
        }
        shooting = false;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rBody.angularVelocity = -moveHorizontal * speedAngular;
        rBody.velocity = transform.up * moveVertical * speedForward;

        //We check if position is in boundaries and teleport if goes beyond it
        float auxPos;
        if (rBody.position.x < boundary.xMin)
        {
            auxPos = boundary.xMax - (boundary.xMin - rBody.position.x);
            rBody.position = new Vector2(auxPos, rBody.position.y);
        }

        if (rBody.position.x > boundary.xMax)
        {
            auxPos = boundary.xMin + (rBody.position.x - boundary.xMax);
            rBody.position = new Vector2(auxPos, rBody.position.y);
        }

        if (rBody.position.y < boundary.yMin)
        {
            auxPos = boundary.yMax - (boundary.yMin - rBody.position.y);
            rBody.position = new Vector2(rBody.position.x, auxPos);
        }

        if (rBody.position.y > boundary.yMax)
        {
            auxPos = boundary.yMin + (rBody.position.y - boundary.yMax);
            rBody.position = new Vector2(rBody.position.x, auxPos);
        }

    }

    public void CreateShot(Quaternion rot)
    {
        bool usedPool = false;
        foreach(GameObject shot in shots)
        {
            if(shot.activeSelf == false)
            {
                
                shot.transform.position = this.transform.position;
                shot.transform.rotation = rot;
                shot.SetActive(true);
                usedPool = true;
                break;
            }
        }

        if(!usedPool)
        {
            GameObject shot = Instantiate(shotPrefab, transform.position, rot, shotsParent);
            shots.Add(shot);
        }
    }

    //When we crash with a enemy, it is damaged
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Asteroid" || other.tag == "UFO")
        {
            anim.SetTrigger("Damaged");
            crash.Play();
            GameManagerScript.instance.MinusLife(1);
            Instantiate(explosion, other.gameObject.transform.position, other.gameObject.transform.rotation);
            other.gameObject.SetActive(false);
        }
    }
    
}
