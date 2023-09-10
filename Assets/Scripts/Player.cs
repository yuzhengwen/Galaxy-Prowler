using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variables for controlling palyer mvmt
    [SerializeField]
    public float xVelocity = 10, yVelocity = 10;
    private float xInput, yInput;
    private Vector3 direction;
    private float xMax = 8, xMin= -8, yMax = 4, yMin = -3;

    //prefab for bullets
    [SerializeField]
    private GameObject _basicBulletPrefab;

    //variables for controlling fire rate
    [SerializeField]
    private float fireInterval = 0.2f;
    private float nextFire = 0;

    public int life = 3;

    private Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        //set initial position
        transform.position = new Vector3(0, -2, -3);
        //find the Spawner object, get the spawner script/class
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        //Time.time counts the seconds the moment the program runs
        if (Input.GetKey(KeyCode.Space) && Time.time>=nextFire)
            Shoot();
    }

    void CalculateMovement()
    {

        //returns value range -1 to 1
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        direction = new Vector3(xInput, yInput, 0);

        // deltaTime = no. of seconds per frame
        transform.Translate(direction * xVelocity * Time.deltaTime);

        //Clamp keeps value within limits
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, yMin, yMax));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y );

    }

    void Shoot()
    {
        nextFire = Time.time + fireInterval;
        Vector3 spawnPosition = transform.position + new Vector3(0, 0, 1);
        Instantiate(_basicBulletPrefab, spawnPosition, Quaternion.identity); 
    }

    public void takeDamage(int damage)
    {
        life -= damage;
        Debug.Log(life + " lives left");
        if (life == 0)
        {
            gameLost();
        }
    }

    void gameLost()
    {
        Debug.Log("Lost");
        spawner.onPlayerDeath();
        Destroy(this.gameObject);
    }
}
