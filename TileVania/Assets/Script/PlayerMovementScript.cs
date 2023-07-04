using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovementScript : MonoBehaviour
{

    Vector2 moveInput;
    Collider2D myCollider;
    Rigidbody2D myRigidBody;
    Animator myAnimator;

    private float gravityScale;
    [Header("Player atributes")]
    private bool isRunning = false;
    private bool isJumping = false;
    private bool isClimbing = false;
    private bool isOnLadder = false;
    private bool isJumpingFromLadder = false;
    [SerializeField] float playerSpeed = 1.0f;
    [SerializeField] float jumpSpeed = 10.0f;
    [SerializeField] float climbingSpeed = 5.0f;
    void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();
        myCollider = this.GetComponent<Collider2D>();
        myAnimator = this.GetComponent<Animator>();

        gravityScale = myRigidBody.gravityScale;
    }

    void Update()
    {
        OnLadder();
        Climbing();
        Jumping();
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && (!isJumping || isOnLadder))
        {
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }

        if(value.isPressed && isJumping && isOnLadder){
            isJumpingFromLadder = true;
        }else{
            isJumpingFromLadder = false;
        }

        Debug.Log("is jumping OR on ladder"+ !isJumping + isOnLadder);
    }

    void OnClimb(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void Run()
    {
        //Calculate isRunning
        isRunning = Mathf.Abs(moveInput.x) > Mathf.Epsilon;

        //Player movement
        Vector2 playerVelocity = new Vector2(moveInput.x * playerSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        //Animation
        myAnimator.SetBool("isRunning", isRunning);
    }

    /*
    * Jumping includes any aereal movement. 
    * Not just the jump action, but any falling, levitating or droping that the player does.
    */
    void Jumping()
    {
        //Update isJumping
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isJumping = false;
        }
        else
        {
            isJumping = true;
        }

        //Animation
        myAnimator.SetBool("isJumping", isJumping);
    }

    void OnLadder()
    {
        //Update isOnLadder
        isOnLadder = myCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));

        //Stop gravity from making the player go downwards if on ladder
        if(isJumping && isOnLadder && !isClimbing){
            
        }else if(isJumpingFromLadder){
            myRigidBody.gravityScale = gravityScale;
        }
        else if (isOnLadder)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, moveInput.y * climbingSpeed);
            myRigidBody.gravityScale = 0;
        }
        else
        {
            myRigidBody.gravityScale = gravityScale;
        }

        //Animation
        myAnimator.SetBool("isOnLadder", isOnLadder);
    }

    private void Climbing()
    {
        isClimbing = Mathf.Abs(moveInput.y) > Mathf.Epsilon;

        //Player movement
        if (isClimbing)
        {
            Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x, moveInput.y * climbingSpeed);
            myRigidBody.velocity = playerVelocity;
        }
        //Animation
        myAnimator.SetBool("isClimbing", isClimbing);
    }

    void FlipSprite()
    {
        if (isRunning)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1.0f);
        }
    }

}
