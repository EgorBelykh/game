using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SpawnAsteroids spawnAsteroids;
    private SpawnerUfo spawnerUfo;
    private UIManager uIManager;
    private AsteroidMovement[] enemies;
    private int count = 2;
    private bool isWave;

    public GameObject player;

    private void Awake()
    {
        spawnAsteroids = GetComponent<SpawnAsteroids>();
        spawnerUfo = GetComponent<SpawnerUfo>();
        uIManager = FindObjectOfType<UIManager>();
    }

    private void FixedUpdate()
    {
        enemies = FindObjectsOfType<AsteroidMovement>();

        if (enemies.Length == 0 && !isWave)
        {


            StartCoroutine(Spawn());
            
            isWave = true;
        }
        else if (enemies.Length != 0)
        {
            isWave = false;
        }
        
        if (FindObjectOfType<PlayerMovement>() != null && FindObjectOfType<PlayerMovement>().health == 0)
        {
            DestroyAll();
            Invoke("Pause", 1);
            
        }

    }

    public void Pause()
    {
        uIManager.Pause();
        
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < count; i++)
        {

           spawnAsteroids.AddActiveBigAsteroid();
        }
        count++;

    }

    public void StartGame()
    {
        count = 2;
        DestroyAll();
        Instantiate(player, transform.position, transform.rotation);
        
            
        
        
    }

    public void DestroyAll()
    {
        if (enemies.Length != 0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                Debug.Log(i);
                if (enemies[i].type.GetHashCode() == 0)
                {
                    spawnAsteroids.RemoveActiveBigAsteroid(enemies[i].gameObject);
                }
                else if (enemies[i].type.GetHashCode() == 1)
                {
                    spawnAsteroids.RemoveActiveMiddleAsteroid(enemies[i].gameObject);
                }
                else if (enemies[i].type.GetHashCode() == 2)
                {
                    spawnAsteroids.RemoveActiveSmallAsteroid(enemies[i].gameObject);
                }

            }
        }

        if (spawnerUfo.active)
        {
            spawnerUfo.RemoveActiveUfo();
        }
        if (FindObjectOfType<PlayerMovement>() != null)
        {
            Destroy(FindObjectOfType<PlayerMovement>().gameObject.transform.parent.gameObject);
        }
    }

}
