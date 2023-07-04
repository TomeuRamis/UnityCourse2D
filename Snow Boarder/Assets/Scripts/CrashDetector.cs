using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float reloadDelay = 1.5f;
    [SerializeField] ParticleSystem bonkEffect;
    [SerializeField] AudioClip bonkSFX;

    bool bonked = false;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground"){
            if(!bonked){
                FindObjectOfType<PlayerControler>().disableControls();
                bonkEffect.Play();
                GetComponent<AudioSource>().PlayOneShot(bonkSFX);
                Invoke("ReloadScene", reloadDelay);
                bonked = true;
            }
            Debug.Log("Bonk!");
        }
    }

    void ReloadScene(){
        SceneManager.LoadScene(0);  
    }
}
