using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 0.1f;
    [SerializeField] float driveSpeed = 0.01f;
    [SerializeField] float slowSpeed = 15f;
    [SerializeField] float fastSpeed = 30f;

    void Start()
    {
        
    }

    void Update()
    {
        float steerAmout = -Input.GetAxis("Horizontal")*steerSpeed * Time.deltaTime;
        float speedAmount = Input.GetAxis("Vertical")*driveSpeed * Time.deltaTime;
        transform.Rotate(0,0,steerAmout);
        transform.Translate(0,speedAmount,0);

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "SpeedUp"){
            driveSpeed = fastSpeed;
            Debug.Log("Speed up!");
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Obstacle"){
            driveSpeed = slowSpeed;
            Debug.Log("Speed down!");
        }
    }
}
