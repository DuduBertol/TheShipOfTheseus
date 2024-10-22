using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerCam : MonoBehaviour
{
    public static S_PlayerCam Instance {get; private set;}

    public float sensibility;
    public float rotationSens;
    
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
        float mouseX = mouseVector.x * Time.deltaTime * sensibility;
        float mouseY = mouseVector.y * Time.deltaTime * sensibility;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
