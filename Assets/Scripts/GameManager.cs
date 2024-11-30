using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        GetPlayer();
        //GetBoss();
    }

    // Update is called once per frame
    void Update()
    {
        //FightOn();
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
            if (paused)
            {
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
                Time.timeScale = 1;
            }
        }

        if(SceneManager.GetActiveScene().name == scene1)
        {
            GetBoss1();
            boss2 = null;
            GetPlayer();
            boss1.isFighting = true;
        }
        if(SceneManager.GetActiveScene().name == scene2)
        {
            GetBoss1();
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
