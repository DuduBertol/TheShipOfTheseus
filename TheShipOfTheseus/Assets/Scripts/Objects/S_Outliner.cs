using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Outliner : MonoBehaviour
{
    private Outline outline;

    //Recursive Unable Outline
    private float timer;
    private float timerMax = 15f;

    private void Awake() 
    {  
        outline = GetComponent<Outline>();
    }

    private void Start() 
    {
        outline.enabled = false;
    }

    private void Update() 
    {
        timer += Time.deltaTime;

        if (timer > timerMax)
        {
            SetOutline(false);
            
            timer = 0f;
        }
    }

    public void SetOutline(bool value)
    {
        outline.enabled = value;
    }

    
}
