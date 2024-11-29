using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Enemy
{
    private Animator animator;
    private float timer;
    private float lastAttack = 0f;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        timer += Time.deltaTime;
        if (Time.time > lastAttack + atkRate)
        {
            ChooseAttack();
        }
    }

    protected override void Die()
    {

    }

    void ChooseAttack()
    {
        float chosenAttack = Mathf.Floor(Random.Range(0, 3));
        switch (chosenAttack)
        {
            case 0:
                Attack1();
                break;
            case 1:
                Attack2();
                break;
            case 2:
                Attack3();
                break;
            default:
                break;
        }
    }

    void Attack1()
    {

    }

    void Attack2()
    {

    }

    void Attack3()
    {

    }

    void ResetAttackTime(float waitTime)
    {
        lastAttack = Time.time + waitTime;
    }
}
