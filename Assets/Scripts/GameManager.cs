using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public Player player;
    public Boss1 boss;
    public int defeatedEnemies = 0;
    public bool fightOn;
    public bool paused;

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
        //GetPlayer();
        //GetBoss();
    }

    // Update is called once per frame
    void Update()
    {
        //FightOn();
        if (Input.GetButtonDown("Pause"))
        {

            paused = !paused;
            if(paused)
                Time.timeScale = 0;
            else
            {
                Time.timeScale = 1;
            }
        }
    }
    void GetPlayer()
    {
        player = FindObjectOfType<Player>();
    }
    void GetBoss()
    {
        boss = FindObjectOfType<Boss1>();
    }
    void FightOn()
    {
        boss.ActivateFight();
    }
    void FightOff()
    {
        boss.DeactivateFight();
    }
    public void OnSceneChange()
    {
        GetPlayer();
        GetBoss();
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
