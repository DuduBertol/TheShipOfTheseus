using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BookshelfSecretDoor : MonoBehaviour
{
    public event EventHandler OnFailed;

    public bool isOpen;

    [SerializeField] private string password;
    [SerializeField] private string tempPassword;

    [SerializeField] private Animator animator;

    private void OpenDoor()
    {
        Debug.Log("PORTA SECRETA Aberta!");

        isOpen = true;

        animator.SetTrigger("OpenDoor");
    }

    public void AddToPassword(string value)
    {   
        tempPassword += value;

        if(tempPassword == password)
        {
            Debug.Log("Senha Correta!");
            
            OpenDoor();
        }
        else if(tempPassword.Length == password.Length)
        {
            ResetPassword();
        }
    }

    public void ResetPassword()
    {
        tempPassword = "";
        
        OnFailed?.Invoke(this, EventArgs.Empty);
    }
}
