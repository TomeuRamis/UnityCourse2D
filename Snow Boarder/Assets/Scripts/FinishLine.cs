using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float reloadDelay = 1.5f;
    [SerializeField] ParticleSystem finishEffect;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            FindObjectOfType<PlayerControler>().disableControls();
            finishEffect.Play();
            GetComponent<AudioSource>().Play();
            Invoke("ReloadScene", reloadDelay);
            Debug.Log("Finish reached!");
        }
    }

    void ReloadScene(){
        SceneManager.LoadScene(0);  
    }
}
