using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Toni");
    }
}
