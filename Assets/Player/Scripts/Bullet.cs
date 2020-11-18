
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{

    public PlayerMovement player;

    public PlayerShot playerShot;

    
    public float speed;
    private Rigidbody rb;
    private Camera cam;
    void Awake()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
        RemoveBullet();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            playerShot.RemoveActiveBullets(this);

            player.score += other.GetComponent<AsteroidMovement>().score;
        }
        else if (other.tag == "Ufo")
        {
            playerShot.RemoveActiveBullets(this);

            player.score += other.GetComponent<UfoMovement>().score;
        }
        
    }

    public void RemoveBullet()
    {
        Vector3 cameraToObject = transform.position - Camera.main.transform.position;
        float distance = -Vector3.Project(cameraToObject, Camera.main.transform.forward).y;

        Vector3 leftBot = cam.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightTop = cam.ViewportToWorldPoint(new Vector3(1, 1, distance));

        float x_left = leftBot.x - 2;
        float x_right = rightTop.x + 2;
        float z_top = rightTop.z + 2;
        float z_bot = leftBot.z - 2;

        

        if (transform.position.x < x_left || transform.position.x > x_right || transform.position.z < z_bot - 1 || transform.position.z > z_top + 1)
        {
            playerShot.RemoveActiveBullets(this);
        }
        
    }
    
    
}
