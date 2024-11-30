using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string scene;
    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
        GameManager.instance.OnSceneChange();
    }


}
