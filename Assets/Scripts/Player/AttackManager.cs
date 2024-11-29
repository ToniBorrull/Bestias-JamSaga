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

    void Start()
    {

        player = GetComponent<Player>();
        defeatedBosses = player.defeatedBosses;
    }

    // Update is called once per frame
    void Update()
    {
        if(defeatedBosses > 2)
        {
            if(Input.GetButtonDown("Q"))
                FourthAttack();
        }

        if(defeatedBosses > 0)
        {
            if(Input.GetButtonDown("E"))
                SecondAttack();
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
        vectorDirector.Normalize();
        Debug.Log(vectorDirector);
        //spawnataque
        //enviar fuerza al vector director
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy en))
        {
            en.TakeDamage(1);
        }
    }

}
