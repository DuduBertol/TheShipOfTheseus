using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerMovement : MonoBehaviour
{

    public static S_PlayerMovement Instance {get; private set;}

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public Transform orientation;

    public bool isFreezed;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private void Awake() 
    {
        Instance = this;    
    }

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;    
        rb.drag = groundDrag;

        S_EventManager.Instance.OnGameOver += S_EventManager_OnGameOver;
        S_EventManager.Instance.OnToggleIsPaused += S_EventManager_OnToggleIsPaused;
    }

    private void S_EventManager_OnToggleIsPaused(object sender, EventArgs e)
    {
        isFreezed = S_EventManager.Instance.isPaused;
    }

    private void S_EventManager_OnGameOver(object sender, EventArgs e)
    {
        isFreezed = true;
    }

    private void Update() 
    {
        MyInput();    
        SpeedControl();
    }

    private void FixedUpdate() 
    {
        if(!isFreezed)
        {
            MovePlayer();    
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Invoke(nameof(ResetJump), jumpCooldown); >> Conhecça a existência desse invoke com cooldown!!!
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 1f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
