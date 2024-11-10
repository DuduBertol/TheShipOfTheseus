using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_KeyReset : MonoBehaviour
{
    public List<SO_KeyDoor> keyDoorSOList;

    private void Start() 
    {
        for (int i = 0; i < keyDoorSOList.Count; i++)
        {
            keyDoorSOList[i].isUnlocked = false;
        }    
    }
}
