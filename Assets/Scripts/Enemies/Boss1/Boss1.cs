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
    public bool isFighting;
    public float initialAtkRate = 2.0f;
    public float rateDecreaseInterval = 10f;
    public float rateDecreaseAmount = 0.2f;
    float rateTimer;
    protected override void Start()
    {
        isFighting = GameManager.instance.fightOn;
        animator = GetComponentInChildren<Animator>();
        atkRate = initialAtkRate;
    }

    protected override void Update()
    {
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
    }
    protected override void Die()
    {
    }
    void ChooseAttack()
    {
        float chosenAttack = Mathf.Floor(UnityEngine.Random.Range(0, 3));
       
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
    void Combo1(float waitTime)
    {
        ResetAttackTime(waitTime);
        PlayAnimation("HighPos");
        Instantiate(projectile, AttackSlotHigh.position, transform.rotation);
        PlayAnimation("Shoot");
    }
   

    void Combo2(float waitTime)
    {
        ResetAttackTime(waitTime);
        PlayAnimation("LowPos");
        Instantiate(projectile, AttackSlotLow.position, transform.rotation);
        PlayAnimation("Shoot");
    }
    void Combo3(float waitTime) 
    {
        ResetAttackTime(waitTime);
        PlayAnimation("MidPos");
        for (int i = 30; i > -30; i -= 15)
        {
            AttackSlotMid.rotation = Quaternion.Euler(0, 0, i);
            Instantiate(projectile, AttackSlotMid.position, AttackSlotMid.rotation);
        }
        PlayAnimation("Shoot");
    }  
    void PlayAnimation(string animation)
    {
        currentAnimation = animation;
        animator.SetBool(animation, true);
        Invoke("ResetAnimation", 0.1f);
    }
    void ResetAnimation()
    {
        animator.SetBool(currentAnimation, false);
        currentAnimation = null;
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

}
