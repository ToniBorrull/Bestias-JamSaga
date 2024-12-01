using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public LayerMask mask;
    public float distanceFromPlayer;
    private Rigidbody rb;
    private float knockbackForce = 7f;
    [Header("Ataques")]
    public GameObject firstAttack;
    public GameObject secondAttack;

    [Header("PrimerAtaque")]
    public int fireballForce;
    public float cooldownAttack1;
    private float cooldownTimer1 = 0;

    [Header("SegundoAtaque")]
    public float cooldownAttack2;
    private float cooldownTimer2 = 0;

    private Player player;
    private int defeatedBosses;
    private Vector3 target;
    public bool knockbacking = false;


    private float knockbackTimer = 0;
    private float timerKnockback = 1;
    

    void Start()
    {
        player = GetComponent<Player>();
        defeatedBosses = player.defeatedBosses;
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (knockbacking && knockbackTimer < timerKnockback)
        {
            transform.position = Vector3.Lerp(transform.position, target, knockbackForce*Time.deltaTime);
            if (Vector3.Distance(transform.position, target) <= .2f)
            {
                knockbacking = false;
            }
            knockbackTimer += Time.deltaTime;
        }
        if (cooldownTimer2 >= cooldownAttack2)
        {
            if (defeatedBosses > 2)
            {
                if (Input.GetButtonDown("Q"))
                {
                    FourthAttack();
                    cooldownTimer2 = 0;
                }
            }
        }
        else
        {
            cooldownTimer2 += Time.deltaTime;
        }
        if (cooldownTimer1 >= cooldownAttack1)
        {
            if (defeatedBosses > 0)
            {
                if (Input.GetButtonDown("E"))
                {
                    SecondAttack();
                    cooldownTimer1 = 0;
                }
            }
            
        }
        else {
            cooldownTimer1 += Time.deltaTime;
        }


        
    }

    void FourthAttack()
    {
        Debug.Log("Cuarto ataque");
    }


    void SecondAttack()
    {
        //fireballs
        Vector3 vectorDirector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if (vectorDirector.sqrMagnitude == 0)
        {
            vectorDirector = transform.right;
        }
        else
        {
            vectorDirector.Normalize();
        }
        Debug.Log(vectorDirector);
        GameObject _ = Instantiate(firstAttack, transform.position + vectorDirector, Quaternion.identity);
        _.GetComponent<Rigidbody>().AddForce(vectorDirector * fireballForce);
        Destroy(_,3);
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("A");
        if (other.gameObject.TryGetComponent(out Boss1 en))
        {
            Debug.Log("E");
            if (!en.isFighting)
            {
                en.TakeDamage(1);
                en.UpdateHearts();
                Knockback();
                
            }
            else
            {
                player.TakeDamage(1);
                Knockback();
            }


        }
        if (other.gameObject.TryGetComponent(out Boss2 en2))
        {
            if (en2.stunned)
            {
                en2.TakeDamage(1);
                en2.UpdateHearts();
                Knockback();
            }
            else
            {
                player.TakeDamage(1);
            }
        }
        else if (other.CompareTag("BossAttack"))
        {
            player.TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    void Knockback()
    {
        knockbacking = true;
        target = new Vector3(transform.position.x-3,transform.position.y,transform.position.z);
        knockbackTimer = 0;

    }

}
