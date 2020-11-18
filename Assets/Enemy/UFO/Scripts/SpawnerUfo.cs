using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerUfo : MonoBehaviour
{
    private Camera cam;
    private float timer;
    public float maxTimer;
    public List<GameObject> activeUfo;
    public List<GameObject> disactiveUfo;
    private float spawnPosZ;
    private float spawnPosX;
    public bool active;

    public GameObject deadParticle;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        timer = maxTimer;
    }

    private void Update()
    {
        Vector3 cameraToObject = transform.position - Camera.main.transform.position;
        float distance = -Vector3.Project(cameraToObject, Camera.main.transform.forward).y;

        Vector3 leftBot = cam.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightTop = cam.ViewportToWorldPoint(new Vector3(1, 1, distance));

        float x_left = leftBot.x ;
        float x_right = rightTop.x;
        float z_top = rightTop.z;
        float z_bot = leftBot.z;

        if (active && (activeUfo[0].transform.position.x > x_right + 5 || activeUfo[0].transform.position.x < x_left - 5))
        {
            RemoveActiveUfo();
        }
        



        if (timer == 0)
        {
            spawnPosZ = Random.Range(z_bot * 0.8f, z_top * 0.8f);
            float i = Random.Range(0, 100);
            if (i > 50)
            {
                spawnPosX = x_right + 3;
            }
            else
            {
                spawnPosX = x_left - 3;
            }
            AddActiveUfo();
            timer = maxTimer;
        }

        if (!active)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, maxTimer);
        }
        

        
    }


    public void AddActiveUfo()
    {
        activeUfo.Add(disactiveUfo[0]);
        disactiveUfo[0].transform.position = new Vector3(spawnPosX, 0, spawnPosZ);
        
        disactiveUfo[0].SetActive(true);
        disactiveUfo.RemoveAt(0);
        active = true;

        
    }

    public void RemoveActiveUfo()
    {
        activeUfo[0].SetActive(false);
        GameObject particle =  Instantiate(deadParticle, activeUfo[0].transform.position, transform.rotation);
        Destroy(particle, 1);
        disactiveUfo.Add(activeUfo[0]);
        activeUfo.RemoveAt(0);
        active = false;
    }

}
