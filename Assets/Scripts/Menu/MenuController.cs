using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

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
    }








    /*
        public Animator transition;

        public float transitionTime = 1f;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                LoadNextLevel();
            }
        }

        public void LoadNextLevel()
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }

        IEnumerator LoadLevel(int index)
        {
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(index);
        }
    }*/


}
