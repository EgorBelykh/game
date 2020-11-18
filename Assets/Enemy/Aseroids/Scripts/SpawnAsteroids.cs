using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    private Camera cam;

    
    
    public GameObject deadParticle;

    public List<GameObject> activeBigAsteroids;
    public List<GameObject> disactiveBigAsteroids;

    public List<GameObject> activeMiddleAsteroids;
    public List<GameObject> disactiveMiddleAsteroids;

    public List<GameObject> activeSmallAsteroids;
    public List<GameObject> disactiveSmallAsteroids;

    private float x_left;
    private float x_right;
    private float z_top;
    private float z_bot;

    

    public void Awake()
    {
        cam = Camera.main;
    }
    public void AddActiveBigAsteroid()
    {
        activeBigAsteroids.Add(disactiveBigAsteroids[0]);
        disactiveBigAsteroids[0].transform.position = RandomPosition();
        disactiveBigAsteroids[0].GetComponent<AsteroidMovement>().spawn = this;
        disactiveBigAsteroids[0].SetActive(true);
        disactiveBigAsteroids.RemoveAt(0);
    }

    public void RemoveActiveBigAsteroid(GameObject obj)
    {
        obj.SetActive(false);
        GameObject particle = Instantiate(deadParticle, obj.transform.position, RandomRotetion()    );
        Destroy(particle, 1);
        disactiveBigAsteroids.Add(obj);
        activeBigAsteroids.Remove(obj);

    }

    public void AddActiveMiddleAsteroid(GameObject obj)
    {
        RemoveActiveBigAsteroid(obj);
        activeMiddleAsteroids.Add(disactiveMiddleAsteroids[0]);
        disactiveMiddleAsteroids[0].transform.position = obj.transform.position;
        AsteroidMovement move = disactiveMiddleAsteroids[0].GetComponent<AsteroidMovement>();
        move.spawn = this;
        move.direction = obj.GetComponent<AsteroidMovement>().direction + 45;
        disactiveMiddleAsteroids[0].SetActive(true);
        disactiveMiddleAsteroids.RemoveAt(0);

        activeMiddleAsteroids.Add(disactiveMiddleAsteroids[0]);
        disactiveMiddleAsteroids[0].transform.position = obj.transform.position;
        move = disactiveMiddleAsteroids[0].GetComponent<AsteroidMovement>();
        move.spawn = this;
        move.direction = obj.GetComponent<AsteroidMovement>().direction - 45;
        disactiveMiddleAsteroids[0].SetActive(true);
        disactiveMiddleAsteroids.RemoveAt(0);
    }

    public void RemoveActiveMiddleAsteroid(GameObject obj)
    {

        obj.SetActive(false);
        GameObject particle = Instantiate(deadParticle, obj.transform.position, RandomRotetion());
        Destroy(particle, 1);
        disactiveMiddleAsteroids.Add(obj);
        activeMiddleAsteroids.Remove(obj);
    }

    public void AddActiveSmallAsteroid(GameObject obj)
    {
        RemoveActiveMiddleAsteroid(obj);
        activeSmallAsteroids.Add(disactiveSmallAsteroids[0]);
        disactiveSmallAsteroids[0].transform.position = obj.transform.position;
        AsteroidMovement move = disactiveSmallAsteroids[0].GetComponent<AsteroidMovement>();
        move.spawn = this;
        move.direction = obj.GetComponent<AsteroidMovement>().direction + 45;
        disactiveSmallAsteroids[0].SetActive(true);
        disactiveSmallAsteroids.RemoveAt(0);

        activeSmallAsteroids.Add(disactiveSmallAsteroids[0]);
        disactiveSmallAsteroids[0].transform.position = obj.transform.position;
        move = disactiveSmallAsteroids[0].GetComponent<AsteroidMovement>();
        move.spawn = this;
        move.direction = obj.GetComponent<AsteroidMovement>().direction - 45;
        disactiveSmallAsteroids[0].SetActive(true);
        disactiveSmallAsteroids.RemoveAt(0);
    }

    public void RemoveActiveSmallAsteroid(GameObject obj)
    {
        obj.SetActive(false);
        GameObject particle = Instantiate(deadParticle, obj.transform.position, RandomRotetion());
        Destroy(particle, 1);
        disactiveSmallAsteroids.Add(obj);
        activeSmallAsteroids.Remove(obj);
    }

    private void Update()
    {
        Vector3 cameraToObject = transform.position - Camera.main.transform.position;
        float distance = -Vector3.Project(cameraToObject, Camera.main.transform.forward).y;

        Vector3 leftBot = cam.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightTop = cam.ViewportToWorldPoint(new Vector3(1, 1, distance));

        x_left = leftBot.x;
        x_right = rightTop.x;
        z_top = rightTop.z;
        z_bot = leftBot.z;
        
        
        
       
    }

    

    public Vector3 RandomPosition()
    {
        Vector3 pos;
        pos = new Vector3(Random.Range(x_left * 0.9f, x_right * 0.9f), 0, Random.Range(z_bot * 0.9f, z_top * 0.9f));
         return pos;
    }

    public Quaternion RandomRotetion()
    {
        Quaternion quat = new Quaternion();
        quat.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        return quat;
    }
}
