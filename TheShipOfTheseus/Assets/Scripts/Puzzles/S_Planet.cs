using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Planet : MonoBehaviour
{
    public string planetName;
    public bool canBePlaced;

    public void SetParent(Transform newParent)
    {
        transform.parent = newParent;
        transform.localPosition = Vector3.zero;
        
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void FreezePlanet()
    {
        GetComponent<Collider>().enabled = false;
    }
}
