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
    [SerializeField] private bool isInspecting;

    [Header(">> Dev Only <<")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;
    
    [SerializeField] private Vector3 interactPos;
    [SerializeField] private Quaternion interactRot;

    private Transform thisParent;
    
    private S_PlayerPickUp playerPickUp;
    
    private int defaultLayer = 0;
    private int holdLayer = 6;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();    
    }

    private void Start() 
    {
        playerPickUp = FindObjectOfType<S_PlayerPickUp>();
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

        SoundManager.Instance.PlayPickUpSound(transform.position, 0.1f);
    }

    public void Drop()
    {
        Unselected();

        SoundManager.Instance.PlayDropSound(transform.position, 0.1f);
    }

    public void Inspect()
    {
        isInspecting = true;

        transform.position = playerPickUp.playerInspectPos.position;
        // transform.rotation = Quaternion.Euler(0, 0, 0);
        
        originalObject.transform.localPosition = Vector3.zero;
        translucedObject.transform.localPosition = Vector3.zero;
        translucedObject.SetActive(false);

        
        Cursor.lockState = CursorLockMode.Confined;    
        Cursor.visible = true;
    }

    public void BackInspect()
    {
        isInspecting = false;

        Cursor.lockState = CursorLockMode.Locked;    
        Cursor.visible = false;

        transform.localPosition = new Vector3(0, 0, interactPos.z);
        // transform.localRotation = interactRot;

        Selected();
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
        isInspecting = true;

		while(rotateAllowed && isInspecting)
		{

            rotation = GameInput.Instance.GetMouseMovementVectorNormalizedPlayer();

			// apply rotation
			rotation *= (S_PlayerCam.Instance.rotationSens/10);
			transform.Rotate(Vector3.up * (inverted? 1: -1), rotation.x, Space.World);
			transform.Rotate(S_PlayerCam.Instance.gameObject.transform.right * (inverted? -1: 1), rotation.y, Space.World);

			yield return null;
		}
	}

}
