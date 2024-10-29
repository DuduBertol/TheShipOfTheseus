using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SecretButton : MonoBehaviour
{
    [SerializeField] private Transform wallToOpen;

    public void Interact()
    {
        if(wallToOpen != null) Destroy(wallToOpen.gameObject);
    }
}
