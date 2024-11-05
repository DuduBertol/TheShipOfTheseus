using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Key : MonoBehaviour
{
    public SO_KeyDoor keyDoorSO;

    private void Start() 
    {
        keyDoorSO.isUnlocked = false;    
    }

    public void GetKey()
    {
        Debug.Log("Coletei chave!");
        
        keyDoorSO.isUnlocked = true;

        gameObject.SetActive(false);
    }
}
