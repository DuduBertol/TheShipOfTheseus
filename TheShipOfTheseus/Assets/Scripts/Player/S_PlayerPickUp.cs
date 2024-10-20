using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerPickUp : MonoBehaviour
{
    public float pickUpRange;
    public Transform playerHoldPos;
    public Transform playerTwinPos;

    public enum ObjectViewState
    {
        View_Hold,
        NoView_Hold,
        View_NoHold,
        NoView_NoHold
    }

    public ObjectViewState objectViewState;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Collider playerCollider;
    [SerializeField] private GameObject heldObject;
    [SerializeField] private GameObject highlightObject;

    private void Start() 
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) // Mouse Esquerdo
    {
        Debug.Log("Interact!");

        RaycastHit hit; 
        //Disparei um raycast

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange, ~playerLayer))
        {
            if(hit.transform.gameObject.TryGetComponent(out S_InteractableObject interactableObject)) 
            // Encontrei um objeto interagível
            {
                if(heldObject == null)
                //Peguei o objeto
                {
                    heldObject = interactableObject.gameObject;
                    
                    interactableObject.PickUp();
                    interactableObject.SetParent(playerHoldPos);
                    Physics.IgnoreCollision(interactableObject.GetComponent<Collider>(), playerCollider.GetComponent<Collider>(), true);
                }
                else
                //Dropei o objeto 
                {
                    heldObject = null;
                    interactableObject.Drop();
                    interactableObject.ClearParent();
                    Physics.IgnoreCollision(interactableObject.GetComponent<Collider>(), playerCollider.GetComponent<Collider>(), false);
                }
            }
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * pickUpRange, Color.blue);
    }

    private void Update() 
    {
        RaycastHit hit; 
        //Disparei um raycast 

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange, ~playerLayer))
        {
            if(hit.transform.gameObject.TryGetComponent(out S_InteractableObject interactableObject)) 
            // Encontrei um objeto interagível
            {
                highlightObject = interactableObject.gameObject;
                interactableObject.SetOutline(true);

                if (heldObject != null)
                // Tenho um objeto e enxergo ele
                {
                    objectViewState = ObjectViewState.View_Hold;
                }
                else
                {
                    objectViewState = ObjectViewState.View_NoHold;
                }
            }
            else if (heldObject != null)
            // Tenho um objeto mas não enxergo ele
            {
                objectViewState = ObjectViewState.NoView_Hold;
            }
            else
            //Não tenho objeto em mãos e não enxergo nenhum
            {
                objectViewState = ObjectViewState.NoView_NoHold;

                if (highlightObject != null)
                {
                    highlightObject.GetComponent<S_InteractableObject>().SetOutline(false);
                    highlightObject = null;
                }
            }
        }
        else
        {
            if (highlightObject != null)
            {
                highlightObject.GetComponent<S_InteractableObject>().SetOutline(false);
                highlightObject = null;
            }

            objectViewState = ObjectViewState.NoView_NoHold;
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * pickUpRange, Color.green);
    }

    public ObjectViewState GetObjectViewState()
    {
        return objectViewState;
    }
}
