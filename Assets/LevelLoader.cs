using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitiontime = 1f;

    public void LoadArcade()
    {
        StartCoroutine(LoadLevel(1));
    }

        public void LoadGalaxite()
    {
        StartCoroutine(LoadLevel(2));
    }
    public void LoadStartMenu()
    {
        StartCoroutine(LoadLevel(0));
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
