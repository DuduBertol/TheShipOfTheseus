using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FinalKey : MonoBehaviour
{
    [SerializeField] private S_FinalDoor finalDoor;

    [SerializeField] private string type; 

    public void Collect()
    {
        finalDoor.KeyCollected(type);

        gameObject.SetActive(false);

        SoundManager.Instance.PlayGetKeySound(transform.position, 0.8f);
    }
}
