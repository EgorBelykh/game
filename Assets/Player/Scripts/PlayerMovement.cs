using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float minSpeed;
    public float swingSpeed;
    public float acceleration;
    public int score;
    public int health;

    public GameObject ship;
    private Rigidbody rb;

    private float vertical;
    private float horizontal;

    public bool control;

    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        StartCoroutine(GodMod());
    }

    public IEnumerator GodMod()
    {
        
        
        GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        ship.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        ship.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        ship.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        ship.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        ship.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        ship.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        ship.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        ship.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        ship.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        ship.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        ship.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        ship.SetActive(true);
        GetComponent<CapsuleCollider>().enabled = true;
        
    }
    
    void Update()
    {
        Vector3 sp = rb.velocity;
        sp.x = Mathf.Clamp(sp.x, -maxSpeed, maxSpeed);
        sp.z = Mathf.Clamp(sp.z, -maxSpeed, maxSpeed);
        rb.velocity = sp;

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        rb.angularVelocity = transform.up * swingSpeed * horizontal;
        rb.AddForce(transform.forward * acceleration * vertical);

        if (control)
        {

            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Transform tr = transform;
                Vector3 worldPosition = ray.GetPoint(position);
                Vector3 dir = worldPosition - transform.position;

                Quaternion lookDir = Quaternion.LookRotation(dir);
                Quaternion targetRot = Quaternion.Slerp(transform.rotation, lookDir, swingSpeed * Time.deltaTime);
                transform.rotation = targetRot;
                Debug.DrawLine(Camera.main.transform.position, dir);
                if (Input.GetMouseButton(0))
                {
                    rb.AddForce(transform.forward * acceleration);
                }



            }

        }

        health = Mathf.Clamp(health, 0, 3);
      
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(GodMod());
        health--;
    }
}
