using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_InteractableObject : MonoBehaviour
{
    [SerializeField] private SO_InteractableObject interactableObjectSO;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform thisParent;

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();    
    }

    public SO_InteractableObject GetInteractableObjectSO()
    {
        return interactableObjectSO;
    }

    public void SetParent(Transform parent)
    {
        rb.isKinematic = true;
        transform.parent = parent;
    }

    public void ClearParent()
    {
        rb.isKinematic = false;
        transform.parent = null;
    }

    private void Update() 
    {
        if(thisParent != null)
        {
            transform.localPosition = Vector3.zero;
        }    
    }
}
