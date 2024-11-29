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
    private bool firstPhase = true;
    private bool secondPhase = true;
    private Animator animator;
    private float timer;
    private float lastAttack = 0f;
    private bool attackCharged = false;
    

    protected override void Start()
    {
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
        //float chosenAttack = Mathf.Floor(Random.Range(0, 3));
        //switch (chosenAttack)
        //{
        //    case 0:
        //      Attack1();
        //        break;
        //    case 1:
        //        Attack2();
        //        break;
        //    case 2:
                Attack3();
        //        break;
        //    default:
        //        break;
        //}
    }

    void Attack1()
    {
        //coletazo

    }

    void Attack2()
    {
        //mucho pincho
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
