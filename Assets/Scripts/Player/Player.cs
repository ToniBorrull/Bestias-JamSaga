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
    public GameObject[] hearts;
    public Movement playerMovement;
    public Rigidbody rb;
    
    void Start()
    {
        playerColision = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
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
    }

    private void Die()
    {
            playerMovement.enabled = false;
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddTorque(new Vector3(5, 3, 2), ForceMode.Impulse);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        StartCoroutine(Invencibilidad());
        if(health > 0)
            UpdateHearts();
        else
        {
            UpdateHearts();
            Die();
        }
    }

    private IEnumerator Invencibilidad()
    {
        playerColision.enabled = false;
        yield return null;
        playerColision.enabled = true;
    }
    void UpdateHearts()
    {
        hearts[health].GetComponent<Rigidbody>().useGravity = true;
        Destroy(hearts[health], 1f);
    }

}
