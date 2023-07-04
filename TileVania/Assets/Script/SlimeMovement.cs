using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    [SerializeField] float moveSpeed = 1.0f;
    void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        
    }

    void Move(){
        myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Ground"){
            moveSpeed *= -1;
            FlipSprite();
        }
    }


    void FlipSprite(){
        this.transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1.0f);
    }
}
