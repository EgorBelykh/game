using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoShot : MonoBehaviour
{
    

   
    public Transform bulletSpawn;
    public float bulletSpeed;
    private float timer;
    public float maxTimer;

    public List<BulletEnemy> activeBullets;
    public List<BulletEnemy> disactiveBullets;

    private AudioSource audioSource;

     

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        //player = GetComponent<UfoMovement>().player;
        timer = maxTimer;
    }


    public void AddActiveBullets()
    {
        BulletEnemy bul = disactiveBullets[0];
        activeBullets.Add(bul);
        bul.speed = bulletSpeed;
        bul.ufoShot = this;
        bul.gameObject.transform.position = bulletSpawn.position;
        bul.gameObject.transform.rotation = bulletSpawn.rotation;
        bul.gameObject.SetActive(true);
        disactiveBullets.Remove(bul);
        audioSource.Play();
    }

    public void RemoveActiveBullets(BulletEnemy bul)
    {
        bul.gameObject.SetActive(false);
        
        disactiveBullets.Add(bul);
        activeBullets.Remove(bul);

    }


    void Update()
    {
        
            if (timer == 0)
            {
                //GameObject obj  = Instantiate(bullet,bulletSpawn.position,bulletSpawn.rotation);
                //obj.GetComponent<Bullet>().speed = bulletSpeed; 
                AddActiveBullets();
                timer = maxTimer;
            }

        
        timer -= Time.deltaTime;
        timer = Mathf.Clamp(timer, 0, maxTimer);

    }
}
