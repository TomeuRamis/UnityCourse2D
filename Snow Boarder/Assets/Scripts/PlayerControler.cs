using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] float torqueAmmount = 1f;
    [SerializeField] float boostSpeed = 50f;
    float normalSpeed = 20f;

    Rigidbody2D rb2d;
    SurfaceEffector2D se2d;

    bool disabledControls = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        se2d = FindObjectOfType<SurfaceEffector2D>();
        normalSpeed = se2d.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(!disabledControls){
            rotatePlayer();
            boostPlayer();
        }
    }

    void boostPlayer(){
        if(Input.GetKey(KeyCode.UpArrow)){
            se2d.speed =  boostSpeed;
        }else if(Input.GetKey(KeyCode.DownArrow)){
            se2d.speed =  normalSpeed;
        }
    }

    void rotatePlayer(){
         if(Input.GetKey(KeyCode.LeftArrow)){
            rb2d.AddTorque(torqueAmmount);
        } else if(Input.GetKey(KeyCode.RightArrow)){
            rb2d.AddTorque(-torqueAmmount);
        }
    }

    public void disableControls(){
        disabledControls = true;
    }
}
