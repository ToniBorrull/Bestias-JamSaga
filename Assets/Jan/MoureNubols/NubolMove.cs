using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NubolMove : MonoBehaviour
{
    public float velocity;

    public GameObject respawn;

    private void Start()
    {

        Random.Range(0.15f, 1);

        velocity = Random.Range(0.15f, 1);
    }
    void Update()
    {
        this.transform.position -= transform.up * velocity * Time.deltaTime;

        if (velocity > 1.5f)
        {
            velocity = 1.5f;
        }
    }

    void ChangeVelocity()
    {
        velocity = Random.Range(0.15f, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("returnNubol"))
            {

            transform.position = new Vector3(respawn.transform.position.x , transform.position.y, transform.position.z);
            ChangeVelocity();
        }
            
    }
}
