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
    public GameObject pause;
    public GameObject curtainLeft;
    public GameObject curtainRight;
    public bool open;
    public Light foco;
    public bool done;

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
        done = false;
    }

    
    void Update()
    {
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
            if(SceneManager.GetActiveScene().name == scene1)
            {
                GetBoss1();
                boss2 = null;
                GetPlayer();
                GetLights();
                //Esperar para abrir cortinas
                GetCurtains();
                OpenLights();
                StartCoroutine(curtainsTrigger());
            }
            if(SceneManager.GetActiveScene().name == scene2)
            {
                GetBoss2();
                boss1 = null;
                GetPlayer();
                GetCurtains();
                StartCoroutine(curtainsTrigger());
            }

            if(curtainRight != null && curtainLeft != null)
            {
                OpenCurtains();
            }
        
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
        if (!done)
        {
            yield return new WaitForSeconds(2f);
            open = true;
            done = true;
        }
    }
}
