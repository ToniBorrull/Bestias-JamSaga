using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimationEvents : MonoBehaviour
{
    public Animator animator;
    public Boss1 boss;
    public ParticleSystem shootParticles;
    public void Shoot()
    {
        animator.SetTrigger("Shoot");
    }

    public void InstanceBullet()
    {
        boss.CreateBullet();
        shootParticles.Play();
    }

}
