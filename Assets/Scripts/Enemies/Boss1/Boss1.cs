using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss1 : Enemy
{
    public Fireball projectile;
    public Transform AttackSlot1;
    public Transform AttackSlot2;
    private Animator animator;
    private float lastAttack = 0f;
    public float waitTime;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        if (Time.time > lastAttack + atkRate)
        {
            waitTime = Random.Range(1, 3);
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

        //Debug.Log("ChosenAttack: " + chosenAttack);
       
        switch (chosenAttack)
        {
            case 0:
                Combo1();
            break;
                case 1:
                    Combo2();
                break;
                    case 2:
                        Combo3();
                    break;
            default:
            break;
                    
        }
    }
    void Combo1()
    {
        StartCoroutine(ControllerCombo(waitTime));
    }
    void Combo2() {
       // StartCoroutine();
    }
    void Combo3() {
    }

    IEnumerator ControllerCombo(float waitTime)
    {
        lastAttack = Time.time + waitTime;
        animator.SetBool("HighPos", true);
        Instantiate(projectile, AttackSlot1.position, transform.rotation);
        yield return new WaitForSeconds(waitTime);

        //Aqui se puede añadir un wait for animator clip (en el futuro)
        
        animator.SetBool("HighPos", false);
        Instantiate(projectile, AttackSlot2.position, transform.rotation);
    }
}
