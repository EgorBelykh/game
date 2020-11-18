using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public PlayerMovement player;

    private bool control;
    public Text controlButton;

    public GameObject resumeButton;

    public Text score;
    public Text health;
    private bool isPaused;

    private GameManager game;

    public GameObject pause;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        game = FindObjectOfType<GameManager>();
    }

    void Start()
    {

        pause.SetActive(true);

        
        Time.timeScale = 0;
    }

    
    void Update()
    {
        if (player != null)
        {
            score.text = "Score: " + player.score.ToString();
            health.text = "Heath: " + player.health.ToString();
            player.control = control;
            player.GetComponent<PlayerShot>().control = control;
            
        }
        else
        {
            player = FindObjectOfType<PlayerMovement>();
        }

        if (control)
        {
            controlButton.text = "Управление: " + "Клавитура + мышь";
        }
        else
        {
            controlButton.text = "Управление: " + "Клавитура";
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = true;
            Pause();

        }

        if (isPaused)
        {
            resumeButton.SetActive(true);
        }
        else
        {
            resumeButton.SetActive(false);
        }
        
    }

    public void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void NewGame()
    {

        game.StartGame();

        Resume();


    }

    public void SwitchControl()
    {
        control = !control;
        
    }

    public void Pause()
    {
        pause.SetActive(true);
        Time.timeScale = 0;
        
        
    }

    public void Exit()
    {
        Application.Quit();
    }

}
