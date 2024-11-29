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
    private float timer;
    private string currentAnimation;
    private bool isFighting;
    protected override void Start()
    {

        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        timer += Time.deltaTime;
        if (Time.time > lastAttack + atkRate)
        {
            combo1Rate = Random.Range(1, 3);
            ChooseAttack();
        }
    }
    protected override void Die()
    {
    }
    void ChooseAttack()
    {
        //3 ataques de 0 a 2
        float chosenAttack = Mathf.Floor(Random.Range(0, 3));
       
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
    }
   

    void Combo2(float waitTime)
    {
        ResetAttackTime(waitTime);
        PlayAnimation("LowPos");
        Instantiate(projectile, AttackSlotLow.position, transform.rotation);
       
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
    }  
    void PlayAnimation(string animation)
    {
        currentAnimation = animation;
        animator.SetBool(animation, true);
        Invoke("ResetAnimation", 0.2f);
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
}
