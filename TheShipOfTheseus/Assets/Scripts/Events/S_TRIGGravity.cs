using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TRIGGravity : MonoBehaviour
{
    private Vector3 gravity;
    private Vector3 gravityHigh = new Vector3(0, -30f, 0);

    private void Start() 
    {
        gravity = Physics.gravity;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Physics.gravity = gravityHigh;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Physics.gravity = gravity;
        }
    }
}
