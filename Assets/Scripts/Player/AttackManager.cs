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
    public GameObject thirdAttack;
    public GameObject fourthAttack;

    private Player player;
    private int defeatedBosses;
    private Collider[] ataque;
    private Vector3 delayVector;

    void Start()
    {

        player = GetComponent<Player>();
        defeatedBosses = player.defeatedBosses;
    }

    // Update is called once per frame
    void Update()
    {
        delayVector = new Vector3(distanceFromPlayer, 0, 0);
        if(defeatedBosses > 2)
        {
            FourthAttack();
        }

        if(defeatedBosses > 0)
        {
            if(Input.GetButtonDown("E"))
                SecondAttack();
        }

        if(Input.GetButtonDown("Fire1"))
            FirstAttack();


        
    }

    void FourthAttack()
    {

    }


    void SecondAttack()
    {
        //fireballs
        Vector3 vectorDirector = new Vector3(Input.GetAxis("horizontal"), Input.GetAxis("vertical"), 0);
        //spawnataque
        //enviar fuerza al vector director
    }

    void FirstAttack()
    {
        //espada
        ataque = Physics.OverlapSphere(transform.position + delayVector, 2, mask);
        foreach(Collider collider in ataque)
        {
            if (collider.gameObject.TryGetComponent<Enemy>(out Enemy en))
            {
                en.TakeDamage(1);
            }
        }
    }

}
