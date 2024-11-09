using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_PlayerCam : MonoBehaviour
{
    public static S_PlayerCam Instance {get; private set;}

    [Header("Sensivity")]
    [Range(1f, 20f)]public float sensivity;
    [SerializeField] private Slider sensivitySlider;
    
    [Header("Rotation Sensivity")]
    [SerializeField] private Slider rotationSensivitySlider;
    [Range(1f, 20f)]public float rotationSens;

    private float sensMax = 20f;

    
    [Header("Camera")]
    public Transform orientation;

    public bool isFreezed;

    
    private float xRotation;
    private float yRotation;



    private void Awake() 
    {
        Instance = this;    
    }

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;    
        Cursor.visible = false;
    
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
        if(!isFreezed)
        {
            HandleMouseMovement();
        }
    }

    private void HandleMouseMovement()
    {
        Vector2 mouseVector = GameInput.Instance.GetMouseMovementVectorNormalizedPlayer(); 
            //ARRUMAR ESSA BOSTA
            //VOLTA PRO CODIGO ANTIGO

        // get mouse input
        float mouseX = mouseVector.x * Time.deltaTime * sensivity;
        float mouseY = mouseVector.y * Time.deltaTime * sensivity;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void SetSensivity()
    {
        sensivity = sensivitySlider.value * sensMax;
    }
    public void SetRotationSens()
    {
        rotationSens = rotationSensivitySlider.value * sensMax;
    }
}
