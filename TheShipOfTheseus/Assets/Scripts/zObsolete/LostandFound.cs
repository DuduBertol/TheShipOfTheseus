using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostandFound : MonoBehaviour
{
    [SerializeField] private Transform lostAndFoundPoint;

    private void OnTriggerExit(Collider collider) 
    {
        collider.transform.position = lostAndFoundPoint.position; 
    }
}
