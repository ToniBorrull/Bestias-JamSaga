using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public Player player;
    public Boss1 boss1;
    public Boss2 boss2;
    public int defeatedEnemies = 0;
    public bool fightOn;
    public bool paused;
    public string scene1;
    public string scene2;
    public Canvas pause;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
  
    void Start()
    {
        paused = false;
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            GetCanvas();
            paused = !paused;
            if (paused)
            {
                pause.gameObject.SetActive(true);
                Time.timeScale = 0;
                if(boss1 != null)
                {
                    boss1.isFighting = false;
                }
                if(boss2 != null)
                {
                    boss2.isFighting = false;
                }
            }else
            {
                pause.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }

        //Get objects by scene
        if(SceneManager.GetActiveScene().name == scene1)
        {
            GetBoss1();
            boss2 = null;
            GetPlayer();
            boss1.isFighting = true;
        }
        if(SceneManager.GetActiveScene().name == scene2)
        {
            GetBoss2();
            boss1 = null;
            GetPlayer();
            boss2.isFighting = true;
        }
    }
    void GetPlayer()
    {
        player = FindObjectOfType<Player>();
    }
    void GetBoss1()
    {
        boss1 = FindObjectOfType<Boss1>();
    }
    void GetBoss2()
    {
        boss2 = FindObjectOfType<Boss2>();
    }
    void FightOn1()
    {
        boss1.ActivateFight();
    }
    void FightOff1()
    {
        boss1.DeactivateFight();
    }
    void GetCanvas()
    {
        pause = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<Canvas>();
    }



    //SCENE Manager
    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
