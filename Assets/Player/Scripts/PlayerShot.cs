using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    
    public Transform bulletSpawn;
    public float bulletSpeed;
    private float timer;
    public float maxTimer;

    public List<Bullet> activeBullets;
    public List<Bullet> disactiveBullets;

    public AudioClip shot;
    private AudioSource audioSource;

    
    public bool control;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void AddActiveBullets()
    {
        Bullet bul = disactiveBullets[0];
        activeBullets.Add(bul);
        bul.speed = bulletSpeed;
        bul.player = GetComponent<PlayerMovement>();
        bul.playerShot = this;
        bul.gameObject.transform.position = bulletSpawn.position;
        bul.gameObject.transform.rotation = bulletSpawn.rotation;
        bul.gameObject.SetActive(true);
        disactiveBullets.Remove(bul);
        audioSource.clip = shot;
        audioSource.Play();
    }

    public void RemoveActiveBullets(Bullet bul)
    {
        bul.gameObject.SetActive(false);
        disactiveBullets.Add(bul);
        activeBullets.Remove(bul);
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (timer == 0)
            {

                AddActiveBullets();
                timer = maxTimer;
            }

        }

        if (control)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (timer == 0)
                {

                    AddActiveBullets();
                    timer = maxTimer;
                }
            }
        }
       

        
        timer -= Time.deltaTime;
        timer = Mathf.Clamp(timer, 0, maxTimer);
        
    }
}
