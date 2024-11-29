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

    protected override void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Combo1();
        }
    }
    protected override void Die()
    {

    }

    void Combo1()
    {
        StartCoroutine(ControllerCombo());
    }
    IEnumerator ControllerCombo()
    {
        animator.SetBool("HighPos", true);
        Instantiate(projectile, AttackSlot1.position, transform.rotation);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("HighPos", false);
        Instantiate(projectile, AttackSlot2.position, transform.rotation);
    }
}
