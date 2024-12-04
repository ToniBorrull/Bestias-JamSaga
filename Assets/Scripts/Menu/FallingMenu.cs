using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void falling()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Polea", GetComponent<Transform>().position);
    }
}
