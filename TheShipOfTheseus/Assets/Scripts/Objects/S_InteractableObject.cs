using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_InteractableObject : MonoBehaviour
{
    [SerializeField] private SO_InteractableObject interactableObjectSO;
    [SerializeField] private GameObject originalObject;
    [SerializeField] private GameObject translucedObject;

    [Header(">> Dev Only <<")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;
    [SerializeField] private Transform thisParent;
    
    private S_PlayerPickUp playerPickUp;
    private Outline outline;
    private int defaultLayer = 0;
    private int holdLayer = 6;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();    
        outline = GetComponent<Outline>();
    }

    private void Start() 
    {
        playerPickUp = FindObjectOfType<S_PlayerPickUp>();

        outline.enabled = false;
    }

    private void Update() 
    {
    }

    public SO_InteractableObject GetInteractableObjectSO()
    {
        return interactableObjectSO;
    }

    public void SetParent(Transform parent)
    {
        transform.parent = parent;
        thisParent = parent;
    }

    public void ClearParent()
    {
        transform.parent = null;
        thisParent = null;
    }

    public void PickUp()
    {
        Selected();
    }

    public void Drop()
    {
        Unselected();
    }

    public void SetOutline(bool value)
    {
        outline.enabled = value;
    }

    private void Selected()
    {
        originalObject.transform.position = playerPickUp.playerTwinPos.position;
        translucedObject.SetActive(true);

        rb.isKinematic = true;
        col.isTrigger = true;
        gameObject.layer = holdLayer;
    }

    private void Unselected()
    {
        originalObject.transform.position = translucedObject.transform.position;
        col.isTrigger = false;
        translucedObject.SetActive(false);

        rb.isKinematic = false;
        gameObject.layer = defaultLayer;
    }

}
