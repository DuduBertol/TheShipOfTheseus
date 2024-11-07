using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FinalDoor : MonoBehaviour
{
    [SerializeField] private bool hasSunKey;
    [SerializeField] private bool hasMoonKey;

    [SerializeField] private bool isSunUnlocked;
    [SerializeField] private bool isMoonUnlocked;

    [SerializeField] private Transform keyMesh_SUN;
    [SerializeField] private Transform keyMesh_MOON;

    [SerializeField] private Animator animator;

    private void Start() 
    {
        
    }

    public void TryOpenDoor()
    {
        Debug.Log("Tentei abrir a porta final.");

        if(isSunUnlocked && isMoonUnlocked)
        {
            Debug.Log("Abri porta final!");

            if(animator.enabled)
            {
                animator.SetTrigger("OpenDoor");
            }
        }
        
        
        if(hasSunKey)
        {
            Debug.Log("Desbolqueei SOL!");

            UnlockSunDoor();
        }
        
        if(hasMoonKey)
        {
            Debug.Log("Desbolqueei LUA!");

            UnlockMoonDoor();
        }
    }
    
    public void DisableAnimator()
    {
        animator.enabled = false;
    }


    public void KeyCollected(string type)
    {
        if(type == "SUN")
        {
            hasSunKey = true;
        }
        else if(type == "MOON")
        {
            hasMoonKey = true;
        }
    }

    private void UnlockSunDoor()
    {
        isSunUnlocked = true;
        keyMesh_SUN.gameObject.SetActive(true);
    }

    private void UnlockMoonDoor()
    {
        isMoonUnlocked = true;
        keyMesh_MOON.gameObject.SetActive(true);
    }

}
