using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_KeyDoor : ScriptableObject
{
    public GameObject doorPrefab;
    public GameObject keyPrefab;
    public bool isUnlocked;
}
