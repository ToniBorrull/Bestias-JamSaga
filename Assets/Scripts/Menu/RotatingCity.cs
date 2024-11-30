using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class RotatingCity : MonoBehaviour
{
    bool goingA;
    float i = 0;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        i += Time.deltaTime * speed;

        Quaternion rotation = Quaternion.Euler(0, i, 0);
        transform.rotation = rotation;
    }
}
