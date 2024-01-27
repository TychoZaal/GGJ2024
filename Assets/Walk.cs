using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    //the animator component attached to this object
    Animator anim;

    void Start()
    {
        //get the animator component
        anim = GetComponent<Animator>();
    }

    //switch between idle and walk animations every 5 seconds
    void Update()
    {
        if (Time.time % 10 < 5)
        {
            //set isWalking to false
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
    }
}
