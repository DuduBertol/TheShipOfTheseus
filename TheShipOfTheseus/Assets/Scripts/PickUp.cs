using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    /* public event EventHandler<OnAnyObjectDroppedOnBoardEventArgs> OnAnyObjectDroppedOnBoard;
    public class OnAnyObjectDroppedOnBoardEventArgs: EventArgs
    {
        public BoardObject boardObject;
    } */

    public GameObject player;
    public Transform holdPos;
    public Transform objectsPlaceArea;
    public float pickUpRange = 5f;
    public float throwForce = 500f;
    
    [Range(0.1f, 9f)][SerializeField] private float rotationSensitivity = 1f;
    [SerializeField] private LayerMask playerLayer;
    
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private Movement movement;

    
    private GameObject heldObject;
    // private BoardObjectSO boardObjectSO;
    private Rigidbody heldObjectRigidboby;
    private bool canDrop = true;
    private float initialCamSensivity;
    private float initialMoveSensivity;
    private int layerNumber;


    private void Start() 
    {
        cameraMovement.enabled = true;

        layerNumber = LayerMask.NameToLayer("holdLayer");

        initialCamSensivity = cameraMovement.sensitivity;
        initialMoveSensivity = movement.sensitivity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //change E to whichever key you want to press to pick up
        {
            if (heldObject == null) //if currently not holding anything
            {
                //perform raycast to check if player is looking at object within pickuprange
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange, ~playerLayer))
                {
                    //make sure pickup tag is attached
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        if(hit.transform.gameObject.GetComponent<BoardObject>().GetIsOnBoard() && hit.transform.gameObject.GetComponent<BoardObject>().GetIsMainObject())
                        {
                            hit.transform.gameObject.GetComponent<BoardObject>().SetIsOnBoard(false);
                            GameController.Instance.DecreaseObjectsInBoardAmount();

                        }
                        if(hit.transform.gameObject.GetComponent<BoardObject>().GetIsMainObject())
                        {
                            SoundManager.Instance.PlayPaperSound(hit.transform.position, 1);
                        }
                        else
                        {
                            SoundManager.Instance.PlayPinSound(hit.transform.position, 1);
                        }
                        //pass in object hit into the PickUpObject function
                        PickUpObject(hit.transform.gameObject);

                    }
                }
            }
            else
            {
                if(canDrop == true)
                {
                    //StopClipping(); //prevents object from clipping through walls
                    if(heldObject.GetComponent<BoardObject>().GetCanDropOnBoard()) 
                    { //drop on board
                        if(!heldObject.transform.gameObject.GetComponent<BoardObject>().GetIsOnBoard() && heldObject.transform.gameObject.GetComponent<BoardObject>().GetIsMainObject())
                        {
                            heldObject.transform.gameObject.GetComponent<BoardObject>().SetIsOnBoard(true);
                            GameController.Instance.IncreaseObjectsInBoardAmount();

                            SoundManager.Instance.PlayPaperSound(heldObject.transform.position, 1);
                        }
                        else
                        {
                            SoundManager.Instance.PlayPinSound(heldObject.transform.position, 1);
                        }
                        DropObjectOnBoard();
                    }
                    else 
                    { //drop on floor
                        DropObject();
                    }
                }
            }
        }

        if (heldObject != null) //if player is holding object
        {
            MoveObject(); //keep object position at holdPos
            RotateObject();
            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true) //Mous0 (leftclick) is used to throw, change this if you want another button to be used)
            {
                // StopClipping();
                ThrowObject();
            }

        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * pickUpRange, Color.green);

        //meus testes
        /* if(GetHeldObjectBoardObjectSO().parentOnBoard != null)
        {
            Debug.Log(GetHeldObjectBoardObjectSO().parentOnBoard.transform.position);
        } */

    }

    private void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) 
        {
            heldObject = pickUpObj; 
            heldObjectRigidboby = pickUpObj.GetComponent<Rigidbody>(); 
            heldObjectRigidboby.isKinematic = true;
            heldObjectRigidboby.transform.parent = holdPos.transform; 
            heldObject.layer = layerNumber; //change the object layer to the holdLayer
            if(heldObject.transform.childCount != 0)
            {
                heldObject.transform.GetChild(0).gameObject.layer = layerNumber;
                heldObject.transform.GetChild(1).gameObject.layer = layerNumber;
            }

            heldObject.transform.rotation = Quaternion.RotateTowards(heldObject.transform.rotation, Quaternion.Euler(0, 90, 0), 1f);

            // heldObject.transform.position = holdPos.transform.position; 
            // heldObject.transform.position = Vector3.zero;
            // ResetObjectRotation();

            /* Debug.Log(heldObject.transform.position);
            Debug.Log(holdPos.transform.position);
            Debug.Log(heldObject.transform.localPosition);
            Debug.Log(holdPos.transform.localPosition); */


            Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    private void DropObject()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObject.layer = 8; //object assigned back to default layer
        if(heldObject.transform.childCount != 0)
        {
            heldObject.transform.GetChild(0).gameObject.layer = 8;
            heldObject.transform.GetChild(1).gameObject.layer = 8;
        }
        heldObjectRigidboby.isKinematic = false;
        heldObject.transform.parent = null; //unparent object
        heldObject = null; //undefine game object
    }
    
    private void DropObjectOnBoard()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObject.layer = 8; //object assigned back to default layer
        if(heldObject.transform.childCount != 0)
        {
            heldObject.transform.GetChild(0).gameObject.layer = 8;
            heldObject.transform.GetChild(1).gameObject.layer = 8;
        }
        heldObjectRigidboby.isKinematic = true;
        heldObject.transform.parent = objectsPlaceArea; //parent as board
        heldObject.transform.localPosition = new Vector3(0, heldObject.transform.localPosition.y, heldObject.transform.localPosition.z); 
        heldObject.transform.localEulerAngles = Vector3.zero;
        heldObject.GetComponent<BoardObject>().SetIsOnBoard(true);

        heldObject = null; //undefine game object

        // GetChildInBoard();
    }

    private void RotateObject()
    {
        if (Input.GetKey(KeyCode.R))//hold R key to rotate, change this to whatever key you want
        {
            GameController.Instance.ShowLens(true);

            canDrop = false; //make sure throwing can't occur during rotating

            //disable player being able to look around

            cameraMovement.sensitivity = 0f;
            movement.sensitivity = 0f;

            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
            float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;
            //rotate the object depending on mouse X-Y Axis
            heldObject.transform.Rotate(Vector3.down, XaxisRotation);
            heldObject.transform.Rotate(Vector3.forward, YaxisRotation);
        }
        else
        {
            GameController.Instance.ShowLens(false);

            //re-enable player being able to look around
            cameraMovement.sensitivity = initialCamSensivity;
            movement.sensitivity = initialMoveSensivity;
            canDrop = true;
        }
    }

    private void MoveObject()
    {
        //keep object position the same as the holdPosition position
        heldObject.transform.position = holdPos.transform.position;
    }

    private void ThrowObject()
    {
        SoundManager.Instance.PlayThrowSound(heldObject.transform.position, 1);

        //same as drop function, but add force to object before undefining it
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

    /* private void ResetObjectRotation()
    {
        Vector3 actualRotation = heldObject.transform.eulerAngles;

        heldObject.transform.localEulerAngles = new Vector3(Mathf.Lerp(actualRotation.x, 0, 0.2f), Mathf.Lerp(actualRotation.y, 90f, 0.2f), Mathf.Lerp(actualRotation.y, 0, 0.2f));
    } */

    private BoardObjectSO GetHeldObjectBoardObjectSO()
    {
        return heldObject.GetComponent<BoardObject>().GetBoardObjectSO();
    }

    
}
