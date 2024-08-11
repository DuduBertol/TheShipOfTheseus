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
    [SerializeField] private List<Transform> objectsInBoardList;
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private Movement movement;
    
    private GameObject heldObject;
    // private BoardObjectSO boardObjectSO;
    private Rigidbody heldObjectRigidboby;
    private bool canDrop = true;
    private float initialCamSensivity;
    private float initialMoveSensivity;


    private void Start() 
    {
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
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    //make sure pickup tag is attached
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        if(hit.transform.gameObject.GetComponent<BoardObject>().GetIsOnBoard() && hit.transform.gameObject.GetComponent<BoardObject>().GetIsMainObject())
                        {
                            hit.transform.gameObject.GetComponent<BoardObject>().SetIsOnBoard(false);
                            GameController.Instance.DecreaseObjectsInBoardAmount();
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
            heldObject.transform.position = holdPos.transform.position; 

            // heldObj.layer = LayerNumber; //change the object layer to the holdLayer
            Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    private void DropObject()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        //heldObj.layer = 0; //object assigned back to default layer
        heldObjectRigidboby.isKinematic = false;
        heldObject.transform.parent = null; //unparent object
        heldObject = null; //undefine game object
    }
    
    private void DropObjectOnBoard()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        //heldObj.layer = 0; //object assigned back to default layer
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
            canDrop = false; //make sure throwing can't occur during rotating

            //disable player being able to look around

            cameraMovement.sensitivity = 0f;
            movement.sensitivity = 0f;

            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
            float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;
            //rotate the object depending on mouse X-Y Axis
            heldObject.transform.Rotate(Vector3.down, XaxisRotation);
            heldObject.transform.Rotate(Vector3.right, YaxisRotation);
        }
        else
        {
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
        //same as drop function, but add force to object before undefining it
        Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        // heldObj.layer = 0;
        heldObjectRigidboby.isKinematic = false;
        heldObject.transform.parent = null;
        heldObjectRigidboby.AddForce(transform.forward * throwForce);
        heldObject = null;
    }

    private BoardObjectSO GetHeldObjectBoardObjectSO()
    {
        return heldObject.GetComponent<BoardObject>().GetBoardObjectSO();
    }

    /* private void GetChildInBoard()
    {
        int child = objectsPlaceArea.childCount;

        for (int i = 0; i < child; i++)
        {
            objectsInBoardList.Append(objectsPlaceArea.GetChild(i));
        }
    } */

    
}
