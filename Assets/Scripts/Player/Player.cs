using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public bool isDead;
    public int defeatedBosses;
    private Collider playerColision;
    void Start()
    {
        playerColision = GetComponent<Collider>();
        defeatedBosses = GameManager.instance.defeatedEnemies;
        isDead = false;
        if (defeatedBosses > 1)
        {
            maxHealth += 1;
            //escudo.SetActive(true);
        }

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = health <= 0 ? 0 : health;
        if (health < 0) Die();
    }

    private void Die()
    {
        if (!isDead)
        {
            //animacion muerte
            isDead = true;
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        StartCoroutine(Invencibilidad());
    }

    private IEnumerator Invencibilidad()
    {
        playerColision.enabled = false;
        yield return null;
        playerColision.enabled = true;
    }

}
