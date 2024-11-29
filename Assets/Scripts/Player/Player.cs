using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health;

    public float maxHealth;
    public bool isDead;

    void Start()
    {
        isDead = false;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = health <= 0 ? 0 : health;
        if (health < 0) Die();
    }

    void Die()
    {
        if (!isDead)
        {
            //animacion muerte
            isDead = true;
        }
    }

    void TakeDamage(float dmg)
    {
        health -= dmg;
    }

}
