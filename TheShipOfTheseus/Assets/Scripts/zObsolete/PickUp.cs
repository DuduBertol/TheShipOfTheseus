using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    public Transform holdPos;
    public Transform objectsPlaceArea;
    public float pickUpRange = 5f;
    public float throwForce = 500f;
    
    [Header("Sensivity")]
    [Range(0.1f, 9f)][SerializeField] private float rotationSensitivity = 1f;
    [SerializeField] private Slider sensivitySlider;
    [SerializeField] private Slider rotationSensivitySlider;

    [Header("Others")]
    [SerializeField] private Transform selectionCursor;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private Movement movement;


    
    private GameObject heldObject;
    private Rigidbody heldObjectRigidboby;
    private bool canDrop = true;
    private float initialCamSensivity;
    private float initialMoveSensivity;
    private float initialRotationSensivity;
    private int layerNumber;


    private void Start() 
    {
        cameraMovement.enabled = true;

        layerNumber = LayerMask.NameToLayer("holdLayer"); 

        initialCamSensivity = cameraMovement.sensitivity;
        initialMoveSensivity = movement.sensitivity;
        initialRotationSensivity = rotationSensitivity;

        sensivitySlider.value = initialMoveSensivity;
        rotationSensivitySlider.value = initialRotationSensivity;
    }

    private void Update()
    {
        RaycastHit hit; 
        //Disparei um raycast

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange, ~playerLayer))
        {
            if(hit.transform.gameObject.tag == "canPickUp")
            //Achei um objeto pegável
            {
                selectionCursor.gameObject.SetActive(true);
            }
            else
            {
                selectionCursor.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if(heldObject == null)
            //Tenho um objeto
            {
                if(hit.transform.gameObject.tag == "canPickUp")
                //É um objeto pegável
                {
                    PickUpObject(hit.transform.gameObject);
                    //Pego o objeto
                }
            }
            else
            {
                if(canDrop == true)
                //Ele pode ser largado
                {
                    DropObject();
                }
            }
        }


        if (heldObject != null) 
        {
            /* if(heldObject.GetComponent<BoardObject>().GetIsCard())
            {
                if(Input.GetKeyDown(KeyCode.F))
                {
                    int cardNumber = heldObject.GetComponent<BoardObject>().GetCardNumber();
                    GameController.Instance.ActiveCard(cardNumber);
                }
            } */

            MoveObject(); 
            RotateObject();
            
            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true)
            {
                ThrowObject();
            }
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * pickUpRange, Color.green);
    }


    private void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) 
        {
            heldObject = pickUpObj; 
            heldObjectRigidboby = pickUpObj.GetComponent<Rigidbody>(); 
            heldObjectRigidboby.isKinematic = true;
            heldObjectRigidboby.transform.parent = holdPos.transform; 
            heldObject.layer = layerNumber; 
            if(heldObject.transform.childCount != 0)
            {
                heldObject.transform.GetChild(0).gameObject.layer = layerNumber;
                heldObject.transform.GetChild(1).gameObject.layer = layerNumber;
            }
            heldObject.transform.rotation = Quaternion.RotateTowards(heldObject.transform.rotation, Quaternion.Euler(0, 90, 0), 1f);
            
            if(heldObject.GetComponent<BoardObject>().GetIsCard())
            {
                GameController.Instance.ActiveLerFText(true);
            }
            Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    private void DropObject()
    {
        GameController.Instance.ActiveLerFText(false);
        Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), false);

        heldObject.layer = 8; 
        if(heldObject.transform.childCount != 0)
        {
            heldObject.transform.GetChild(0).gameObject.layer = 8;
            heldObject.transform.GetChild(1).gameObject.layer = 8;
        }
        heldObjectRigidboby.isKinematic = false;
        heldObject.transform.parent = null;
        heldObject = null; 

    }
    
    /* private void DropObjectOnBoard()
    {
        GameController.Instance.ActiveLerFText(false);
        Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), false);

        heldObject.layer = 8; 
        if(heldObject.transform.childCount != 0)
        {
            heldObject.transform.GetChild(0).gameObject.layer = 8;
            heldObject.transform.GetChild(1).gameObject.layer = 8;
        }
        heldObjectRigidboby.isKinematic = true;
        heldObject.transform.parent = objectsPlaceArea; 
        heldObject.transform.localPosition = new Vector3(0, heldObject.transform.localPosition.y, heldObject.transform.localPosition.z); 
        heldObject.transform.localEulerAngles = Vector3.zero;
        heldObject.GetComponent<BoardObject>().SetIsOnBoard(true);
        heldObject = null; 
    } */

    private void RotateObject()
    {
        if (Input.GetKey(KeyCode.R))
        {
            GameController.Instance.ShowLens(true);

            canDrop = false;
            cameraMovement.sensitivity = 0f;
            movement.sensitivity = 0f;

            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
            float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;

            heldObject.transform.Rotate(Vector3.down, XaxisRotation);
            heldObject.transform.Rotate(Vector3.forward, YaxisRotation);
        }
        else
        {
            GameController.Instance.ShowLens(false);

            cameraMovement.sensitivity = initialCamSensivity;
            movement.sensitivity = initialMoveSensivity;
            canDrop = true;
        }
    }

    private void MoveObject()
    {
        heldObject.transform.position = holdPos.transform.position;
    }

    private void ThrowObject()
    {
        SoundManager.Instance.PlayThrowSound(heldObject.transform.position, 1);
        GameController.Instance.ActiveLerFText(false);
        Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), false);

        heldObject.layer = 8;
        if(heldObject.transform.childCount != 0)
        {
            heldObject.transform.GetChild(0).gameObject.layer = 8;
            heldObject.transform.GetChild(1).gameObject.layer = 8;
        }
        heldObjectRigidboby.isKinematic = false;
        heldObject.transform.parent = null;
        heldObjectRigidboby.AddForce(transform.forward * throwForce);
        heldObject = null;
    }

    public void SetRotationSensivitySlider()
    {
        rotationSensitivity = rotationSensivitySlider.value * initialRotationSensivity;
    }

    public void SetSensivitySlider()
    {
        cameraMovement.sensitivity = sensivitySlider.value * initialCamSensivity;
        movement.sensitivity = sensivitySlider.value * initialMoveSensivity;
    }

    
}
