using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerPickUp : MonoBehaviour
{
    public float pickUpRange;
    public Transform playerHoldPos;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject heldObject;

    private void Update() 
    {
        RaycastHit hit; 
        //Disparei um raycast

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange, ~playerLayer))
        {
            if(hit.transform.gameObject.TryGetComponent(out S_InteractableObject interactableObject))
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Interact!");

                    if(heldObject == null)
                    {
                        heldObject = interactableObject.gameObject;
                        interactableObject.SetParent(playerHoldPos);
                    }
                    else
                    {
                        heldObject = null;
                        interactableObject.ClearParent();
                    }
                }
            }
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * pickUpRange, Color.green);
    }
}
