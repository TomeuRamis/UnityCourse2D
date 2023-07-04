using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    bool hasPackage = false;
    [SerializeField] float delayDestroy = 0.5f;
    [SerializeField] Color32 hasPackageColor = new Color32(1,0,0,1);
    [SerializeField] Color32 noPackageColor = new Color(0,0,0,1);

    SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.tag){
            case "Package":
                if(!hasPackage){
                    hasPackage = true;
                    spriteRenderer.color = hasPackageColor;
                    Destroy(other.gameObject, delayDestroy);
                    Debug.Log("Package picked up!");
                }else{
                    Debug.Log("Already has package");
                }
                break;
            case "Customer":
                if(hasPackage){
                    hasPackage = false;
                    spriteRenderer.color = noPackageColor;
                    Debug.Log("Package delivered!");
                }else{
                    Debug.Log("Customer reached!");
                }
                break;
            default:
                Debug.Log("Atropellao");
                break;
        }
    }
}
