using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public LayerMask mask;
    public float distanceFromPlayer;
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
    

    void Start()
    {
        player = GetComponent<Player>();
        defeatedBosses = player.defeatedBosses;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (other.gameObject.TryGetComponent(out Enemy en))
        {
            if(!en.isAttacking)
                en.TakeDamage(1);
            else
                player.TakeDamage(1);
        }
    }

}
