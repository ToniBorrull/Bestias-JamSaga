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

    protected override void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
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
        float chosenAttack = 1;//Mathf.Floor(Random.Range(0, 3));

        //Debug.Log("ChosenAttack: " + chosenAttack);
       
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
        StartCoroutine(ControllerCombo1(waitTime));
    }
    void Combo2(float waitTime)
    {
        lastAttack = Time.time + waitTime;
        animator.SetBool("MidPos", true);
        Instantiate(projectile, AttackSlotMid.position, transform.rotation);
        AttackSlotMid.rotation = Quaternion.EulerAngles(transform.rotation.x, transform.rotation.y, 0);
        Instantiate(projectile, AttackSlotMid.position, transform.rotation);
        AttackSlotMid.rotation = Quaternion.EulerAngles(transform.rotation.x, transform.rotation.y, 30);
        Instantiate(projectile, AttackSlotMid.position, transform.rotation);
        animator.SetBool("MidPos", false);
        //(ContollerCombo2(combo2Rate));
    }
    void Combo3(float waitTime) {
    }

    IEnumerator ControllerCombo1(float waitTime)
    {
        lastAttack = Time.time + waitTime;
        animator.SetBool("HighPos", true);
        Instantiate(projectile, AttackSlotHigh.position, transform.rotation);
        yield return new WaitForSeconds(waitTime);

        //Aqui se puede añadir un wait for animator clip (en el futuro)
        
        animator.SetBool("HighPos", false);
        Instantiate(projectile, AttackSlotLow.position, transform.rotation);
    }
}
