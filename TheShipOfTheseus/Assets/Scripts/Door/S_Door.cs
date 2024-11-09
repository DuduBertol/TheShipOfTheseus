using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Door : MonoBehaviour
{
    public SO_KeyDoor keyDoorSO;
    public Animator animator;

    private bool isOpen;

    public void OpenDoor()
    {
        Debug.Log("Tentei abrir porta!");

        if(keyDoorSO.isUnlocked && !isOpen)
        {
            isOpen = true;
            Debug.Log("Abri porta!");
            if(animator.enabled)
            {
                animator.SetTrigger("OpenDoor");
                SoundManager.Instance.PlayOpenDoorSound(transform.position, 0.4f);
            }
        }
    }

    public void DisableAnimator()
    {
        animator.enabled = false;
    }
}
