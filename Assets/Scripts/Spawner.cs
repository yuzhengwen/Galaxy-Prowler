using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    private float xMin = -8, yMin = -4;
    private float xMax = 8, yMax = 4;

    public static bool gameOver = false;

    //variables relating to asteroid spawning
    [SerializeField]
    private GameObject asteroidPrefab;
    [SerializeField]
    private GameObject asteroidContainer;
    public float asteroidSpawnInterval = 3.0f;
    public int asteroidSpawnRate = 3;
    public int maxAsteroids = 15;
    public int asteroidCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //A coroutine is a function that allows pausing its execution and resuming from the same point after a condition is met
        //eg. wait 5s, then continue from that point
        StartCoroutine(spawnRandomAsteroids(asteroidSpawnInterval, asteroidSpawnRate));
    }

    // Update is called once per frame
    void Update()
    {
    }

    //IEnumerator not IEnumerable!!
    IEnumerator spawnRandomAsteroids(float interval, int rate)
    {
        while (gameOver == false) {
            for (int i= 0; i< rate; i++) {
                if (asteroidCount < maxAsteroids)
                {
                    float x = Random.Range(xMin, xMax);
                    float y = Random.Range(yMin, yMax);
                    float z = 20;
                    Vector3 spawnPosition = new Vector3(x, y, z);
                    GameObject newAsteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
                    newAsteroid.transform.parent = asteroidContainer.transform;
                    asteroidCount++;
                    Debug.Log("Spawned");
                }
            }
            //wait a few seconds, then moves on to next line of code below (repeats while loop)
            yield return new WaitForSeconds(interval);
        }
    }

    public void spawnObject(GameObject obj, Vector3 spawnPosition)
    {

    }

    public void onPlayerDeath()
    {
        gameOver = true;
        destroyAllAsteroids();
    }

    private void destroyAllAsteroids()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject asteroid in asteroids)
            Destroy(asteroid);
    }
}
