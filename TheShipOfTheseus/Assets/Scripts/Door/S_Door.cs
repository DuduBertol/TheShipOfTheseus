using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Door : MonoBehaviour
{
    public SO_KeyDoor keyDoorSO;

    public void OpenDoor()
    {
        Debug.Log("Tentei abrir porta!");

        if(keyDoorSO.isUnlocked)
        {
            Debug.Log("Porta Aberta!");
            Destroy(gameObject);
        }
    }
}
