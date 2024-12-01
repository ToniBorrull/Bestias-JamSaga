using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FMODPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      if(GameManager.instance.scene1 == SceneManager.GetActiveScene().name)
        {
         FMODUnity.RuntimeManager.PlayOneShot("event:/AmbientSound_2", GetComponent<Transform>().position);
        }  
    }

}
