using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TRIGSecurity : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;

    private void OnCollisionEnter(Collision other) 
    {
        other.transform.position = spawnPos.position;
    }
}
