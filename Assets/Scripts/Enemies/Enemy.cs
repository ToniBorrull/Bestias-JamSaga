using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float health;
    public float atkRate;
    public bool isAttacking;
    public int attacksDone;
    protected abstract void Start();

    protected abstract void Update();

    public virtual void TakeDamage(float dmg)
    {
        health -= dmg;
        health = health <= 0 ? 0 : health;
        if (health < 0) Die();
    }
    protected abstract void Die();

    protected void AttackDone()
    {
        attacksDone++;
    }

    protected bool isStunned()
    {
        isAttacking = attacksDone == 5 ? false : true;
        if (isAttacking )
        {
            attacksDone = 0;

        }
        return isAttacking;
    }

}