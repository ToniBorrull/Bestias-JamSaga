using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public Player player;
    public Boss1 boss1;
    public Boss2 boss2;
    public int defeatedEnemies = 0;
    public bool fightOn = true;
    public bool paused;
    public string scene1;
    public string scene2;
    public string menuScene;
    public GameObject pause;
    public GameObject curtainLeft;
    public GameObject curtainRight;
    public bool open;
    public Light foco;
    public bool done;
    public bool gMDone;
    public bool close;

    public UnityEngine.UI.Button Play;
    public UnityEngine.UI.Button Options;
    public UnityEngine.UI.Button Exit;

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
        fightOn = false;
        paused = false;
        open = false;
        done = false;
        close = false;
    }

    
    void Update()
    {
        
        /*
        if (Input.GetButtonDown("Pause"))
        {
            GetCanvas();
            paused = !paused;
            if (paused)
            {
                if(!pause.activeInHierarchy)
                    pause.SetActive(true);
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
                pause.SetActive(false);
                Time.timeScale = 1;
            }*/


        //Get objects by scene

        if (SceneManager.GetActiveScene().name == menuScene)
        {
            if (gMDone)
            {
                ResetVariables();
                gMDone = false;
            }
        }
        if (SceneManager.GetActiveScene().name == scene1)
        {
            GetBoss1();
            boss2 = null;
            GetPlayer();
            GetLights();
            //Esperar para abrir cortinas
            GetCurtains();
            OpenLights();
            StartCoroutine(curtainsTrigger());
            gMDone = true;
        }
        if(SceneManager.GetActiveScene().name == scene2)
        {
            GetBoss2();
            boss1 = null;
            GetPlayer();
            GetCurtains();
            StartCoroutine(curtainsTrigger());
            gMDone = true;
        }

        if (curtainRight != null && curtainLeft != null)
        {
            OpenCurtains();
            CloseCurtains();
                
        }
        if (boss1 != null)
        {
            if (boss1.health <= 0 )
            {
                close = true;
            }
        }
        if(boss2 != null)
        {
            if(boss2.health <= 0)
            {
                close = true;
            }
        }
    }

    void ResetVariables()
    {
        fightOn = false;
        paused = false;
        open = false;
        done = false;
        close = false; 
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
        pause = GameObject.FindGameObjectWithTag("PauseCanvas");
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
        if (foco.spotAngle <= 60f)
        {
            foco.spotAngle += 1.0f;
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
                if(boss1 != null)
            {
                boss1.isFighting = true;
            }
            if(boss2 != null)
            {
                boss2.isFighting = true;
            }
                open = false;
            }
        }
    }

    void CloseCurtains()
    {
        if (close)
        {
            curtainLeft.transform.localScale += new Vector3(0.3f, 0, 0) * Time.deltaTime;
            curtainRight.transform.localScale += new Vector3(0.3f, 0, 0) * Time.deltaTime;

            if (curtainRight.transform.localScale.x >= 1)
            {
                close = false;
                if(SceneManager.GetActiveScene().name == scene1)
                { 
                    ChangeScene("Ferran");
                    defeatedEnemies++;
                }
                if (SceneManager.GetActiveScene().name == scene2)
                {
                    ChangeScene("WinScreen");
                }
            }
        }
    }

    //SCENE Manager

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
        ResetVariables();
    }
    IEnumerator curtainsTrigger()
    {
        if (!done)
        {
            yield return new WaitForSeconds(2f);
            open = true;
            done = true;
        }
    }
}
