using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [Range(0.1f, 9f)][SerializeField] public float sensitivity = 2f;

    private float x;
    private Vector3 playerInputs;
    private float playerSpeed = 4f;
    private bool isFreezedPlayer;

    private CharacterController characterController;
    private Transform myCamera;
   
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start() 
    {
        myCamera = Camera.main.transform;
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(!GameController.Instance.IsGameStarted || GameController.Instance.IsGamePaused) return;

        x += Input.GetAxis("Mouse X") * sensitivity;
        var xQuat = Quaternion.AngleAxis(x, Vector3.up);
        transform.localRotation = xQuat;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        playerInputs = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        playerInputs = transform.TransformDirection(playerInputs);
        
        characterController.Move(playerInputs * Time.deltaTime * playerSpeed);

    }

    private void OnTriggerEnter(Collider collider) 
    {   
        if(collider.gameObject.CompareTag("FinalDoor"))
        {
            GameController.Instance.PlayFinalEvent();
            
            SceneManager.LoadScene(2);
        }
    }
}
