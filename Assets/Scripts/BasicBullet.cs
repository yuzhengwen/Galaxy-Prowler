using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{

    [SerializeField]
    private float velocity = 20;

    // Start is called before the first frame update
    void Start()
    {
        //set initial rotation
        transform.eulerAngles = new Vector3(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //why is it y translation to move in z axis??
        Vector3 forward = new Vector3(0, 1, 0);
        transform.Translate(forward * velocity * Time.deltaTime);

        //destroy when bullet goes far away
        if (transform.position.z > 30)
        {
            Destroy(this.gameObject);
        }
    }
}
