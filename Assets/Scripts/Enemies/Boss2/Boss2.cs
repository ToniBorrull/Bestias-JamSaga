using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Boss2 : Enemy
{
    public bool isFighting = false;
    public int speed;
    public float minX;
    public float maxX;
    public float persecutionTime;
    private float chosenAttack;
    private bool attacking = false;
    private bool isActive = false;


    public GameObject spike;
    public int spikeForce;
    public float spikeDelay;

    private bool firstPhase = true;
    private bool secondPhase = true;

    private bool gotPosition = false;
    private Vector3 playerPos = Vector3.zero;
    public float attackSpeed;
    public float tailDelay;
    private Vector3 originalPosition = Vector3.zero;

    private Player player;
    private Animator animator;
    private float timer;
    private float lastAttack = 0f;
    private bool attackCharged = false;
    private bool stunned = false;
    private float stunTimer = 0f;
    public float stunTime;
    
    

    protected override void Start()
    {
        player = GameManager.instance.GetPlayer();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Time.time > lastAttack + atkRate)
        {
            if (stunned)
            {
                //stunAnim
                if (stunTimer <= stunTime) {
                    Debug.Log("Stunned");
                    stunTimer += Time.deltaTime;
                }
                else
                {
                    stunned = false;
                    stunTimer = 0f;
                }
            }
            else
            {
                stunned = isStunned();
                ChooseAttack();
                AttackDone();
            }
        }
        else
        {
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
    }

    protected override void Die()
    {

    }

    void ChooseAttack()
    {
        chosenAttack = Mathf.Floor(UnityEngine.Random.Range(0, 3));
        attacking = false;
        switch (chosenAttack)
        {
                case 0:
                ResetAttackTime(tailDelay);
                break;
                case 1:
                ResetAttackTime(spikeDelay);
                break;
                case 2:
                firstPhase = true;
                secondPhase = true;
                ResetAttackTime(persecutionTime);
                break;
        }
        is_Persecution_active = false;

    }

    void Attack1()
    {
        //coletazo
        if (!gotPosition)
        {
            playerPos = player.transform.position;
            originalPosition = transform.position;
            gotPosition = true;
        }
        else
        {
            if (Vector3.Distance(playerPos, transform.position) > 5 && firstPhase)
            {
                transform.position = Vector3.Lerp(transform.position, playerPos, attackSpeed * Time.deltaTime);
                
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 30), 1); ;

            }
            else if(secondPhase)
            {
                firstPhase = false;
                //animacion pegar
                StartCoroutine(Tail());
                secondPhase = false;
            }

            if (!firstPhase && !secondPhase)
            {
                if (Vector3.Distance(transform.position, originalPosition) > 0.1f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -30), 1); ;

                    transform.position = Vector3.Lerp(transform.position, originalPosition, attackSpeed * Time.deltaTime);
                }
                else
                {
                    firstPhase = true;
                    secondPhase = true;
                    gotPosition = false;

                }
            }



        }
    }

    IEnumerator Tail()
    {
        animator.SetBool("Tail", true);
        yield return new WaitForSeconds(.25f);
        animator.SetBool("Tail", false);
    }

    void Attack2()
    {
        if (attacking)
            return;
        //mucho pincho

        
        

        attacking = true;
        StartCoroutine(HandleSpikesAttack());
    }

    IEnumerator HandleSpikesAttack()
    {
        animator.SetBool("Spikes", true);

        yield return new WaitForSeconds(0.25f);

        SpawnSpikes();

        animator.SetBool("Spikes", false);

        yield return new WaitForSeconds(1f); 

        attacking = false;
    }

    void SpawnSpikes()
    {
        for (int i = 0; i <= 180; i += 30)
        {
            GameObject _ = Instantiate(spike, transform.position, Quaternion.Euler(0, 0, i - 90));
            _.GetComponent<Rigidbody>().AddForce(_.transform.up * spikeForce);
            Destroy(_, 3);
        }
    }

    void Attack3()
    {
        if (!attackCharged)
        {

            StartCoroutine(PersecutionActive(persecutionTime));
            return;
        }

        if (firstPhase)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= minX)
            {
                firstPhase = false; 
            }
        }
        else if (secondPhase)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x >= maxX)
            {
                secondPhase = false;
                transform.rotation = Quaternion.identity;

            }
        }
        else
        {
            if (!secondPhase && !firstPhase && is_Persecution_active && attackCharged)
            {

                StartCoroutine(PersecutionDisable(persecutionTime));
            }
        }
    }

    bool is_Persecution_active = false;
    IEnumerator PersecutionActive(float waitTime)
    {
        if (isActive) yield break;
        if (is_Persecution_active) yield break;
        is_Persecution_active = true;
        isActive = true;


        //Debug.Log("PersecutionActive");

        yield return new WaitForSeconds(waitTime * 0.25f);

        animator.SetBool("Stampede", true);
        attackCharged = true;
        isActive = false;

       
    }

    IEnumerator PersecutionDisable(float waitTime)
    {
        if (isActive) yield break;
        if (!is_Persecution_active) yield break;
        //Debug.Log("PersecutionDisable");
        isActive = true;

        yield return new WaitForSeconds(waitTime * 0.25f);
        if (!secondPhase && !firstPhase)
        {
            animator.SetBool("Stampede", false);
            ResetAttackPhases();
        }

        isActive = false;
    }

    void ResetAttackPhases()
    {
        secondPhase = true;
        firstPhase = true;
        attackCharged = false;
    }


    void ResetAttackTime(float waitTime)
    {
        lastAttack = Time.time + waitTime;

    }

}
