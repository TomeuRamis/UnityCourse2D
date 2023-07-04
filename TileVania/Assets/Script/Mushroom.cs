using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    Animator myAnimator;
    void Start()
    {
        myAnimator = this.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
         myAnimator.ResetTrigger("Bounce");
         myAnimator.SetTrigger("Bounce");
       
    }
}
