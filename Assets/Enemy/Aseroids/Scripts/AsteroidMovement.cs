using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{

    public enum Type
    {
        Big,
        Middle,
        Small
    }
    public Type type;

    public SpawnAsteroids spawn;

    public Rigidbody rb;
    public float speed;
    public float minSpeed;
    public float maxSpeed;

    public float direction;

    public int score;
    

    


    void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void OnEnable()
    {
        if (type.GetHashCode() == 0)
        {
            direction = Random.Range(0, 360);

        }
        Quaternion newQuaternion = transform.rotation;
        Vector3 vec = new Vector3(transform.rotation.x, direction, transform.rotation.z);
        newQuaternion.eulerAngles = vec;
        transform.rotation = newQuaternion;
        speed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        rb.velocity = transform.forward * speed;
         
    }

   


    private void OnTriggerEnter(Collider other)
    {
        if (type.GetHashCode() == 0)
        {
            if (other.tag == "Player" || other.tag == "Ufo")
            {
                spawn.RemoveActiveBigAsteroid(gameObject);
            }
            else if (other.tag == "Bullet")
            {
                spawn.AddActiveMiddleAsteroid(gameObject);
            }
        }
        else if (type.GetHashCode() == 1)
        {
            if (other.tag == "Player" || other.tag == "Ufo")
            {
                spawn.RemoveActiveMiddleAsteroid(gameObject);
            }
            else if (other.tag == "Bullet" )
            {
                spawn.AddActiveSmallAsteroid(gameObject);
            }
        }
        else if (type.GetHashCode() == 2)
        {
            if (other.tag == "Player" || other.tag == "Ufo")
            {
                spawn.RemoveActiveSmallAsteroid(gameObject);
            }
            else if (other.tag == "Bullet")
            {
                spawn.RemoveActiveSmallAsteroid(gameObject);
            }
        }

    }
}
