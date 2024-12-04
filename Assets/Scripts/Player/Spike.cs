using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Boss1 en))
        {
            en.TakeDamage(1);
        }
        if(other.TryGetComponent(out Boss2 en2))
        {
            if(en2.stunned)
             en2.TakeDamage(1);
        }
    }

}

