using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Outliner : MonoBehaviour
{
    private Outline outline;

    private void Awake() 
    {  
        outline = GetComponent<Outline>();
    }

    private void Start() 
    {
        outline.enabled = false;
    }

    public void SetOutline(bool value)
    {
        outline.enabled = value;
    }
}
