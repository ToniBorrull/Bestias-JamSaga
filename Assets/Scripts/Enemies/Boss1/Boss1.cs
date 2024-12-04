using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss1 : Enemy
{
    public Fireball projectile;
    public Transform AttackSlotHigh;
    public Transform AttackSlotMid;
    public Transform AttackSlotLow;
    private Animator animator;
    private float lastAttack = 0f;
    public float combo1Rate;
    public float combo2Rate;
    public float combo3Rate;
    private float fightTimer;
    private string currentAnimation;
    private string lastAnimation;
    public bool isFighting;
    public float initialAtkRate = 2.0f;
    public float rateDecreaseInterval = 10f;
    public float rateDecreaseAmount = 0.2f;
    float rateTimer;
    public Rigidbody rb;
    public GameObject estrellitas;

    public GameObject[] hearts;
    public AudioClip fireballSound;

    public float chillTime = 3;
    private float chillTimer = 0;

    protected override void Start()
    {
        isFighting = GameManager.instance.fightOn;
        animator = GetComponentInChildren<Animator>();
        atkRate = initialAtkRate;
    }

    protected override void Update()
    {
        //Debug.Log(isFighting);
        //Debug.Log(attacksDone);
        if (isFighting)
        {
            fightTimer += Time.deltaTime;

            rateTimer += Time.deltaTime;
            if (rateTimer >= rateDecreaseInterval)
            {
                rateTimer = 0f;
                //Limite 0.5 segundos
                atkRate = Mathf.Max(0.5f, atkRate - rateDecreaseAmount);
                Debug.Log($"Nueva tasa de ataque: {atkRate}");
            }

            if (Time.time > lastAttack + atkRate)
            {
                ChooseAttack();
            }
        }
        else
        {
            if(chillTimer < chillTime)
            {
                Debug.Log("Chilling");
                chillTimer += Time.deltaTime;
            }
            else
            {
                Debug.Log("He dejado de chillear");
                attacksDone = 0;
                isFighting = true;
                chillTimer = 0;
            }
        }
        estrellitas.SetActive(!isFighting);
    }
    protected override void Die()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 randomTorque = new Vector3(UnityEngine.Random.Range(2, 5), UnityEngine.Random.Range(3, 10), UnityEngine.Random.Range(2, 3));
        rb.AddTorque(randomTorque, ForceMode.Impulse);
        Destroy(gameObject, 1f);
    }
    void ChooseAttack()
    {
        Stunned();
        AttackDone();
        float chosenAttack = Mathf.Floor(UnityEngine.Random.Range(0, 3));
        Debug.Log(attacksDone);
        switch (chosenAttack)
        {
            case 0:
                Combo1(combo1Rate);
            break;
                case 1:
                    Combo2(combo2Rate);
                break;
                    case 2:
                        Combo3(combo3Rate);
                    break;
            default:
            break;
                    
        }
    }

    void Stunned()
    {
        isFighting = attacksDone >= 5 ? false : true;
    }
    void Combo1(float waitTime)
    {
        ResetAttackTime(waitTime);
        PlayAnimation("HighPos");
    }
   

    void Combo2(float waitTime)
    {
        ResetAttackTime(waitTime);
        PlayAnimation("LowPos");
    }
    void Combo3(float waitTime) 
    {
        ResetAttackTime(waitTime);
        PlayAnimation("MidPos");
    }

    public void CreateBullet()
    {
        switch (lastAnimation) {
            case "MidPos":
            for (int i = 30; i > -30; i -= 15)
            {
                AttackSlotMid.rotation = Quaternion.Euler(0, 0, i);
                Instantiate(projectile, AttackSlotMid.position, AttackSlotMid.rotation);
            }
                break;
            case "LowPos":
                Instantiate(projectile, AttackSlotLow.position, transform.rotation);
                break;
            case "HighPos":
                Instantiate(projectile, AttackSlotHigh.position, transform.rotation);
                break;
        }
    }
    void PlayAnimation(string animation)
    {
        if (lastAnimation != animation)
        {
            currentAnimation = animation;
            animator.SetTrigger(animation);
            lastAnimation = animation;
        }
        else
        {
            animator.SetTrigger("Shoot");
            FMODUnity.RuntimeManager.PlayOneShot("event:/Fireball", GetComponent<Transform>().position);
            Debug.Log("HaSonado");
        }
    }

    void ResetAttackTime(float waitTime)
    {
        lastAttack = Time.time + waitTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            Destroy(other.gameObject);
            Debug.Log("Attack");
        }
        
    }
    public void ActivateFight() 
    {
        isFighting = true;
    }
    public void DeactivateFight()
    {
        isFighting = false;
    }
    public void UpdateHearts()
    {
            hearts[(int)health].GetComponent<Rigidbody>().useGravity = true;
            Rigidbody heartsRb = hearts[(int)health].GetComponent<Rigidbody>();
            Vector3 randomTorque = new Vector3(0, UnityEngine.Random.Range(3, 10), 0);
            heartsRb.AddTorque(randomTorque, ForceMode.Impulse);
            Destroy(hearts[(int)health], 2f);
    }


}
