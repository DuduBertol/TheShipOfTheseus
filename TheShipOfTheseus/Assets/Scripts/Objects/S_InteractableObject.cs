using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_InteractableObject : MonoBehaviour
{
    [SerializeField] private SO_InteractableObject interactableObjectSO;

    [Header(">> Dev Only <<")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform thisParent;
    
    private int defaultLayer = 0;
    private int holdLayer = 6;

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();    
    }

    private void Update() 
    {
        if(thisParent != null)
        {
            transform.localPosition = Vector3.zero;
        }    
    }

    public SO_InteractableObject GetInteractableObjectSO()
    {
        return interactableObjectSO;
    }

    public void SetParent(Transform parent)
    {
        transform.parent = parent;
    }

    public void ClearParent()
    {
        transform.parent = null;
    }

    public void PickUp()
    {
        rb.isKinematic = true;
        gameObject.layer = holdLayer;
    }

    public void Drop()
    {
        rb.isKinematic = false;
        gameObject.layer = defaultLayer;
    }
}
