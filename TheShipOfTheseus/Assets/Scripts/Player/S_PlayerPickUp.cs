using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_PlayerPickUp : MonoBehaviour
{
    public static S_PlayerPickUp Instance {get; private set;}

    public event EventHandler <OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs: EventArgs
    {
        public PlayerActionState state;
    }

    public enum PlayerActionState
    {
        Interact,
        Inspect,
        AnyState
    }
    public PlayerActionState playerActionState;

    public enum ObjectViewState
    {
        View_Hold,
        NoView_Hold,
        View_NoHold,
        NoView_NoHold
    }
    public ObjectViewState objectViewState;

    public float pickUpRange;
    public Transform playerHoldPos;
    public Transform playerTwinPos;
    public Transform playerInspectPos;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Collider playerCollider;
    [SerializeField] private GameObject heldObject;
    [SerializeField] private GameObject highlightObject;

    private void Awake() 
    {
        Instance = this;    
    }

    private void Start() 
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnInspectAction += GameInput_OnInspectAction;
        GameInput.Instance.OnRotationAction += GameInput_OnRotationAction;
        GameInput.Instance.OnRotationCanceledAction += GameInput_OnRotationCanceledAction;
    }

    private void GameInput_OnRotationCanceledAction(object sender, EventArgs e) // Mouse Esquerdo Hold
    {  
        if(playerActionState != PlayerActionState.Inspect) return;
        
        Debug.Log("Rotation Canceled!"); 
        RotateCanceled();
    }
    private void GameInput_OnRotationAction(object sender, EventArgs e) // Mouse Esquerdo Hold
    { 
        if(playerActionState != PlayerActionState.Inspect) return;

        Debug.Log("Rotation!"); 
        Rotate();
    }
    private void GameInput_OnInspectAction(object sender, EventArgs e) // Mouse Esquerdo
    {
        Debug.Log("Inspect!"); 


        if(playerActionState == PlayerActionState.AnyState) return;
            //Não tenho objeto - Logo Any State

        else if(playerActionState == PlayerActionState.Interact)
        //Tenho objeto e estou em Interact, logo entrar em Inspect
        {
            ChangeState(PlayerActionState.Inspect);
            
            heldObject.GetComponent<S_InteractableObject>().Inspect();
            
            S_PlayerCam.Instance.isFreezed = true;
            S_PlayerMovement.Instance.isFreezed = true;
        }


        else if(playerActionState == PlayerActionState.Inspect)
        //Tenho objeto e estou em Inspect, logo voltar
        {
            ChangeState(PlayerActionState.Interact);
            
            heldObject.GetComponent<S_InteractableObject>().BackInspect();

            S_PlayerCam.Instance.isFreezed = false;
            S_PlayerMovement.Instance.isFreezed = false;
        }
            
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) // Mouse Esquerdo
    {
        if(playerActionState == PlayerActionState.Interact || playerActionState == PlayerActionState.AnyState)
        {
            InteractAction();
        }
    }

    private void Update() 
    {
        RaycastHit hit; 
        //Disparei um raycast 

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange, ~playerLayer))
        {
            //OBJETO
            if(hit.transform.gameObject.TryGetComponent(out S_InteractableObject interactableObject)) 
            // Encontrei um objeto interagível
            {
                highlightObject = interactableObject.gameObject;

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
                    highlightObject.GetComponent<S_Outliner>().SetOutline(false);
                    highlightObject = null;
                }
            }

            //OUTLINE
            if(hit.transform.gameObject.TryGetComponent(out S_Outliner outliner))
            {
                objectViewState = ObjectViewState.View_NoHold;

                highlightObject = outliner.gameObject;
                outliner.SetOutline(true);
            }

        }
        else
        {
            if (highlightObject != null)
            {
                highlightObject.GetComponent<S_Outliner>().SetOutline(false);
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

    public void ChangeState(PlayerActionState state)
    {
        playerActionState = state;

        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
            state = state
        });
    }

    private void InteractAction()
    {
        Debug.Log("Interact!");      

        RaycastHit hit; 
        //Disparei um raycast

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange, ~playerLayer))
        {
            if(hit.transform.gameObject.TryGetComponent(out S_InteractableObject interactableObject)) 
            // Encontrei um objeto interagível
            {
                Interact(interactableObject);
            }
            else if(hit.transform.gameObject.TryGetComponent(out S_Key key))
            {
                GetKey(key);
            }
            else if(hit.transform.gameObject.TryGetComponent(out S_Door door))
            {
                OpenDoor(door);
            }
            else if(hit.transform.gameObject.TryGetComponent(out S_SingleSlider slider))
            {
                SliderAction(slider);
            }
            else if(hit.transform.gameObject.TryGetComponent(out S_Lever lever))
            {
                LeverAction(lever);
            }
            else if(hit.transform.gameObject.TryGetComponent(out S_SingleLocker singleLocker))
            {
                SingleLockerAction(singleLocker);
            }
            else if(hit.transform.gameObject.TryGetComponent(out S_SingleVaultLocker singleVaultLocker))
            {
                SingleVaultLockerAction(singleVaultLocker);
            }
            else if(hit.transform.gameObject.TryGetComponent(out S_SecretButton secretButton))
            {
                SecretButtonAction(secretButton);
            }
            else if(hit.transform.gameObject.TryGetComponent(out S_SecretBook secretBook))
            {
                SecretBookAction(secretBook);
            }
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * pickUpRange, Color.blue);
    }

    private void Rotate()
    {
        StartCoroutine(heldObject.GetComponent<S_InteractableObject>().Rotate());
    }
    private void RotateCanceled()
    {
        heldObject.GetComponent<S_InteractableObject>().SetRotateBool(false);
    }

    private void Interact(S_InteractableObject interactableObject)
    {
        if(heldObject == null)
                //Peguei o objeto
                {
                    heldObject = interactableObject.gameObject;
                    
                    interactableObject.PickUp();
                    interactableObject.SetParent(playerHoldPos);
                    Physics.IgnoreCollision(interactableObject.GetComponent<Collider>(), playerCollider.GetComponent<Collider>(), true);

                    ChangeState(PlayerActionState.Interact);
                }
                else
                //Dropei o objeto 
                {
                    heldObject = null;
                    interactableObject.Drop();
                    interactableObject.ClearParent();
                    Physics.IgnoreCollision(interactableObject.GetComponent<Collider>(), playerCollider.GetComponent<Collider>(), false);

                    ChangeState(PlayerActionState.AnyState);
                }
    }

    private void GetKey(S_Key key)
    {
        key.GetKey();
    }
    
    private void OpenDoor(S_Door door)
    {
        door.OpenDoor();
    }

    private void SliderAction(S_SingleSlider slider)
    {
        slider.Interact();
    }
    private void LeverAction(S_Lever lever)
    {
        lever.Interact();
    }
    private void SingleLockerAction(S_SingleLocker singleLocker)
    {
        singleLocker.Interact();
    }
    private void SingleVaultLockerAction(S_SingleVaultLocker singleVaultLocker)
    {
        singleVaultLocker.Interact();
    }

    private void SecretButtonAction(S_SecretButton secretButton)
    {
        secretButton.Interact();
    }

    private void SecretBookAction(S_SecretBook secretBook)
    {
        secretBook.Interact();
    }
}
