using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GetPlayer();
        GetBoss();
    }

    // Update is called once per frame
    void Update()
    {
        FightOn();
        if (Input.GetButtonDown("Pause"))
        {
            if (paused)
            {
                paused = true;
                Time.timeScale = 1.0f;
            }
            else
            {
                paused = false;
                Time.timeScale = 0;
            }
        }
    }
    void GetPlayer()
    {
        player = FindObjectOfType<Player>();
    }
    void GetBoss()
    {
        //boss = FindObjectOfType<Boss1>();
    }
    void FightOn()
    {
        boss.ActivateFight();
    }
    void FightOff()
    {
        boss.DeactivateFight();
    }
}
