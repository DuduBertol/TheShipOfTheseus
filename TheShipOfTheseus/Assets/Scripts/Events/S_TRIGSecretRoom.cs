using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TRIGSecretRoom : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            S_EventManager.Instance.Love_SpawnLoveSecretRoom();

            Destroy(gameObject);
        }
    }
}
