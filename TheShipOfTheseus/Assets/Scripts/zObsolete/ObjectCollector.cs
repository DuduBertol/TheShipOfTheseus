using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ObjectCollector : MonoBehaviour
{
    [SerializeField] private Transform objectPointTransform;
    [SerializeField] private Transform thisObjectChild;
    [SerializeField] private Transform myVirtualCamera;

    [SerializeField] private bool isOnObjectView;
    private Transform myCamera;

    private Movement movement;

    private void Awake() 
    {
        movement = GetComponent<Movement>();    
    }
    private void Start() 
    {
        myCamera = Camera.main.transform;    
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            isOnObjectView = !isOnObjectView;
            
            if(isOnObjectView)
            {
                // myVirtualCamera.GetComponent<CinemachineVirtualCamera>(). = false;
                // movement.ChangeForObjectView(true);
            }
            else
            {
                myVirtualCamera.GetComponent<CinemachineVirtualCamera>().enabled = true;
                // movement.ChangeForObjectView(false);
            }
        }

        if(isOnObjectView)
        {
            objectPointTransform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            objectPointTransform.localRotation = Quaternion.Euler(y * 10, x * 10, 0);
        }
    }


}
