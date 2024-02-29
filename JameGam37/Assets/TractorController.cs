using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorController : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float acceleration = 5f;
    public float deceleration = 5f;
    public float rotationSpeed = 100f;

    private float currentSpeed = 0f;

    public GameObject frontTire;
    public Animator FrontTireAnimator;
    //public Animator BackTireAnimator;
    bool Idle;
    void FixedUpdate()
    {
        
        // Acceleration and Deceleration
        float moveInput = Input.GetAxis("Vertical");
        Debug.Log("move input is: " + moveInput);
        if (moveInput == 0)
        {
            Idle = true;
        }
        if (moveInput > 0f)
        {
            Idle = false;
            // Accelerate
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        }
        else if (moveInput < 0f)
        {
            Idle = false;
            // Decelerate (apply negative acceleration)
            currentSpeed = Mathf.MoveTowards(currentSpeed, -maxSpeed, deceleration * Time.deltaTime);
        }
        else
        {
            // Decelerate to stop when not pressing any movement keys
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }


        // Movement
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        // Rotation
        
        if (!Idle)
        {
            float rotationInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            FrontTireAnimator.SetFloat("SteerSpeed", -1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            FrontTireAnimator.SetFloat("SteerSpeed", 1);
        }
        else
            FrontTireAnimator.SetFloat("SteerSpeed", 0);
    }
}
