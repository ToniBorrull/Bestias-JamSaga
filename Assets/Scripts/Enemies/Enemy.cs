using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float health;
    public float atkRate;
    public bool isAttacking;
    protected abstract void Start();

    protected abstract void Update();

    public virtual void TakeDamage(float dmg)
    {
        health -= dmg;
        health = health <= 0 ? 0 : health;
        if (health < 0) Die();
    }
    protected abstract void Die();
}