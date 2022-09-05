using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public Animator transition;

    public float transitiontime = 1f;

    public void LoadStartMenu()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
        Debug.Log("loadstartmenu wtf");
    }
    public void LoadStartgalaxite()
    {
        Debug.Log("loadstartmenu wtf");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //play anim
        transition.SetTrigger("startfade");
        //wait
        yield return new WaitForSeconds(transitiontime);
        //load scene
        SceneManager.LoadScene(levelIndex);
    }
}
