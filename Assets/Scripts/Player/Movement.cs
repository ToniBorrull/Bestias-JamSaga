using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float minY;
    public float maxY;
    public float speed;

    private float movement;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement += Input.GetAxis("Vertical") * speed * Time.deltaTime;

        movement = Mathf.Clamp(movement, minY, maxY);

        transform.position = new Vector3(0, movement, 0);
    }
}
