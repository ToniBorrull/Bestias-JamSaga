using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;

public class Boss2 : Enemy
{
    public int speed;
    public float minX;
    public float maxX;

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
    

    protected override void Start()
    {
        player = GameManager.instance.player;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        timer += Time.deltaTime;
        if (Time.time > lastAttack + atkRate)
        {
            ChooseAttack();
        }
    }

    protected override void Die()
    {

    }

    void ChooseAttack()
    {
        //float chosenAttack = Mathf.Floor(UnityEngine.Random.Range(0, 3));
        //switch (chosenAttack)
        //{
        //    case 0:
                Attack1();
        //        break;
        //    case 1:
        //      Attack2();
        //        break;
        //    case 2:
        //        Attack3();
        //        break;
        //    default:
        //        break;
    
        //}

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
            }
            else if(secondPhase)
            {
                firstPhase = false;
                //animacion pegar
                
                secondPhase = false;
            }

            if (!firstPhase && !secondPhase)
            {
                if (Vector3.Distance(transform.position, originalPosition) > 0.1f)
                {
                    transform.position = Vector3.Lerp(transform.position, originalPosition, attackSpeed * Time.deltaTime);
                }
                else
                {
                    firstPhase = true;
                    secondPhase = true;
                    ResetAttackTime(tailDelay);
                }
            }



        }
    }

    void Attack2()
    {
        //mucho pincho
        ResetAttackTime(spikeDelay);
        for (int i = 0; i <= 180; i += 30) {
            GameObject _ = Instantiate(spike, transform.position, Quaternion.Euler(0, 0, i-90));
            _.GetComponent<Rigidbody>().AddForce(_.transform.up * spikeForce);
            Destroy(_, 3);
        }

    }

    void Attack3()
    {
        //persecucion
        if (!attackCharged)
        {
            StartCoroutine(Persecution(2));
        }
        
        if (attackCharged)
        {
            if (firstPhase)
            {
                if (transform.position.x > minX)
                {
                    transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
                }
                else
                {
                    firstPhase = false;
                }
            }
            else if (secondPhase)
            {
                firstPhase = false;
                if (transform.position.x < maxX)
                {
                    transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
                }
                else { secondPhase = false; }
            }
            else
            {
                secondPhase = false;
                StartCoroutine(Persecution(2));
            }

        }
    }

    void ResetAttackTime(float waitTime)
    {
        lastAttack = Time.time + waitTime;
    }

    IEnumerator Persecution(float waitTime)
    {
        //animación cargar rotación
        yield return new WaitForSeconds(waitTime*0.5f);
        if (!attackCharged)
        {
            attackCharged = true;
            //animación moverse

        }
        else if (!secondPhase && !firstPhase)
        {
            secondPhase = true;
            firstPhase = true;
            attackCharged = false;
            ResetAttackTime(waitTime);
        }
    }

}
