using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoMovement : MonoBehaviour
{
    private GameObject player;

    private SpawnerUfo spawnerUfo;

    private float speed;
    public float maxSpeed;

    private Rigidbody rb;

    public int score;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        spawnerUfo = FindObjectOfType<SpawnerUfo>();



    }

    private void OnEnable()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        if (transform.position.x > player.transform.position.x)
        {
            speed = -maxSpeed;
        }
        else
        {
            speed = maxSpeed;
        }

    }

    void FixedUpdate()
    {
        if (player != null)
        {
            transform.LookAt(player.transform);
        }
        else
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
        }
        
        rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);

        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Bullet")
        {
            spawnerUfo.RemoveActiveUfo();
        }
        else if (other.tag == "Asteroid")
        {
            spawnerUfo.RemoveActiveUfo();
        }
    }
}
