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
    public bool stunned = false;
    private float stunTimer = 0f;
    public float stunTime;
    public float rotationSpeed;

    public GameObject stars;
    public GameObject[] hearts;
    private Rigidbody rb;
    
    

    protected override void Start()
    {
        player = GameManager.instance.GetPlayer();
        animator = GetComponent<Animator>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Time.time > lastAttack + atkRate)
        {
            if (stunned)
            {
                //stunAnim
                if(Vector3.Distance(transform.position,originalPosition)>.2f)
                {
                    transform.position = Vector3.Lerp(transform.position, originalPosition, attackSpeed * Time.deltaTime);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationSpeed * Time.deltaTime);
                }
                else if (stunTimer <= stunTime) {
                    stars.SetActive(true);
                    Debug.Log("Stunned");
                    stunTimer += Time.deltaTime;
                }
                else
                {
                    stars.SetActive(false);
                    attacksDone = 0;
                    stunned = false;
                    stunTimer = 0f;
                }
            }
            else
            {
                stunned = !isStunned();
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
        rb = GetComponent<Rigidbody>();
        Vector3 randomTorque = new Vector3(UnityEngine.Random.Range(2, 5), UnityEngine.Random.Range(3, 10), UnityEngine.Random.Range(2, 3));
        rb.AddTorque(randomTorque, ForceMode.Impulse);
        Destroy(gameObject, 1f);
        Debug.Log("nigga");
    }

    void ChooseAttack()
    {
        chosenAttack = Mathf.Floor(UnityEngine.Random.Range(0, 3));
        attacking = false;
        transform.rotation = Quaternion.identity;
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
            gotPosition = true;
        }
        else
        {
            if (Vector3.Distance(playerPos, transform.position) > .5f && firstPhase)
            {
                transform.position = Vector3.Lerp(transform.position, playerPos, attackSpeed * Time.deltaTime);
                
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 15), rotationSpeed*Time.deltaTime); ;

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
                if (Vector3.Distance(transform.position, originalPosition) > .1f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -15), rotationSpeed*Time.deltaTime); ;

                    transform.position = Vector3.Lerp(transform.position, originalPosition, attackSpeed * Time.deltaTime);
                }
                else
                {
                    firstPhase = true;
                    secondPhase = true;
                    gotPosition = false;
                    transform.rotation = Quaternion.identity;

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
            GameObject _ = Instantiate(spike, transform.position, Quaternion.Euler(0, 0, i));
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
    public void UpdateHearts()
    {
        hearts[(int)health].GetComponent<Rigidbody>().useGravity = true;
        Rigidbody heartsRb = hearts[(int)health].GetComponent<Rigidbody>();
        Vector3 randomTorque = new Vector3(0, UnityEngine.Random.Range(3, 10), 0);
        heartsRb.AddTorque(randomTorque, ForceMode.Impulse);
        Destroy(hearts[(int)health], 2f);
    }

}
