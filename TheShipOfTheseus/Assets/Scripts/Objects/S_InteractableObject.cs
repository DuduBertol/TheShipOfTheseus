using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_InteractableObject : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private SO_InteractableObject interactableObjectSO;
    [SerializeField] private GameObject originalObject;
    [SerializeField] private GameObject translucedObject;
    
    [Header("Rotation")]
	[SerializeField] private bool inverted;
	[SerializeField] private Vector2 rotation;
    [SerializeField] private bool rotateAllowed;

    [Header(">> Dev Only <<")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;
    
    [SerializeField] private Vector3 interactPos;
    [SerializeField] private Quaternion interactRot;

    private Transform thisParent;
    
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

        interactPos = transform.localPosition;
        interactRot = transform.localRotation;
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

    public void Inspect()
    {
        transform.position = playerPickUp.playerInspectPos.position;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        
        originalObject.transform.localPosition = Vector3.zero;
        translucedObject.transform.localPosition = Vector3.zero;
        translucedObject.SetActive(false);
    }

    public void BackInspect()
    {
        transform.localPosition = interactPos;
        transform.localRotation = interactRot;

        Selected();
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

    public void SetRotateBool(bool value)
    {
        rotateAllowed = value;
    }
    public bool GetRotateBool()
    {
        return rotateAllowed;
    }

    public IEnumerator Rotate()
	{
		rotateAllowed = true;

		while(rotateAllowed)
		{
            rotation = GameInput.Instance.GetMouseMovementVectorNormalizedPlayer();

			// apply rotation
			rotation *= S_PlayerCam.Instance.rotationSens;
			transform.Rotate(Vector3.up * (inverted? 1: -1), rotation.x, Space.World);
			transform.Rotate(S_PlayerCam.Instance.gameObject.transform.right * (inverted? -1: 1), rotation.y, Space.World);
			yield return null;
		}
	}

}
