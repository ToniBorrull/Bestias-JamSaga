using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float minY;
    public float maxY;
    public float minX;
    public float maxX;
    public float speed;

    private float movementY;
    private float movementX;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementY += Input.GetAxis("Vertical") * speed * Time.deltaTime;
        movementX += Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        movementY = Mathf.Clamp(movementY, minY, maxY);
        movementX = Mathf.Clamp(movementX, minX, maxX);


        transform.position = new Vector3(movementX, movementY, 0);
    }
}
