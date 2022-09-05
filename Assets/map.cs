using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class map : MonoBehaviour
{
 public Animator anim;
 public static bool maptoggled = false;

    void Start () {
        anim = GetComponent<Animator>();
    }
    void togglemap()
    {
        if (Input.GetKeyUp(KeyCode.M) && !maptoggled)
        {
            anim.SetBool("ismapopen", true);
            maptoggled = true;
            Debug.Log("true");
        }
        else
        {
            anim.SetBool("ismapopen", false);
            maptoggled = false;
            Debug.Log("false");
        }
        
    }
     
}

