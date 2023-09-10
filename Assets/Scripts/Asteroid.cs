using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float velocity = 10;

    private Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        //find the Spawner object, get the spawner script/class
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 towardPlayer = new Vector3(0, 0, -1);
        transform.Translate(towardPlayer * velocity * Time.deltaTime);

        if (transform.position.z < -6)
        {
            int newX = Random.Range(-8, 8);
            int newY = Random.Range(-6, 6);
            transform.position = new Vector3(newX, newY, 20);
        }
    }

    // called when 2 objects enter each other's space, collider is passed as variable
    private void OnTriggerEnter(Collider other)
    {
        string otherName = other.gameObject.tag;
        switch (otherName)
        {
            case "Player":
                asteroidDestroyed();
                //calls the other gameObject, and goes to its root (transform)
                //gets the Player script component from the Collider object
                other.transform.GetComponent<Player>().takeDamage(1);
                break;
            case "BasicBullet":
                Debug.Log("Hit bullet");
                asteroidDestroyed();
                Destroy(other.gameObject);
                break;

        }

    }

    private void asteroidDestroyed()
    {
        Destroy(this.gameObject);
        spawner.asteroidCount--;
        Debug.Log(spawner.asteroidCount + "asteroids left");
    }
}
