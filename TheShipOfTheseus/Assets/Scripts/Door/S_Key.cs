using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Key : MonoBehaviour
{
    public SO_KeyDoor keyDoorSO;

    public void GetKey()
    {
        Debug.Log("Coletei chave!");
        
        keyDoorSO.isUnlocked = true;
        gameObject.SetActive(false);

        SoundManager.Instance.PlayUnlockDoorSound(transform.position, 0.4f);
    }
}
