using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_InteractableObject : MonoBehaviour
{
    [SerializeField] private SO_InteractableObject interactableObjectSO;

    public SO_InteractableObject GetInteractableObjectSO()
    {
        return interactableObjectSO;
    }
}
