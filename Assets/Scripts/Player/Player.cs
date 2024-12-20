using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            Vector3 randomTorque = new Vector3(Random.Range(1, 5), Random.Range(2, 3), Random.Range(1, 2));
            rb.AddTorque(randomTorque, ForceMode.Impulse);
        StartCoroutine(ReturnMenu());
        
            
    }

    IEnumerator ReturnMenu()
    {
        yield return new WaitForSeconds(1);
        GameManager.instance.ChangeScene("Menu");
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
        for(int yihad = 0; yihad < 10; yihad++)
        {
            //this.GetComponent<Renderer>().material.color = Color.red;

            yield return new WaitForSeconds(.2f);


            //this.GetComponent<Renderer>().material.color = Color.white;
        }


        playerColision.enabled = true;
    }
    void UpdateHearts()
    {
        if (!isDead)
        {
            hearts[health].GetComponent<Rigidbody>().useGravity = true;
            Rigidbody heartsRb = hearts[health].GetComponent<Rigidbody>();
            Vector3 randomTorque = new Vector3(0, Random.Range(3, 10), 0);
            heartsRb.AddTorque(randomTorque, ForceMode.Impulse);
            Destroy(hearts[health], 1f);
        }
    }

}
