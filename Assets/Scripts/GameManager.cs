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
    public string menuScene;

    public Canvas pause;
    public Canvas pressStart;
    public Canvas menu;

    public GameObject curtainLeft;
    public GameObject curtainRight;
    public bool open;
    public Light foco;
    public bool done;
    public bool startDone = false;

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
        open = false;
    }

    
    void Update()
    {
        if (!pressStart)
        {
            if (Input.anyKey)
            {
                pressStart.enabled = false;
                menu.enabled = true;
                startDone = true;
            }
        }
        if (Input.GetButtonDown("Pause"))
        {
            //GetCanvas();
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
        if(SceneManager.GetActiveScene().name == menuScene)
        {
            GetPressStart();
        }
        if(SceneManager.GetActiveScene().name == scene1)
        {
            if (!done)
            {
                GetBoss1();
                boss2 = null;
                GetPlayer();
                GetCurtains();
                GetLights();
                StartCoroutine(curtainsTrigger());
                done = true;
            }
        }
        if(SceneManager.GetActiveScene().name == scene2)
        {
            GetBoss2();
            boss1 = null;
            GetPlayer();
            GetCurtains();
            GetLights();    
            StartCoroutine(curtainsTrigger());
        }

        if(curtainRight != null && curtainLeft != null)
        {
            OpenCurtains();
            OpenLights();
        }
        
    }
    public Player GetPlayer()
    {
        player = FindObjectOfType<Player>();
        if(player != null)
            return player;

        return null;
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
    void GetPressStart()
    {
        pressStart = GameObject.FindGameObjectWithTag("StartCanvas").GetComponent<Canvas>();
        menu = GameObject.FindGameObjectWithTag("MenuCanvas").GetComponent<Canvas>();
        menu.enabled = false;
    }

    void GetCurtains()
    {
        curtainLeft = GameObject.FindGameObjectWithTag("LeftCurtain");
        curtainRight = GameObject.FindGameObjectWithTag("RightCurtain");
    }
    void GetLights()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Foco");
        foco = obj.GetComponent<Light>();

    }
    void OpenLights()
    {
        if (foco != null)
        {
            if (foco.spotAngle < 60f)
            {
                foco.GetComponent<Light>().spotAngle += 1.0f;
            }
        }
    }

    void OpenCurtains()
    {
        if (open)
        {
            curtainLeft.transform.localScale -= new Vector3(0.3f, 0, 0) * Time.deltaTime;
            curtainRight.transform.localScale -= new Vector3(0.3f, 0, 0) * Time.deltaTime;
            
            if(curtainRight.transform.localScale.x <= 0)
            {
                open = false;
                if (boss1 != null)
                {
                    boss1.isFighting = true;
                }
                if(boss2 != null)
                {
                    boss2.isFighting = true;
                }
                
            }
        }
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
    IEnumerator curtainsTrigger()
    {
        yield return new WaitForSeconds(2f);
        open = true;
    }
}
